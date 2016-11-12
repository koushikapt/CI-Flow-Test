using Castle.Core.Logging;
using System;
using System.Web.Mvc;

namespace App.Presentation.Web
{
    /// <summary>
    /// Configure the Global Application Filters.
    /// </summary>
    public static class FilterConfig
    {
        /// <summary>
        /// Register Global Filters.
        /// </summary>
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new GeneralHandleErrorAttribute());
        }

        /// <summary>
        /// Global Error Handle Filter to Log all application exceptions by windson logger.
        /// </summary>
        [AttributeUsage(AttributeTargets.All)]
        private sealed class GeneralHandleErrorAttribute : HandleErrorAttribute
        {
            /// <summary>
            /// Perform action on application exception.
            /// </summary>
            public override void OnException(ExceptionContext filterContext)
            {
                WindsorConfig.Container.Resolve<ILogger>().LogException(filterContext.Exception);
                base.OnException(filterContext);
            }
        }
    }
}