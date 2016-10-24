using UMPG.USL.Models.LicenseModel;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface IDataHarmonizationManager
    {
        bool TakeLicenseSnapshot(License licenseToBeSnapshotted);
    }
}