using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotLabelGroupManager : ISnapshotLabelGroupManager
    {
        private readonly ISnapshotLabelGroupRepository _snapshotLabelGroupRepository;

        public SnapshotLabelGroupManager(ISnapshotLabelGroupRepository snapshotLabelGroupRepository)
        {
            _snapshotLabelGroupRepository = snapshotLabelGroupRepository;
        }

        public Snapshot_LabelGroup SaveLabelGroupSnapshotLabelGroup(Snapshot_LabelGroup snapshotLabelGroup)
        {
            return _snapshotLabelGroupRepository.SaveSnapshotLabelGroup(snapshotLabelGroup);
        }

        public Snapshot_LabelGroup GetSnapshotLabelGroupByLabelGroupId(int labelGroupId)
        {
            return _snapshotLabelGroupRepository.GetSaSnapshotLabelGroupByLabelGroupId(labelGroupId);
        }
    }
}