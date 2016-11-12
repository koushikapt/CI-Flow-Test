using App.Core;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

namespace App.Presentation.Web
{
    /// <summary>
    /// Extend the Owin Startup Class For Authorization Setup.
    /// </summary>
    public static partial class Startup
    {
        /// <summary>
        /// Setup the OAuth Method.
        /// For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864.
        /// </summary>
        public static void ConfigureAuth(IAppBuilder app)
        {
            // Enable the application to use a cookie to store information for the signed in user
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                CookieName = ".APP.AUTH",
                LoginPath = new PathString(Common.ApplicationRelativePathLogin),
                LogoutPath = new PathString(Common.ApplicationRelativePathLogout)
            });

            // Use a cookie to temporarily store information about a user logging in with a third party login provider
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);
        }
    }
}