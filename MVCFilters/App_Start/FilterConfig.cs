﻿using System.Web;
using System.Web.Mvc;

namespace MVCFilters
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new MyCustomeExceptionFilter());
        }
    }
}
