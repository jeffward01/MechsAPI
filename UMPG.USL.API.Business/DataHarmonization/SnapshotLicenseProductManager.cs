using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amazon.Runtime.Internal;
using NLog;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotLicenseProductManager : ISnapshotLicenseProductManager
    {
        private readonly ISnapshotLicenseProductRepository _snapshotLicenseProductRepository;
        private readonly ISnapshotWorksRecordingRepository _snapshotWorksRecordingRepository;
        private readonly ISnapshotRecsConfigurationRepository _snapshotRecsConfigurationRepository;
        private readonly ISnapshotProductHeaderRepository _snapshotProductHeaderRepository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public SnapshotLicenseProductManager(ISnapshotLicenseProductRepository snapshotLicenseProductRepository, ISnapshotWorksRecordingRepository snapshotWorksRecordingRepository, ISnapshotRecsConfigurationRepository snapshotRecsConfigurationRepository, ISnapshotProductHeaderRepository snapshotProductHeaderRepository)
        {
            _snapshotProductHeaderRepository = snapshotProductHeaderRepository;
            _snapshotRecsConfigurationRepository = snapshotRecsConfigurationRepository;
            _snapshotWorksRecordingRepository = snapshotWorksRecordingRepository;
            _snapshotLicenseProductRepository = snapshotLicenseProductRepository;
        }

        public Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct snapshotLicenseProduct)
        {

            var worksRecordings = snapshotLicenseProduct.Recordings;
            var recsConfiguratons = snapshotLicenseProduct.ProductConfigurations;
            var productHeader = snapshotLicenseProduct.ProductHeader;

            snapshotLicenseProduct.ProductHeader = null;
            snapshotLicenseProduct.Recordings = null;
            snapshotLicenseProduct.ProductConfigurations = null;

            if (productHeader != null)
            {
                Logger.Info("ArtistRecsId: " + productHeader.ArtistRecsId);
                _snapshotProductHeaderRepository.SaveSnapshotProductHeader(productHeader);
            }


            //Save works recordings
            if (worksRecordings != null)
            {
                foreach (var workRec in worksRecordings)
                {
                    if (workRec != null)
                    {
                        _snapshotWorksRecordingRepository.SaveSnapshotWorksRecording(workRec);
                    }
                }
            }

            //save recs config
           
            if (recsConfiguratons != null)
            {
                Logger.Info("Number of Recs Config should be 2 === " + recsConfiguratons.Count);
                foreach (var recConfig in recsConfiguratons)
                {
                    if (recConfig != null)
                    {
                        _snapshotRecsConfigurationRepository.SaveSnapshotRecsConfiguration(recConfig);
                    }
                }
            }

            return _snapshotLicenseProductRepository.SaveSnapshotLicenseProduct(snapshotLicenseProduct);
        }
        public Snapshot_LicenseProduct GetSnapshotLicenseProductByLicenseProductId(int snapshotLicenseProductId)
        {
            return _snapshotLicenseProductRepository.GetLicenseProductSnapShotById(snapshotLicenseProductId);
        }
    }
}
