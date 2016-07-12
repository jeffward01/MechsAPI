using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Castle.Core.Logging;
using UMPG.USLAPI;

namespace UMPG.USL.API.Filters
{
    public class ExceptionFilter : ExceptionFilterAttribute 
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Startup.Container.Resolve<ILogger>().Error(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);
        }
    }
}