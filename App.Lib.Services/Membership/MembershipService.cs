using App.Core.Models.View;
using App.Core.Services.Membership;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Security.Principal;
using System.Web;

namespace App.Services.Membership
{
    /// <summary>
    /// <see cref="IMembershipService" /> Service Implementation.
    /// </summary>
    public class MembershipService : IMembershipService
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipService"/> class.
        /// </summary>
        public MembershipService()
            : this(new HttpContextWrapper(HttpContext.Current).GetOwinContext().Authentication) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="MembershipService"/> class.
        /// </summary>
        /// <param name="authenticationManager">The authentication manager.</param>
        public MembershipService(IAuthenticationManager authenticationManager)
        {
            AuthenticationManager = authenticationManager;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets Manager that handle the authentication process.
        /// </summary>
        /// <value>
        /// The authentication manager.
        /// </value>
        protected IAuthenticationManager AuthenticationManager { get; private set; }
        #endregion

        #region IMembershipService Members
        /// See Interface For More
        public bool Login(LoginViewModel model)
        {
            if (!(model.Username == "admin" && model.Password == "Admin1234"))
            {
                return false;
            }

            AuthenticationManager.SignIn(
                new AuthenticationProperties() { IsPersistent = model.RememberMe },
                new GenericIdentity("Test User", DefaultAuthenticationTypes.ApplicationCookie));

            return true;
        }

        /// See Interface For More
        public void Logout()
        {
            AuthenticationManager.SignOut();
        }
        #endregion
    }
}