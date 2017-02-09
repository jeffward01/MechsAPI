using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotLicenseProductManager
    {
        Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct snapshotLicenseProduct);
        Snapshot_ProductHeader GetProductForTrackId(int snapshotTrackId);
        Snapshot_LicenseProduct GetSnapshotLicenseProductBySnapshotLicenseProductId(int snapshotLicenseProductId);
        int GetProductHeaderIdForSnapshotLicenseProductId(int id);
        Snapshot_ProductHeader SaveProductHeaderSnapshot(Snapshot_ProductHeader productHeader);
        Snapshot_ProductHeader GetSnapshotProductHeaderByLicenseId(int licenseId);
        bool SaveSnapshotWorksRecording(List<Snapshot_WorksRecording> worksRecordings,
            int licenseProductId);

        Snapshot_LicenseProduct GetSnapshotLicenseProductByLicenseProductId(int licenseProductId);

        void DeleteLicenseProductAndChildEntities(Snapshot_License license, int snapshotLicenseProductId);
    }
}