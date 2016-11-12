using App.Core.Models.View;
using App.Core.Services.Membership;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSubstitute;
using System;
using System.Web.Mvc;
using App.Presentation.Web.Controllers;

namespace Tests.Functional.Web.MVC
{
    [TestClass]
    public class AccountControllerTests
    {
        #region Fields
        private AccountController controller = new AccountController();
        #endregion

        #region Setup
        [TestInitialize]
        public void Initialize()
        {
            controller.MembershipService = Substitute.For<IMembershipService>();
        }
        #endregion

        #region Login Tests
        [TestMethod]
        [TestCategory("Unit")]
        [TestProperty("Module", "Account Controller")]
        public void Login_PreserveReturnUrl()
        {
            var uri = new Uri("http://google.com");
            var result = controller.Login(uri);
            Assert.AreSame(result.ViewBag.ReturnUrl, uri);
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestProperty("Module", "Account Controller")]
        public void Login_NavigateToReturnUrl_WhenLoginSuccessful()
        {
            controller.MembershipService.Login(Arg.Any<LoginViewModel>()).Returns(true);
            var result = controller.Login(new LoginViewModel(), string.Empty) as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"].ToString());
            Assert.AreEqual("Home", result.RouteValues["Controller"].ToString());
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestProperty("Module", "Account Controller")]
        public void Login_CallMembershipLogin_WhenLogin()
        {
            var model = new LoginViewModel();
            controller.Login(model, "domain.com");
            controller.MembershipService.Received().Login(model);
        }
        #endregion

        #region Logout Tests
        [TestMethod]
        [TestCategory("Unit")]
        [TestProperty("Module", "Account Controller")]
        public void Logout_CallMembershipLogout_WhenLogout()
        {
            controller.Logout();
            controller.MembershipService.Received().Logout();
        }

        [TestMethod]
        [TestCategory("Unit")]
        [TestProperty("Module", "Account Controller")]
        public void Logout_NavigateToHome()
        {
            var result = controller.Logout() as RedirectToRouteResult;
            Assert.AreEqual("Index", result.RouteValues["Action"].ToString());
            Assert.AreEqual("Home", result.RouteValues["Controller"].ToString());
        }
        #endregion
    }
}
