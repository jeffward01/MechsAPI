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

        List<RecsProductChanges> RecordingRemovedFromRecs(List<Snapshot_WorksRecording> licenseRecordings,
            List<int> RecordingIdsAdded);

        List<RecsProductChanges> ConfigurationAddedToRecs(List<RecsConfiguration> recsConfigurations,
            List<int> RecordingIdsAdded);

        List<RecsProductChanges> ConfigurationRemovedFromRecs(List<Snapshot_RecsConfiguration> recsConfigurations,
            List<int> RecordingIdsAdded);

        List<RecsProductChanges> WritersAddedToRecs(List<WorksWriter> licenseRecordings, List<int> writerCaeCodesAdded);

        List<RecsProductChanges> WritersRemovedFromRecs(List<Snapshot_WorksWriter> licenseRecordings,
            List<int> writerCaeCodesAdded);

        List<RecsProductChanges> OriginalPublishersAddedToRecs(List<OriginalPublisher> licenseRecordings,
            List<string> originalPublisherIpCodes);

        List<RecsProductChanges> OriginalPublishersRemovedFromRecs(List<Snapshot_OriginalPublisher> licenseRecordings,
            List<string> originalPublisherIpCodes);

        List<RecsProductChanges> OriginalPublisherAffiliationAddedToRecs(List<Affiliation> licenseRecordings,
            List<string> originalPublisherIpCodes);

        List<RecsProductChanges> OriginalPublisherAffiliationRemovedFromRecs(
            List<Snapshot_OriginalPublisherAffiliation> licenseRecordings, List<string> originalPublisherIpCodes);
    }
}