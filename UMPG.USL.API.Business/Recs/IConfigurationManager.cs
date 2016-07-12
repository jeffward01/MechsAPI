using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Recs
{
    public interface IConfigurationManager
    {
        List<RecsConfigurations> GetConfigurations();
    }
}
