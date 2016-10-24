using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotLabelManger : ISnapshotLabelManger
    {
        private readonly ISnapshotLabelRepository _snapshotLabelRepository;

        public SnapshotLabelManger(ISnapshotLabelRepository snapshotLabelRepository)
        {
            _snapshotLabelRepository = snapshotLabelRepository;
        }

        public Snapshot_Label SaveSnapshotLabel(Snapshot_Label snapshotLabel)
        {
            return _snapshotLabelRepository.SaveSnapshotLabel(snapshotLabel);
        }
        public Snapshot_Label GetSnapshotLabelByLabelId(int snapshotLabelId)
        {
            return _snapshotLabelRepository.GetSnapshotLabelByLabelId(snapshotLabelId);
        }
    }
}
