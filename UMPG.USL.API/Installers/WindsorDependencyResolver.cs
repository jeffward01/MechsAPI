using System;
using System.Web.Http.Dependencies;
using Castle.Windsor;


namespace UMPG.USL.API.Installers
{
    internal class WindsorDependencyResolver : WindsorDependencyScope, IDependencyResolver
    {
        private readonly IWindsorContainer _container;

        public WindsorDependencyResolver(IWindsorContainer container)
            : base(container)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }

            _container = container;
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }
    }
}