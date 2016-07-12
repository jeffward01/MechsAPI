using System.Collections.Generic;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Data.LookupData
{
    public interface IAgreementStatutoryRateRepository
    {
        AgreementStatutoryRate Get(int? year);

        List<AgreementStatutoryRate> GetAll();
    }
}
