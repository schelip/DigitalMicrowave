[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(DigitalMicrowave.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(DigitalMicrowave.App_Start.NinjectWebCommon), "Stop")]

namespace DigitalMicrowave.App_Start
{
    using System;
    using System.Web;
    using System.Web.Http;
    using DigitalMicrowave.Web.Model.Validators;
    using DigitalMicrowave.Business.Services;
    using DigitalMicrowave.Infrastructure.Services;
    using DigitalMicrowave.Web.Model.InputModel;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;
    using Ninject.Web.WebApi;
    using FluentValidation;
    using DigitalMicrowave.Infrastructure.Data.Repositories;
    using DigitalMicrowave.Business.Repositories;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application.
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }

        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }

        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterRepositories(kernel);
                RegisterServices(kernel);
                RegisterValidators(kernel);
                GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IMicrowaveService>().To<MicrowaveService>();
            kernel.Bind<IHeatingProcedureService>().To<HeatingProcedureService>();
        }

        private static void RegisterValidators(IKernel kernel)
        {
            kernel.Bind<IValidator<StartHeatingInputModel>>().To<StartHeatingInputModelValidator>();
        }

        private static void RegisterRepositories(IKernel kernel)
        {
            kernel.Bind<IHeatingProcedureRepository>().To<HeatingProcedureRepository>();
        }
    }
}