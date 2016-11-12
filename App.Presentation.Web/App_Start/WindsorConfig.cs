using App.Core;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.MicroKernel;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Installer;
using System;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace App.Presentation.Web
{
    /// <summary>
    /// Castle Windsor DI and IoC Configuration.
    /// </summary>
    public class WindsorConfig : IWindsorInstaller
    {
        #region Static Properties
        /// <summary>
        /// Gets the applciation Bin Directory path.
        /// </summary>
        /// <value>
        /// The assembly directory.
        /// </value>
        public static string AssemblyDirectory
        {
            get
            {
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var path = new Uri(codeBase);
                return Path.GetDirectoryName(path.LocalPath);
            }
        }

        /// <summary>
        /// Gets Castle Windsor Resolve Container.
        /// </summary>
        /// <value>
        /// The resolve container.
        /// </value>
        public static IWindsorContainer Container { get; private set; }

        /// <summary>
        /// Gets the business libraries assembly filter.
        /// </summary>
        /// <value>
        /// The business libraries assembly filter.
        /// </value>
        public static AssemblyFilter Filter
        {
            get
            {
                return new AssemblyFilter(AssemblyDirectory).FilterByName(name => name.Name.StartsWith(Common.ApplicationBusinessLibrariesPrefix));
            }
        }
        #endregion

        #region Static Methods
        /// <summary>
        /// Setup the Winston Castle.
        /// </summary>
        public static void Setup()
        {
            Container = new WindsorContainer().Install(FromAssembly.This());
            WindsorControllerFactory controllerFactory = new WindsorControllerFactory(Container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
            // GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), controllerFactory);
        }

        private static void BeginContainerComponentLoggin(IKernel kernel)
        {
            var logger = kernel.Resolve<ILogger>();
            logger.Info("Installing components. " + AssemblyDirectory);
            kernel.HandlerRegistered += (IHandler handler, ref bool stateChanged) => logger.Info(handler.ComponentModel.Name + " is registered.");
        }
        #endregion

        #region IWindsorInstaller Members
        /// <summary>
        /// Installation on types resovers.
        /// </summary>
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            // Register Logger
            container.AddFacility<LoggingFacility>(f => f.UseNLog());

            // Track installation
            BeginContainerComponentLoggin(container.Kernel);

            // Register Services
            container.Register(
                Types.FromAssemblyInDirectory(Filter)
                     .Pick()
                     .If(t => t.Name.EndsWith("Service"))
                     .WithService
                     .FirstInterface()
                     .LifestylePerWebRequest());

            // Register Web Controller
            container.Register(
                Classes.FromThisAssembly()
                       .BasedOn(typeof(IController))
                       .LifestylePerWebRequest());
        }
        #endregion

        #region Types
        /// <summary>
        /// Windsor Controller Factory.
        /// </summary>
        protected class WindsorControllerFactory : DefaultControllerFactory //, IHttpControllerActivator
        {
            #region Fields
            private readonly IKernel kernel;
            #endregion

            #region Constructor
            /// <summary>
            /// Initializes a new instance of the <see cref="WindsorControllerFactory" /> class.
            /// </summary>
            public WindsorControllerFactory(IKernel kernel)
            {
                this.kernel = kernel;
            }
            #endregion

            /*
            #region Public Methods
            /// <summary>
            /// Release a controller instance.
            /// </summary>
            /// <param name="controller">The controller instance to be released.</param>
            public override void ReleaseController(IController controller)
            {
                kernel.ReleaseComponent(controller);
            }

            /// <summary>
            /// Creates an <see cref="T:System.Web.Http.Controllers.IHttpController" /> object.
            /// </summary>
            /// <param name="request">The message request.</param>
            /// <param name="controllerDescriptor">The HTTP controller descriptor.</param>
            /// <param name="controllerType">The type of the controller.</param>
            /// <exception cref="System.Web.HttpException">Must return 404 error.</exception>
            public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
            {
                if (controllerType == null)
                {
                    throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", request.RequestUri));
                }

                var controller = (IHttpController)this.kernel.Resolve(controllerType);
                request.RegisterForDispose(new Release(() => kernel.ReleaseComponent(controller)));
                return controller;
            }
            #endregion
            */

            #region Inner Methods
            /// <summary>
            /// Retrieve a Controller Instance.
            /// </summary>
            /// <param name="requestContext">Server requires.</param>
            /// <param name="controllerType">Controller type.</param>
            /// <returns>IController instance.</returns>
            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                if (controllerType == null)
                {
                    throw new HttpException(404, string.Format("The controller for path '{0}' could not be found.", requestContext.HttpContext.Request.Path));
                }

                try
                {
                    return (IController)kernel.Resolve(controllerType);
                }
                catch (Exception ex)
                {
                    kernel.Resolve<ILogger>().LogException(ex);
                    throw;
                }
            }

            /// <summary>
            /// Releases the specified controller.
            /// </summary>
            /// <param name="controller">The controller to release.</param>
            public override void ReleaseController(IController controller)
            {
                // If controller implements IDisposable, clean it up responsibly
                var disposableController = controller as IDisposable;
                if (disposableController != null)
                {
                    disposableController.Dispose();
                }

                // Inform Castle that the controller is no longer required
                kernel.ReleaseComponent(controller);
            }

            private class Release : IDisposable
            {
                private readonly Action release;

                public Release(Action release)
                {
                    this.release = release;
                }

                public void Dispose()
                {
                    this.release();
                }
            }
            #endregion
        }
        #endregion
    }
}