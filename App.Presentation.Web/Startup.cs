using Microsoft.Owin;
using Owin;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

[assembly: OwinStartup(typeof(App.Presentation.Web.Startup))]

namespace App.Presentation.Web
{
    /// <summary>
    /// Startup Owin Configuration Class.
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// Initial Configuration Method.
        /// </summary>
        public static void Configuration(IAppBuilder app)
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            // GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            WindsorConfig.Setup();
            ConfigureAuth(app);
        }
    }
}