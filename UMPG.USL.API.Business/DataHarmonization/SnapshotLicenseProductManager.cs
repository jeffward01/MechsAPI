using NLog;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotLicenseProductManager : ISnapshotLicenseProductManager
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();
        private readonly ISnapshotLicenseRepository _snapshotLicenseRepository;
        private readonly ISnapshotLicenseProductRepository _snapshotLicenseProductRepository;
        private readonly ISnapshotLicenseNoteRepository _snapshotLicenseNoteRepository;
        private readonly ISnapshotContactRepository _snapshotContactRepository;
        private readonly ISnapshotRoleRepository _snapshotRoleRepository;
        private readonly ISnapshotAddressRepository _snapshotAddressRepository;
        private readonly ISnapshotPhoneRepository _snapshotPhoneRepository;
        private readonly ISnapshotContactEmailRepository _snapshotContactEmailRepository;
        private readonly ISnapshotWorksRecordingRepository _snapshotWorksRecordingRepository;
        private readonly ISnapshotRecsConfigurationRepository _snapshotRecsConfigurationRepository;
        private readonly ISnapshotProductHeaderRepository _snapshotProductHeaderRepository;
        private readonly ISnapshotConfigurationRepository _snapshotConfigurationRepository;
        private readonly ISnapshotArtistRecsRepository _snapshotArtistRecsRepository;
        private readonly ISnapshotLabelRepository _snapshotLabelRepository;
        private readonly ISnapshotLabelGroupRepository _snapshotLabelGroupRepository;
        private readonly ISnapshotLicenseeLabelGroupRepository _licenseeLabelGroupRepository;
        private readonly ISnapshotLicenseProductConfigurationRepository _licenseProductConfigurationRepository;
        private readonly ISnapshotWorkTrackRepository _snapshotWorkTrackRepository;
        private readonly ISnapshotLicenseProductRecordingRepository _snapshotLicenseProductRecordingRepository;
        private readonly ISnapshotWorksWriterRepository _snapshotWorksWriterRepository;
        private readonly ISnapshotAffiliationRepository _snapshotAffiliationRepository;
        private readonly ISnapshotKnownAsRepository _snapshotKnownAsRepository;
        private readonly ISnapshotOriginalPublisherRepository _snapshotOriginalPublisherRepository;
        private readonly ISnapshotRecsCopyrightRespository _snapshotRecsCopyrightRespository;
        private readonly ISnapshotSampleRepository _snapshotSampleRepository;
        private readonly ISnapshotLocalClientCopyrightRepository _snapshotLocalClientCopyrightRepository;
        private readonly ISnapshotAquisitionLocationCodeRepository _aquisitionLocationCodeRepository;

        public SnapshotLicenseProductManager(ISnapshotLicenseRepository snapshotLicenseRepository,
            ISnapshotAquisitionLocationCodeRepository aquisitionLocationCodeRepository,
            ISnapshotRecsCopyrightRespository snapshotRecsCopyrightRespository,
            ISnapshotSampleRepository snapshotSampleRepository,
            ISnapshotOriginalPublisherRepository snapshotOriginalPublisherRepository,
            ISnapshotKnownAsRepository snapshotKnownAsRepository,
            ISnapshotAffiliationRepository snapshotAffiliationRepository,
            ISnapshotWorksWriterRepository snapshotWorksWriterRepository,
            ISnapshotLicenseProductRecordingRepository snapshotLicenseProductRecordingRepository,
            ISnapshotWorkTrackRepository snapshotWorkTrackRepository,
            ISnapshotLicenseProductConfigurationRepository licenseProductConfigurationRepository,
            ISnapshotLicenseProductRepository snapshotLicenseProductRepository,
            ISnapshotLicenseNoteRepository snapshotLicenseNoteRepository,
            ISnapshotContactRepository snapshotContactRepository, ISnapshotRoleRepository snapshotRoleRepository,
            ISnapshotAddressRepository snapshotAddressRepository, ISnapshotPhoneRepository snapshotPhoneRepository,
            ISnapshotContactEmailRepository snapshotContactEmailRepository,
            ISnapshotWorksRecordingRepository snapshotWorksRecordingRepository,
            ISnapshotRecsConfigurationRepository snapshotRecsConfigurationRepository,
            ISnapshotProductHeaderRepository snapshotProductHeaderRepository,
            ISnapshotConfigurationRepository snapshotConfigurationRepository,
            ISnapshotArtistRecsRepository snapshotArtistRecsRepository, ISnapshotLabelRepository snapshotLabelRepository,
            ISnapshotLabelGroupRepository snapshotLabelGroupRepository,
            ISnapshotLocalClientCopyrightRepository snapshotLocalClientCopyrightRepository,
            ISnapshotLicenseeLabelGroupRepository snalshotLabelGroupRepository)
        {
            _aquisitionLocationCodeRepository = aquisitionLocationCodeRepository;
            _snapshotLocalClientCopyrightRepository = snapshotLocalClientCopyrightRepository;
            _snapshotSampleRepository = snapshotSampleRepository;
            _snapshotRecsCopyrightRespository = snapshotRecsCopyrightRespository;
            _snapshotOriginalPublisherRepository = snapshotOriginalPublisherRepository;
            _snapshotKnownAsRepository = snapshotKnownAsRepository;
            _snapshotAffiliationRepository = snapshotAffiliationRepository;
            _snapshotWorksWriterRepository = snapshotWorksWriterRepository;
            _snapshotLicenseProductRecordingRepository = snapshotLicenseProductRecordingRepository;
            _snapshotWorkTrackRepository = snapshotWorkTrackRepository;
            _licenseProductConfigurationRepository = licenseProductConfigurationRepository;
            _licenseeLabelGroupRepository = snalshotLabelGroupRepository;
            _snapshotLabelGroupRepository = snapshotLabelGroupRepository;
            _snapshotLabelRepository = snapshotLabelRepository;
            _snapshotArtistRecsRepository = snapshotArtistRecsRepository;
            _snapshotConfigurationRepository = snapshotConfigurationRepository;
            _snapshotProductHeaderRepository = snapshotProductHeaderRepository;
            _snapshotRecsConfigurationRepository = snapshotRecsConfigurationRepository;
            _snapshotWorksRecordingRepository = snapshotWorksRecordingRepository;
            _snapshotContactEmailRepository = snapshotContactEmailRepository;
            _snapshotPhoneRepository = snapshotPhoneRepository;
            _snapshotAddressRepository = snapshotAddressRepository;
            _snapshotRoleRepository = snapshotRoleRepository;
            _snapshotContactRepository = snapshotContactRepository;
            _snapshotLicenseNoteRepository = snapshotLicenseNoteRepository;
            _snapshotLicenseProductRepository = snapshotLicenseProductRepository;
            _snapshotLicenseRepository = snapshotLicenseRepository;
        }

        public Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct snapshotLicenseProduct)
        {
            var worksRecordings = snapshotLicenseProduct.Recordings;
            var recsConfiguratons = snapshotLicenseProduct.ProductConfigurations;
            var productHeader = snapshotLicenseProduct.ProductHeader;

            snapshotLicenseProduct.ProductHeader = null;
            snapshotLicenseProduct.Recordings = null;
            snapshotLicenseProduct.ProductConfigurations = null;

            //Save productHeader
            if (productHeader != null)
            {
                Logger.Info("ArtistRecsId: " + productHeader.ArtistRecsId);

                //save artist
                var artist = productHeader.Artist;
                productHeader.Artist = null;
                _snapshotArtistRecsRepository.SaveSnapshotArtistRecs(artist);

                //save label
                var label = productHeader.Label;
                productHeader.Label = null;

                var labelGroups = label.RecordLabelGroups;
                label.RecordLabelGroups = null;

                _snapshotLabelRepository.SaveSnapshotLabel(label);

                foreach (var labelGroup in labelGroups)
                {    //save labelGroup
                    _snapshotLabelGroupRepository.SaveSnapshotLabelGroup(labelGroup);
                }

                var recsConfig = productHeader.Configurations;
                productHeader.Configurations = null;
                if (recsConfig != null)
                {
                    foreach (var config in recsConfig)
                    {
                        //save children
                        var configuration = config.Configuration;
                        config.Configuration = null;
                        if (configuration != null)
                        {
                            _snapshotConfigurationRepository.SaveSnapshotConfiguration(configuration);
                        }

                        


                        //save parent 
                        _snapshotRecsConfigurationRepository.SaveSnapshotRecsConfiguration(config);
                    }
                }

                //Finally.. save productHEader
                _snapshotProductHeaderRepository.SaveSnapshotProductHeader(productHeader);
            }

            //Save works recordings
            if (worksRecordings != null)
            {
                foreach (var workRec in worksRecordings)
                {
                    if (workRec != null)
                    {
                        //save track
                        var track = workRec.Track;
                        workRec.Track = null;
                        if (track != null)
                        {
                            _snapshotWorkTrackRepository.SaveWorksTrack(track);
                        }

                        //save lpr
                        var lpr = workRec.LicenseRecording;
                        workRec.LicenseRecording = null;
                        if (lpr != null)
                        {
                            _snapshotLicenseProductRecordingRepository.SaveLicenseProductRecording(lpr);
                        }

                        //save writer list
                        var writerList = workRec.Writers;
                        workRec.Writers = null;

                        if (writerList != null)
                        {
                            foreach (var writer in writerList)
                            {
                                //save affiliation
                                var affiliations = writer.Affiliation;
                                writer.Affiliation = null;

                                if (affiliations != null)
                                {
                                    foreach (var affilation in affiliations)
                                    {
                                        _snapshotAffiliationRepository.SaveSnapshotAffiliation(affilation);

                                        //save affiliaton Base (not implemented)
                                        //var affiliationBase .....  save...
                                    }
                                }
                                //save knownAs
                                var knownAs = writer.KnownAs;
                                writer.KnownAs = null;
                                if (knownAs != null)
                                {
                                    foreach (var knwn in knownAs)
                                    {
                                        _snapshotKnownAsRepository.SaveKnownAs(knwn);
                                    }
                                }

                                var originalPubs = writer.OriginalPublishers;
                                writer.OriginalPublishers = null;

                                if (originalPubs != null)
                                {
                                    //save original pub
                                    //save knownAs (not implemented)
                                    foreach (var oPub in originalPubs)
                                    {
                                        _snapshotOriginalPublisherRepository.SaveSnapshotOriginalPublisher(oPub);
                                    }
                                }
                            }
                        }

                        _snapshotWorksRecordingRepository.SaveSnapshotWorksRecording(workRec);
                    }
                }
            }

            //save recs config, usually null
            if (recsConfiguratons != null)
            {
                Logger.Info("Number of Recs Config should be 2 === " + recsConfiguratons.Count);
                foreach (var recConfig in recsConfiguratons)
                {
                    if (recConfig != null)
                    {
                        var config = recConfig.Configuration;
                        recConfig.Configuration = null;

                        var lprConfig = recConfig.LicenseProductConfiguration;
                        recConfig.LicenseProductConfiguration = null;

                        if (config != null)
                        {
                            _snapshotConfigurationRepository.SaveSnapshotConfiguration(config);
                        }

                        

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