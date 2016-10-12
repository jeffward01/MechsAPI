using System.Web.Http;
using Castle.Core.Logging;
using Castle.Facilities.Logging;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using UMPG.USL.API.Controllers;

namespace UMPG.USL.API.Installers
{
    public class ApiInstaller: IWindsorInstaller
    {
    
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(
                Classes.FromThisAssembly()
                    .Where(x => x.FullName.EndsWith("Controller"))
                    .Configure(c => c.LifestyleScoped()));
            container.AddFacility<LoggingFacility>(f => f.LogUsing(LoggerImplementation.NLog)
                                                         .WithConfig("NLog.config"));

        }
    }
}