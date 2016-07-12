using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Data.LicenseData
{
    public interface ILicenseSequenceRepository
    {
        LicenseSequence Get();
        void Update(LicenseSequence value);
    }
}
