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
        private readonly ISnapshotWorksRecordingRepository _snapshotWorksRecordingRepository;
        private readonly ISnapshotRecsConfigurationRepository _snapshotRecsConfigurationRepository;

        public SnapshotLicenseProductManager(ISnapshotLicenseProductRepository snapshotLicenseProductRepository, ISnapshotWorksRecordingRepository snapshotWorksRecordingRepository, ISnapshotRecsConfigurationRepository snapshotRecsConfigurationRepository)
        {
            _snapshotRecsConfigurationRepository = snapshotRecsConfigurationRepository;
            _snapshotWorksRecordingRepository = snapshotWorksRecordingRepository;
            _snapshotLicenseProductRepository = snapshotLicenseProductRepository;
        }

        public Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct snapshotLicenseProduct)
        {

            var worksRecordings = snapshotLicenseProduct.Recordings;
            var recsConfiguratons = snapshotLicenseProduct.ProductConfigurations;


            //foreach (var workRec in worksRecordings)
            //{
            //    _snapshotWorksRecordingRepository.SaveSnapshotWorksRecording(workRec);
            //}

            //foreach (var recConfig in recsConfiguratons)
            //{
            //    _snapshotRecsConfigurationRepository.SaveSnapshotRecsConfiguration(recConfig);
            //}

            return _snapshotLicenseProductRepository.SaveSnapshotLicenseProduct(snapshotLicenseProduct);
        }
        public Snapshot_LicenseProduct GetSnapshotLicenseProductByLicenseProductId(int snapshotLicenseProductId)
        {
            return _snapshotLicenseProductRepository.GetLicenseProductSnapShotById(snapshotLicenseProductId);
        }
    }
}
