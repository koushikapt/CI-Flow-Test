using Castle.Core.Logging;
using System.Web.Mvc;

namespace App.Presentation.Web.Controllers
{
    /// <summary>
    /// Main/Home Web Controller.
    /// </summary>
    [Authorize]
    public class HomeController : Controller
    {
        #region Services
        /// <summary>
        /// Gets or sets the application logger.
        /// </summary>
        public ILogger Logger { get; set; }
        #endregion

        #region Actions
        /// <summary>
        /// Retrieve the Main Working View.
        /// </summary>
        public ActionResult Index()
        {
            Logger.Warn("Index Warning");
            return View();
        }
        #endregion
    }
}