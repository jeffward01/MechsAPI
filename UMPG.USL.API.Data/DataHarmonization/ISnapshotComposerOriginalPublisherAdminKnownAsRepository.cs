using System.Collections.Generic;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Data.DataHarmonization
{
    public interface ISnapshotComposerOriginalPublisherAdminKnownAsRepository
    {
        Snapshot_ComposerOriginalPublisherAdminKnownAs SaveSnapshotComposerOriginalPublisherAdminKnownAs(Snapshot_ComposerOriginalPublisherAdminKnownAs sampleSnapshot);
        List<Snapshot_ComposerOriginalPublisherAdminKnownAs> GetAllComposerOriginalPublisherAdminKnownAsForAdministratorId(int adminAffiliationId);
        bool DeleteSnapshotComposerOriginalPublisherAdminKnownAs(Snapshot_ComposerOriginalPublisherAdminKnownAs composerToDelete);
    }
}