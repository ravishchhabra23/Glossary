using GlossaryLogging;
using GlossaryWebUI.Helpers;
using Ninject;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace GlossaryWebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel kernel;
        public NinjectDependencyResolver(IKernel kernelparam)
        {
            kernel = kernelparam;
            AddBindings();
        }
        private void AddBindings()
        {
            // Add bindings here  
            try
            {
                kernel.Bind<IApiRestClient>().To<ApiRestClient>();
                kernel.Bind<ISessionHelper>().To<SessionHelper>();
                kernel.Bind<ILogger>().To<EventLogger>();
            }
            catch (Exception ex)
            {

            }
        }
        public object GetService(Type serviceType)
        {
            return kernel.TryGet(serviceType);
        }
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return kernel.GetAll(serviceType);
        }
    }
}