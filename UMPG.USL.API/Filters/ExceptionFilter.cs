using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using Castle.Core.Logging;
using UMPG.USLAPI;

namespace UMPG.USL.API.Filters
{
    //Depreciated | Nlog handles this now
    public class ExceptionFilter : ExceptionFilterAttribute 
    {

    //    //Currently not working, depreciated Windsor logging.  We use NLog now
    //    public override void OnException(HttpActionExecutedContext actionExecutedContext)

    //    {
    //        var logger = Startup.Container.Resolve<ILogger>();
    //        logger.Error(actionExecutedContext.Exception.Message, actionExecutedContext.Exception);
    //        if (actionExecutedContext.Exception != null && actionExecutedContext.Exception.InnerException!=null && !string.IsNullOrEmpty(actionExecutedContext.Exception.InnerException.Message))
    //        {
    //            logger.Error(actionExecutedContext.Exception.InnerException.Message, actionExecutedContext.Exception.InnerException);
    //        }
    //    }
    }
}