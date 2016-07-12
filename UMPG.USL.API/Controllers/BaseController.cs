using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Castle.Core.Logging;
using UMPG.USL.API.Business;

namespace UMPG.USL.API.Controllers
{
   public class BaseController:ApiController
   {
       public List<int> UpdateLicensesList { get; set; }
   }
}