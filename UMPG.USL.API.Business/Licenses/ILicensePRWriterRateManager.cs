using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.Licenses
{
    public interface ILicensePRWriterRateManager
    {
        LicenseProductRecordingWriterRate Add(LicenseProductRecordingWriterRate license);

        LicenseProductRecordingWriterRate Get(int id);

        List<LicenseProductRecordingWriterRate> GetAll();

        List<LicenseProductRecordingWriterRate> Search(string query);
        List<LicenseProductRecordingWriterRate> GetEditWriterRates(GetWritersRatesRequest request);
    

    }
}
