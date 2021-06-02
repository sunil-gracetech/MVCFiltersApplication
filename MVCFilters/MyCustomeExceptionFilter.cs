using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCFilters
{
    public class MyCustomeExceptionFilter : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            ExceptionLogger logger = new ExceptionLogger()
            {
                ExceptionMessage = filterContext.Exception.Message,
                ExceptionStackTrack = filterContext.Exception.StackTrace,
                ControllerName = filterContext.RouteData.Values["controller"].ToString(),
                ActionName = filterContext.RouteData.Values["action"].ToString(),
                ExceptionLogTime = DateTime.Now.ToString()
            };


              chetuAuthoEntities db = new chetuAuthoEntities();
        db.ExceptionLoggers.Add(logger);
        db.SaveChanges();

        //var ActionName = filterContext.RouteData.Values["action"].ToString();
        //    Debug.WriteLine(ActionName + " time: " + DateTime.Now.ToLongDateString());

            filterContext.ExceptionHandled = true;
            filterContext.Result = new ViewResult()
            {
                ViewName = "Error"
            };
        }
    }
}