using App.Core.Models.View;
using App.Core.Services.Membership;
using System;
using System.Web.Mvc;

namespace App.Presentation.Web.Controllers
{
    /// <summary>
    /// Account Web Controller.
    /// </summary>
    public class AccountController : Controller
    {
        #region Services
        /// <summary>
        /// Gets or sets the membership service.
        /// </summary>
        /// <value>
        /// The membership service.
        /// </value>
        public IMembershipService MembershipService { get; set; }
        #endregion

        #region Actions
        /// <summary>
        /// Retrieve the Login View.
        /// </summary>
        /// <param name="returnUrl">Return to URL.</param>
        public ViewResult Login(Uri returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        /// <summary>
        /// Perform a Login Operation.
        /// </summary>
        /// <param name="model">Login view model.</param>
        /// <param name="returnUrl">Return to URL.</param>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                if (MembershipService.Login(model))
                {
                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Can't login! Wrong username or password.");
                }
            }

            return View(model);
        }

        /// <summary>
        /// Perform a Logout Operation.
        /// </summary>
        [Authorize]
        public ActionResult Logout()
        {
            MembershipService.Logout();
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region Inner Methods
        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url != null && Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }
        #endregion
    }
}