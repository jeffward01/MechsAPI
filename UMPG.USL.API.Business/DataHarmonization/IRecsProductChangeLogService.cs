using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public interface IRecsProductChangeLogService
    {
        List<RecsProductChanges> ProductAddedToRecs(List<LicenseProduct> licenseProducts, List<int> productIdsAdded);
        List<RecsProductChanges> ProductRemovedFromRecs(List<Snapshot_LicenseProduct> licenseProducts, List<int> productIdsAdded);

        List<RecsProductChanges> RecordingAddedToRecs(List<WorksRecording> licenseRecordings,
            List<int> RecordingIdsAdded);

        List<RecsProductChanges> TracksRemovedFromRecs(List<Snapshot_WorksTrack> licenseRecordings,
            List<int> RecordingIdsAdded);

        List<RecsProductChanges> TracksAddedToRecs(List<WorksTrack> licenseRecordings,
    List<int> RecordingIdsAdded);

        List<RecsProductChanges> RecordingRemovedFromRecs(List<Snapshot_WorksRecording> licenseRecordings,
            List<int> RecordingIdsAdded);

        List<RecsProductChanges> ConfigurationAddedToRecs(List<RecsConfiguration> recsConfigurations,
            List<int> RecordingIdsAdded);

        List<RecsProductChanges> ConfigurationRemovedFromRecs(List<Snapshot_RecsConfiguration> recsConfigurations,
            List<int> RecordingIdsAdded);

        List<RecsProductChanges> WritersAddedToRecs(List<WorksWriter> licenseRecordings, List<int> writerCaeCodesAdded, Snapshot_WorksRecording snapshotWorksRecording);

        List<RecsProductChanges> WritersRemovedFromRecs(List<Snapshot_WorksWriter> licenseRecordings,
            List<int> writerCaeCodesAdded, Snapshot_WorksRecording snapshotWorksRecording);

        List<RecsProductChanges> OriginalPublishersAddedToRecs(List<OriginalPublisher> licenseRecordings,
            List<string> originalPublisherIpCodes, Snapshot_WorksRecording snapshotWorksRecording);

        List<RecsProductChanges> OriginalPublishersRemovedFromRecs(List<Snapshot_OriginalPublisher> licenseRecordings,
            List<string> originalPublisherIpCodes, Snapshot_WorksRecording snapshotWorksRecording);

        List<RecsProductChanges> OriginalPublisherAffiliationAddedToRecs(List<Affiliation> licenseRecordings,
            List<string> originalPublisherIpCodes);

        List<RecsProductChanges> OriginalPublisherAffiliationRemovedFromRecs(
            List<Snapshot_OriginalPublisherAffiliation> licenseRecordings, List<string> originalPublisherIpCodes);

        List<RecsProductChanges> AffiliationBaseRemovedFromRecs(List<Snapshot_OriginalPubAffiliationBase> licenseProducts,
            List<string> productIdsAdded);

        List<RecsProductChanges> AffiliationBaseAddedToRecs(List<AffiliationBase> licenseProducts,
            List<string> productIdsAdded);

        List<RecsProductChanges> CopyrightsRemovedFromRecs(List<Snapshot_RecsCopyright> licenseRecordings,
            List<string> originalPublisherIpCodes, Snapshot_ProductHeader productHeader, Snapshot_WorksTrack snapshotTrack);

        List<RecsProductChanges> CopyrightsAddedToRecs(List<RecsCopyrights> licenseRecordings,
            List<string> originalPublisherIpCodes, Snapshot_ProductHeader productHeader, Snapshot_WorksTrack snapshotTrack);

        List<RecsProductChanges> ComposersAddedToRecs(List<WorksWriter> licenseRecordings,
            List<string> originalPublisherIpCodes);

        List<RecsProductChanges> ComposerRemovedFromRecs(List<Snapshot_Composer> licenseRecordings,
            List<string> originalPublisherIpCodes);

        List<RecsProductChanges> OriginalPublisherAffiliationRemovedFromRecs(
            List<Snapshot_ComposerOriginalPublisherAffiliation> licenseRecordings, List<string> originalPublisherIpCodes);

        List<RecsProductChanges> AffiliationBaseRemovedFromRecs(
            List<Snapshot_ComposerOriginalPublisherAffiliationBase> licenseProducts, List<string> productIdsAdded);

        List<RecsProductChanges> OriginalPublishersRemovedFromRecs(
            List<Snapshot_ComposerOriginalPublisher> licenseRecordings, List<string> originalPublisherIpCodes);

        List<RecsProductChanges> LocalClientAddedToRecs(List<LocalClientCopyright> licenseRecordings,
            List<string> originalPublisherIpCodes);

        List<RecsProductChanges> LocalClientRemovedFromRecs(List<Snapshot_LocalClientCopyright> licenseRecordings,
            List<string> originalPublisherIpCodes);

        List<RecsProductChanges> LocationCodeAddedToRecs(List<string> licenseRecordings,
            List<string> originalPublisherIpCodes);

        List<RecsProductChanges> LocationCodeRemovedFromRecs(List<Snapshot_AquisitionLocationCode> licenseRecordings,
            List<string> originalPublisherIpCodes);
    }
}