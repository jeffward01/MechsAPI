using System;
using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface ISnapshotManager
    {
        bool DoesLicenseSnapshotExist(int licenseId);
        Snapshot_ProductHeader SnapshotProductHeader(ProductHeader productHeaderToBeSnapshotted);
        bool TakeLicenseSnapshotLite(License licenseToBeSnapshotted, bool snapshotComplete);
        Snapshot_LicenseProduct TakeLicenseProductSnapshotLite(LicenseProduct licenseProductToBeSnapshotted);
        Snapshot_License GeLicenseSnapshotByLicenseId(int licenseId);
        bool TakeLicenseSnapshot(License licenseToBeSnapshotted, List<LicenseProduct> licenseProducts);
        bool DoesLicenseSnapshotExistAndComplete(int licenseId);
        bool DeleteLicenseSnapshot(int licenseSnapshotId);

        bool TakeWorksRecordingSnapshot(List<WorksRecording> worksRecordings, int productId, int licenseProductId,
            int createdBy, DateTime createdDate);

        void LinkProductConfigurationToProductHeader(LicenseProductConfiguration licenseProductConfiguration,
            int productHeaderSnapshotId);

        Snapshot_ProductHeader GetSnapshotProductHeaderByLicenseId(int licenseId);

        void DeleteRecsConfigAndChildrenForProductHeader(Snapshot_ProductHeader productHeader,
            int cloneRecsConfigurationId);

        void UpdateProductHeaderSnapshot(ProductHeader newproductHeader, UpdateLicenseProductConfigurationRequest request);
        Snapshot_License GetLicenseSnapshotFull(int licenseId);
    }
}