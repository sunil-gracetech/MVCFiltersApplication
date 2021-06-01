using MVCFilters.viewmodel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
namespace MVCFilters.Controllers
{
    public class AccountController : Controller
    {
        private chetuAuthoEntities authoEntities;

        public AccountController()
        {
            authoEntities = new chetuAuthoEntities();
        }
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(autho autho)
        {
            string password = MD5Hash(autho.password);
            if(authoEntities.authoes.Any(e=>e.email==autho.email && e.password ==password))
            {
                FormsAuthentication.SetAuthCookie(autho.email, false);
                return RedirectToAction("index", "products");
            }
            else
            {
                ModelState.AddModelError("email", "invalid login credentials !");
            }

            return View(autho);
        }
        [NonAction]
        public static string MD5Hash(string value)
        {
            return Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(new UTF8Encoding().GetBytes(value)));
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
            public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(autho autho)
        {
            if (ModelState.IsValid)
            {
                //  autho.password= Crypto.HashPassword("foo");
                autho.password = MD5Hash(autho.password);
                authoEntities.authoes.Add(autho);
                authoEntities.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {

                return View(autho);
            }
        }

        public ActionResult Forget()
        {
            ViewBag.stage = 0;
            return View();
        }

        [HttpPost]
        public ActionResult Forget(ForgetPassword forget)
        {
            if (authoEntities.authoes.Any(e => e.email == forget.Email))
            {
                var otp = GenerateOTP();

                if(SenEmail(forget.Email, "demo3408@gmail.com", otp))
                {
                    var user = authoEntities.authoes.SingleOrDefault(e => e.email == forget.Email);
                    var forgetpass = authoEntities.forgetPasswords.SingleOrDefault(e => e.userid == user.id);
                    if (forgetpass != null)
                    {
                        authoEntities.forgetPasswords.Remove(forgetpass);
                        authoEntities.SaveChanges();
                    }
                    var pass = new forgetPassword()
                    {
                        requestcode = otp,
                        requesttime = DateTime.Now,
                        autho = user
                    };
                    authoEntities.forgetPasswords.Add(pass);
                    authoEntities.SaveChanges();
                    ViewBag.stage = 1;
                    ViewBag.message = "Please check you email and submit given OTP !";
                }

            }
            else
            {
                ModelState.AddModelError("email", "User not found !");
            }
            return View();
        }

        [HttpPost]
        public ActionResult validateOtp(string otp)
        {
            if (otp != null)
            {
                var user = authoEntities.forgetPasswords.SingleOrDefault(e => e.requestcode == otp);

                if(user != null)
                {
                    var forgetpass = authoEntities.forgetPasswords.SingleOrDefault(e => e.userid == user.userid);
                    if (forgetpass != null)
                    {
                        authoEntities.forgetPasswords.Remove(forgetpass);
                        authoEntities.SaveChanges();
                    }
                    ViewBag.stage = 2;
                    Session["userid"] = user.userid;
                    return View("Forget");
                }
                else
                {
                    ViewBag.message = "Invalid OTP or OTP expire !";
                    return View("Forget");
                }
            }
            else
            {
                ViewBag.message = "Please enter OTP!";
                return View("Forget");
            }
        }

        [HttpPost]
        public ActionResult ChangePassword(string password, string confirmPassword)
        {
            if (password == confirmPassword)
            {
                int uid = (int)Session["userid"];
                var autho = authoEntities.authoes.SingleOrDefault(e => e.id ==uid );
                var hashpass = MD5Hash(confirmPassword);
                autho.password = hashpass;
                authoEntities.Entry(autho).State = System.Data.Entity.EntityState.Modified;
                authoEntities.SaveChanges();
                ViewBag.stage = 3;
                ViewBag.message = "Password reset successfully";
                return View("Forget");
            }
            else
            {
                ViewBag.stage = 2;
                ViewBag.message = "confirm password not matched";
                return View("Forget");
            }
        }
        [NonAction]
        public bool SenEmail(string to, string from, string otp)
        {
            var senderEmail = new MailAddress(from ,"Password Reovery");
            var receiverEmail = new MailAddress(to, "Receiver");
            var password = "9958119950";
            var sub = "Password Recovery";

            StringBuilder sb = new StringBuilder();
            sb.Append("<h4>Dear user please provide below OTP to change your password</h4>");
            sb.Append("<p> Your OTP is : " + otp + " valid for 30 minute </p>");

            var smtp = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(senderEmail.Address, password)
            };
            using (var mess = new MailMessage(senderEmail, receiverEmail)
            {
                Subject = sub,
                Body = sb.ToString(),
                IsBodyHtml=true
            })
            {
                smtp.Send(mess);
            }
            return true;
        }
        [NonAction]
        public string GenerateOTP()
        {
            var otp = new Random().Next(1111, 9999);
            return otp.ToString();
        }
    }
}