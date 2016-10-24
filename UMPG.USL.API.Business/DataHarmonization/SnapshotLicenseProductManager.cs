using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotLicenseProductManager : ISnapshotLicenseProductManager
    {
        private readonly ISnapshotLicenseProductRepository _snapshotLicenseProductRepository;

        public SnapshotLicenseProductManager(ISnapshotLicenseProductRepository snapshotLicenseProductRepository)
        {
            _snapshotLicenseProductRepository = snapshotLicenseProductRepository;
        }

        public Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct snapshotLicenseProduct)
        {
            return _snapshotLicenseProductRepository.SaveSnapshotLicenseProduct(snapshotLicenseProduct);
        }
        public Snapshot_LicenseProduct GetSnapshotLicenseProductByLicenseProductId(int snapshotLicenseProductId)
        {
            return _snapshotLicenseProductRepository.GetLicenseProductSnapShotById(snapshotLicenseProductId);
        }
    }
}
