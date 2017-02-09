using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly ISnapshotWorksRecordingRepository _snapshotWorksRecordingRepository;
        private readonly ISnapshotRecsConfigurationRepository _snapshotRecsConfigurationRepository;
        private readonly ISnapshotProductHeaderRepository _snapshotProductHeaderRepository;
        private readonly ISnapshotConfigurationRepository _snapshotConfigurationRepository;
        private readonly ISnapshotArtistRecsRepository _snapshotArtistRecsRepository;
        private readonly ISnapshotLabelRepository _snapshotLabelRepository;
        private readonly ISnapshotLabelGroupRepository _snapshotLabelGroupRepository;

        private readonly ISnapshotWorkTrackRepository _snapshotWorkTrackRepository;
        private readonly ISnapshotWorksWriterRepository _snapshotWorksWriterRepository;
        private readonly ISnapshotAffiliationRepository _snapshotAffiliationRepository;
        private readonly ISnapshotKnownAsRepository _snapshotKnownAsRepository;
        private readonly ISnapshotOriginalPublisherRepository _snapshotOriginalPublisherRepository;
        private readonly ISnapshotRecsCopyrightRespository _snapshotRecsCopyrightRespository;

        private readonly ISnapshotLocalClientCopyrightRepository _snapshotLocalClientCopyrightRepository;
        private readonly ISnapshotAquisitionLocationCodeRepository _aquisitionLocationCodeRepository;
        private readonly ISnapshotAdministratorRepository _snapshotAdministratorRepository;
        private readonly ISnapshotAdminAffiliationRepository _snapshotAdminAffiliationRepository;
        private readonly ISnapshotAdminAffiliationBaseRepository _adminAffiliationBaseRepository;
        private readonly ISnapshotAdminKnownAsRepository _adminKnownAsRepository;
        private readonly ISnapshotAffiliationBaseRepository _affiliationBaseRepository;
        private readonly ISnapshotOriginalPublisherAffiliationRepository _originalPublisherAffiliationRepository;
        private readonly ISnapshotOriginalPubAffiliationBaseRepository _originalPubAffiliationBaseRepository;

        private readonly ISnapshotComposerRepository _snapshotComposerRepository;
        private readonly ISnapshotComposerAffiliationRepository _composerAffiliationRepository;
        private readonly ISnapshotComposerAffiliationBaseRepository _composerAffiliationBaseRepository;
        private readonly ISnapshot_ComposerKnownAsRepository _composerKnownAsRepository;
        private readonly ISnapshotComposerOriginalPublisherAffiliationBaseRepository _composerOriginalPublisherAffiliationBaseRepository;
        private readonly ISnapshot_ComposerOriginalPublisherAffiliationRepository _composerOriginalPublisherAffiliationRepository;
        private readonly ISnapshot_ComposerOriginalPublisherRepository _composerOriginalPublisherRepository;
        private readonly ISnapshot_ComposerOriginalPublisherKnownAsRepository _composerOriginalPublisherKnownAsRepository;
        private readonly ISnapshotSampleRepository _snapshotSampleRepository;
        private readonly ISnapshotSampleAquisitionLocationCodeRepository _snapshotSampleAquisitionLocationCodeRepository;
        private readonly ISnapshotSampleLocalClientCopyrightRepository _snapshotSampleLocalClientCopyrightRepository;
        private readonly ISnapshotComposerOriginalPublisherAdministratorRepository _composerOriginalPublisherAdministratorRepository;
        private readonly ISnapshotComposerOriginalPublisherAdminAffiliationRepository _composerOriginalPublisherAdminAffiliation;
        private readonly ISnapshotComposerOriginalPublisherAdminAffiliationBaseRepository _composerOriginalPublisherAdminAffiliationBaseRepository;
        private readonly ISnapshotComposerOriginalPublisherAdminKnownAsRepository _composerOriginalPublisherAdminKnownAsRepository;
        
        public SnapshotLicenseProductManager(ISnapshotLicenseRepository snapshotLicenseRepository,

            ISnapshotComposerOriginalPublisherAdminKnownAsRepository composerOriginalPublisherAdminKnownAsRepository,
        ISnapshotComposerRepository snapshotComposerRepository,
        ISnapshotComposerAffiliationRepository composerAffiliationRepository,
        ISnapshotComposerAffiliationBaseRepository composerAffiliationBaseRepository,
        ISnapshot_ComposerKnownAsRepository composerKnownAsRepository,
        ISnapshotComposerOriginalPublisherAffiliationBaseRepository composerOriginalPublisherAffiliationBaseRepository,
        ISnapshot_ComposerOriginalPublisherAffiliationRepository composerOriginalPublisherAffiliationRepository,
        ISnapshot_ComposerOriginalPublisherRepository composerOriginalPublisherRepository,
        ISnapshot_ComposerOriginalPublisherKnownAsRepository composerOriginalPublisherKnownAsRepository,
        ISnapshotSampleRepository snapshotSampleRepository,
        ISnapshotSampleAquisitionLocationCodeRepository snapshotSampleAquisitionLocationCodeRepository,
        ISnapshotSampleLocalClientCopyrightRepository snapshotSampleLocalClientCopyrightRepository,
        ISnapshotComposerOriginalPublisherAdministratorRepository composerOriginalPublisherAdministratorRepository,
        ISnapshotComposerOriginalPublisherAdminAffiliationRepository composerOriginalPublisherAdminAffiliationRepository,
        ISnapshotComposerOriginalPublisherAdminAffiliationBaseRepository composerOriginalPublisherAdminAffiliationBaseRepository,

        ISnapshotOriginalPubAffiliationBaseRepository snapshotOriginalPubAffiliationBaseRepository,
            ISnapshotOriginalPublisherAffiliationRepository snapshotOriginalPublisherAffiliationRepository,
            ISnapshotAffiliationBaseRepository affiliationBaseRepository,
            ISnapshotAdminKnownAsRepository snapshotAdminKnownAsRepository,
            ISnapshotAdminAffiliationBaseRepository snapshotAdminAffiliationBaseRepository,
            ISnapshotAdminAffiliationRepository snapshotAdminAffiliationRepository,
            ISnapshotAdministratorRepository snapshotAdministratorRepository,
            ISnapshotAquisitionLocationCodeRepository aquisitionLocationCodeRepository,
            ISnapshotRecsCopyrightRespository snapshotRecsCopyrightRespository,
            ISnapshotOriginalPublisherRepository snapshotOriginalPublisherRepository,
            ISnapshotKnownAsRepository snapshotKnownAsRepository,
            ISnapshotAffiliationRepository snapshotAffiliationRepository,
            ISnapshotWorksWriterRepository snapshotWorksWriterRepository,
            ISnapshotWorkTrackRepository snapshotWorkTrackRepository,
            ISnapshotLicenseProductRepository snapshotLicenseProductRepository,

            ISnapshotWorksRecordingRepository snapshotWorksRecordingRepository,
            ISnapshotRecsConfigurationRepository snapshotRecsConfigurationRepository,
            ISnapshotProductHeaderRepository snapshotProductHeaderRepository,
            ISnapshotConfigurationRepository snapshotConfigurationRepository,
            ISnapshotArtistRecsRepository snapshotArtistRecsRepository, ISnapshotLabelRepository snapshotLabelRepository,
            ISnapshotLabelGroupRepository snapshotLabelGroupRepository,
            ISnapshotLocalClientCopyrightRepository snapshotLocalClientCopyrightRepository
    )
        {
            _composerOriginalPublisherAdminKnownAsRepository = composerOriginalPublisherAdminKnownAsRepository;
            _composerOriginalPublisherAdminAffiliationBaseRepository = composerOriginalPublisherAdminAffiliationBaseRepository;
            _composerOriginalPublisherAdminAffiliation = composerOriginalPublisherAdminAffiliationRepository;
            _composerOriginalPublisherAdministratorRepository = composerOriginalPublisherAdministratorRepository;
            _snapshotSampleRepository = snapshotSampleRepository;
            _snapshotComposerRepository = snapshotComposerRepository;
            _composerAffiliationRepository = composerAffiliationRepository;
            _composerAffiliationBaseRepository = composerAffiliationBaseRepository;
            _composerKnownAsRepository = composerKnownAsRepository;
            _composerOriginalPublisherAffiliationBaseRepository = composerOriginalPublisherAffiliationBaseRepository;
            _composerOriginalPublisherAffiliationRepository = composerOriginalPublisherAffiliationRepository;
            _composerOriginalPublisherRepository = composerOriginalPublisherRepository;
            _composerOriginalPublisherKnownAsRepository = composerOriginalPublisherKnownAsRepository;
            _snapshotSampleAquisitionLocationCodeRepository = snapshotSampleAquisitionLocationCodeRepository;
            _snapshotSampleLocalClientCopyrightRepository = snapshotSampleLocalClientCopyrightRepository;

            _affiliationBaseRepository = affiliationBaseRepository;
            _originalPubAffiliationBaseRepository = snapshotOriginalPubAffiliationBaseRepository;
            _originalPublisherAffiliationRepository = snapshotOriginalPublisherAffiliationRepository;
            _adminKnownAsRepository = snapshotAdminKnownAsRepository;
            _adminAffiliationBaseRepository = snapshotAdminAffiliationBaseRepository;
            _snapshotAdminAffiliationRepository = snapshotAdminAffiliationRepository;
            _snapshotAdministratorRepository = snapshotAdministratorRepository;
            _aquisitionLocationCodeRepository = aquisitionLocationCodeRepository;
            _snapshotLocalClientCopyrightRepository = snapshotLocalClientCopyrightRepository;
            _snapshotRecsCopyrightRespository = snapshotRecsCopyrightRespository;
            _snapshotOriginalPublisherRepository = snapshotOriginalPublisherRepository;
            _snapshotKnownAsRepository = snapshotKnownAsRepository;
            _snapshotAffiliationRepository = snapshotAffiliationRepository;
            _snapshotWorksWriterRepository = snapshotWorksWriterRepository;

            _snapshotWorkTrackRepository = snapshotWorkTrackRepository;

            _snapshotLabelGroupRepository = snapshotLabelGroupRepository;
            _snapshotLabelRepository = snapshotLabelRepository;
            _snapshotArtistRecsRepository = snapshotArtistRecsRepository;
            _snapshotConfigurationRepository = snapshotConfigurationRepository;
            _snapshotProductHeaderRepository = snapshotProductHeaderRepository;
            _snapshotRecsConfigurationRepository = snapshotRecsConfigurationRepository;
            _snapshotWorksRecordingRepository = snapshotWorksRecordingRepository;
            _snapshotLicenseProductRepository = snapshotLicenseProductRepository;
            _snapshotLicenseRepository = snapshotLicenseRepository;
        }

        public Snapshot_ProductHeader GetProductForTrackId(int snapshotTrackId)
        {
            var snapshotTrack = _snapshotWorkTrackRepository.GetTrackBySnapshotWorksTrackId(snapshotTrackId);
            var worksRecording =
                _snapshotWorksRecordingRepository.GetWorksRecordingForSnapshotTrackId(snapshotTrack.SnapshotWorkTrackId);
            var licenseProduct =
                _snapshotLicenseProductRepository.GetLicenseProductByLicenseProductId(worksRecording.LicenseProductId);
          return
                _snapshotProductHeaderRepository.GetProductHeaderByProductHeaderId((int)licenseProduct.ProductHeaderId);
        }


        public Snapshot_ProductHeader GetSnapshotProductHeaderByLicenseId(int licenseId)
        {
            return _snapshotProductHeaderRepository.GetSnapshotProductHeaderByLicenseId(licenseId);
        }

        public Snapshot_ProductHeader SaveProductHeaderSnapshot(Snapshot_ProductHeader productHeader)
        {





       return      SaveProductHeaderLite(productHeader);
       
        }

        private void DeleteSnapshotLicenseProduct(int snapshotLicenseProductId)
        {
            
        }
        public bool SaveSnapshotWorksRecording(List<Snapshot_WorksRecording> worksRecordings,
            int licenseProductId)
        {
            SaveSnapshotWorksRecordingAndChildren(worksRecordings, licenseProductId);
            return true;
        }

        public Snapshot_LicenseProduct SaveSnapshotLicenseProduct(Snapshot_LicenseProduct snapshotLicenseProduct)
        {
            var worksRecordings = snapshotLicenseProduct.Recordings;
            var recsConfiguratons = snapshotLicenseProduct.ProductConfigurations;  
            var productHeader = snapshotLicenseProduct.ProductHeader;

            snapshotLicenseProduct.ProductHeader = null;
            snapshotLicenseProduct.Recordings = null;
            snapshotLicenseProduct.ProductConfigurations = null;  //not used

            //Save productHeader
            if (productHeader != null)
            {
                Logger.Info("ArtistRecsId: " + productHeader.ArtistRecsId);

                //save artist
                var artist = productHeader.Artist;
                productHeader.Artist = null;

                //save label
                var label = productHeader.Label;
                productHeader.Label = null;

                var labelGroups = label.RecordLabelGroups;
                label.RecordLabelGroups = null;

                var recsConfig = productHeader.Configurations;
                productHeader.Configurations = null;

             var savedArtistOnProductHeader =   _snapshotArtistRecsRepository.SaveSnapshotArtistRecs(artist);
                productHeader.SnapshotArtistRecsId = savedArtistOnProductHeader.SnapshotArtistRecsId;

                var savedLabelOnProductHeader =_snapshotLabelRepository.SaveSnapshotLabel(label);
                productHeader.SnapshotLabelId = savedLabelOnProductHeader.SnapshotLabelId;


                //Finally.. save productHEader
                var savedProductHeader = _snapshotProductHeaderRepository.SaveSnapshotProductHeader(productHeader);
                snapshotLicenseProduct.SnapshotProductHeaderId = savedProductHeader.SnapshotProductHeaderId;

                foreach (var labelGroup in labelGroups)
                {    //save labelGroup
                    labelGroup.SnapshotLabelId = savedLabelOnProductHeader.SnapshotLabelId;
                    _snapshotLabelGroupRepository.SaveSnapshotLabelGroup(labelGroup);
                }

                if (recsConfig != null)
                {
                    foreach (var config in recsConfig)
                    {
                        //save children
                        var configuration = config.Configuration;
                        config.Configuration = null;

                        //Assign FK to recsConfig
                        config.SnapshotProductHeaderId = savedProductHeader.SnapshotProductHeaderId;

                        
                        if (configuration != null)
                        {
                            
                            var savedConfigOnRecsConfig = _snapshotConfigurationRepository.SaveSnapshotConfiguration(configuration);
                            config.SnapshotConfigurationId = savedConfigOnRecsConfig.SnapshotConfigId;
                        }
                        //save parent
                        var savedRecsConfigOnProductHeader = _snapshotRecsConfigurationRepository.SaveSnapshotRecsConfiguration(config);
                    }
                }

            }

            var savedLicenseProduct =
            _snapshotLicenseProductRepository.SaveSnapshotLicenseProduct(snapshotLicenseProduct);
            //Save works recordings
            if (worksRecordings != null)
            {
                SaveSnapshotWorksRecordingAndChildren(worksRecordings, savedLicenseProduct.SnapshotLicenseProductId);
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

            return snapshotLicenseProduct;
        }

        private Snapshot_ProductHeader SaveProductHeaderLite(Snapshot_ProductHeader productHeader)
        {
            Logger.Info("ArtistRecsId: " + productHeader.ArtistRecsId);

            //save artist
            var artist = productHeader.Artist;
            productHeader.Artist = null;

            //save label
            var label = productHeader.Label;
            productHeader.Label = null;

            var labelGroups = label.RecordLabelGroups;
            label.RecordLabelGroups = null;

            var recsConfig = productHeader.Configurations;
            productHeader.Configurations = null;

            var savedArtistOnProductHeader = _snapshotArtistRecsRepository.SaveSnapshotArtistRecs(artist);
            productHeader.SnapshotArtistRecsId = savedArtistOnProductHeader.SnapshotArtistRecsId;

            var savedLabelOnProductHeader = _snapshotLabelRepository.SaveSnapshotLabel(label);
            productHeader.SnapshotLabelId = savedLabelOnProductHeader.SnapshotLabelId;


            //Finally.. save productHEader
            var savedProductHeader = _snapshotProductHeaderRepository.SaveSnapshotProductHeader(productHeader);

            foreach (var labelGroup in labelGroups)
            {    //save labelGroup
                labelGroup.SnapshotLabelId = savedLabelOnProductHeader.SnapshotLabelId;
                _snapshotLabelGroupRepository.SaveSnapshotLabelGroup(labelGroup);
            }

            if (recsConfig != null)
            {
                foreach (var config in recsConfig)
                {
                    //save children
                    var configuration = config.Configuration;
                    config.Configuration = null;

                    //Assign FK to recsConfig
                    config.SnapshotProductHeaderId = savedProductHeader.SnapshotProductHeaderId;


                    if (configuration != null)
                    {

                        var savedConfigOnRecsConfig = _snapshotConfigurationRepository.SaveSnapshotConfiguration(configuration);
                        config.SnapshotConfigurationId = savedConfigOnRecsConfig.SnapshotConfigId;
                    }
                    //save parent
                    var savedRecsConfigOnProductHeader = _snapshotRecsConfigurationRepository.SaveSnapshotRecsConfiguration(config);
                }
            }
            return savedProductHeader;
            
        }

        private void SaveSnapshotWorksRecordingAndChildren(List<Snapshot_WorksRecording> worksRecordings, int snapshotLicenseProductId)
        {
            foreach (var workRec in worksRecordings)
            {
                if (workRec != null)
                {
                    //assign FK
                    workRec.SnapshotLicenseProductId = snapshotLicenseProductId;

                    //save track
                    var track = workRec.Track;
                    workRec.Track = null;

                    //set lpr to null (lpr is mechs item)
                    //workRec.LicenseRecording = null;
                    //save writer list
                    var writerList = workRec.Writers;
                    workRec.Writers = null;

                    if (track != null)
                    {
                        //Get all track children and set to null
                        var copyRights = track.Copyrights;
                        track.Copyrights = null;

                        var artist = track.Artist;
                        track.Artist = null;

                        //track.SnapshotWorksRecordingId = savedWorksRecording.SnapshotWorksRecodingId;

                        var savedArtistOnTrack = _snapshotArtistRecsRepository.SaveSnapshotArtistRecs(artist);
                        track.SnapshotArtistRecsId = savedArtistOnTrack.SnapshotArtistRecsId;
                        var savedTrack = _snapshotWorkTrackRepository.SaveWorksTrack(track);

                        workRec.SnapshotWorkTrackId = savedTrack.SnapshotWorkTrackId;
                        //save artist
                        if (copyRights != null)
                        {
                            foreach (var copyRight in copyRights)
                            {
                                //get, and set children to null
                                var copyRightComposers = copyRight.Composers;
                                copyRight.Composers = null;

                                var copyRightSamples = copyRight.Samples;
                                copyRight.Samples = null;

                                var copyRightLocalClients = copyRight.LocalClients;
                                copyRight.LocalClients = null;

                                var copyRightLocationCodes = copyRight.AquisitionLocationCodes;
                                copyRight.AquisitionLocationCodes = null;

                                copyRight.SnapshotWorkTrackId = savedTrack.SnapshotWorkTrackId;

                                //save copyright, get PK.
                                var savedRecsCopyright =
                                    _snapshotRecsCopyrightRespository.SaveSnapshotWorksRecording(copyRight);

                                if (copyRightLocationCodes != null)
                                {
                                    foreach (var copyRightLocationCode in copyRightLocationCodes)
                                    {
                                        copyRightLocationCode.SnapshotRecsCopyrightId =
                                            savedRecsCopyright.SnapshotRecsCopyrightsId;

                                        _aquisitionLocationCodeRepository.SaveAquisitionLocationCode(
                                            copyRightLocationCode);
                                    }
                                }

                                if (copyRightLocalClients != null)
                                {
                                    foreach (var copyRightLocalClient in copyRightLocalClients)
                                    {
                                        copyRightLocalClient.SnapshotRecsCopyrightId =
                                            savedRecsCopyright.SnapshotRecsCopyrightsId;

                                        _snapshotLocalClientCopyrightRepository.SaveLocalClientCopyright(
                                            copyRightLocalClient);
                                    }
                                }

                                if (copyRightSamples != null)
                                {
                                    foreach (var copyRightSample in copyRightSamples)
                                    {
                                        var localClientSamples = copyRightSample.LocalClients;
                                        copyRightSample.LocalClients = null;

                                        var localAquisitionLocaitonCodes = copyRightSample.AquisitionLocationCodes;
                                        copyRightSample.AquisitionLocationCodes = null;

                                        //assign fk
                                        copyRightSample.SnapshotRecsCopyrightId =
                                            savedRecsCopyright.SnapshotRecsCopyrightsId;

                                        //save
                                        var savedSample = _snapshotSampleRepository.SaveSampleSnapshot(copyRightSample);

                                        if (localAquisitionLocaitonCodes != null)
                                        {
                                            foreach (var localAquisitionLocaitonCode in localAquisitionLocaitonCodes)
                                            {
                                                localAquisitionLocaitonCode.SnapshotSampleId =
                                                    savedSample.SnapshotSampleId;

                                                _snapshotSampleAquisitionLocationCodeRepository
                                                    .SaveSampleAquisitionLocationCode(localAquisitionLocaitonCode);
                                            }
                                        }

                                        if (localClientSamples != null)
                                        {
                                            foreach (var localClientSample in localClientSamples)
                                            {
                                                localClientSample.SnapshotSampleId = savedSample.SnapshotSampleId;

                                                _snapshotSampleLocalClientCopyrightRepository
                                                    .SaveSampleLocalClientCopyright(localClientSample);
                                            }
                                        }
                                    }
                                }

                                //create new entities, save them.
                                if (copyRightComposers != null)
                                {
                                    foreach (var copyRightComposer in copyRightComposers)
                                    {
                                        //done
                                        var composerAffiliations = copyRightComposer.Affiliation;
                                        copyRightComposer.Affiliation = null;

                                        //done
                                        var composerKnownAs = copyRightComposer.KnownAs;
                                        copyRightComposer.KnownAs = null;

                                        var composerOriginalPublishers = copyRightComposer.OriginalPublishers;
                                        copyRightComposer.OriginalPublishers = null;

                                        copyRightComposer.SnapshotRecsCopyrightId =
                                            savedRecsCopyright.SnapshotRecsCopyrightsId;

                                        //save composer, get pk
                                        var savedComposer =
                                            _snapshotComposerRepository.SaveComposerSnapshot(copyRightComposer);

                                        if (composerOriginalPublishers != null)
                                        {
                                            foreach (var composerOriginalPublisher in composerOriginalPublishers)
                                            {
                                                //done
                                                var composerOriginalPublisherKnownAs = composerOriginalPublisher.KnownAs;
                                                composerOriginalPublisher.KnownAs = null;

                                                //done
                                                var composerOriginalPublisherAffiliations =
                                                    composerOriginalPublisher.Affiliation;
                                                composerOriginalPublisher.Affiliation = null;

                                                var composerOriginalPubliserAdmins =
                                                    composerOriginalPublisher.Administrator;
                                                composerOriginalPublisher.Administrator = null;

                                                //assign fk
                                                composerOriginalPublisher.SnapshotComposerId =
                                                    savedComposer.SnapshotComposerId;

                                                //save
                                                var savedComposerOriginalPublisher = _composerOriginalPublisherRepository
                                                    .SaveComposerOriginalPublisher(
                                                        composerOriginalPublisher);

                                                if (composerOriginalPublisherKnownAs != null)
                                                {
                                                    foreach (var known in composerOriginalPublisherKnownAs)
                                                    {
                                                        known.SnapshotComposerOriginalPublisherId =
                                                            savedComposerOriginalPublisher
                                                                .SnapshotComposerOriginalPublisherId;

                                                        _composerOriginalPublisherKnownAsRepository
                                                            .SaveComposerOriginalPublisherKnownAs(known);
                                                    }
                                                }

                                                if (composerOriginalPublisherAffiliations != null)
                                                {
                                                    foreach (
                                                        var composerOriginalPublisherAffiliation in
                                                        composerOriginalPublisherAffiliations)
                                                    {
                                                        var composerOpAffiliationBases =
                                                            composerOriginalPublisherAffiliation.Affiliations;
                                                        composerOriginalPublisherAffiliation.Affiliations = null;

                                                        composerOriginalPublisherAffiliation
                                                                .SnapshotComposerOriginalPublisherId =
                                                            savedComposerOriginalPublisher
                                                                .SnapshotComposerOriginalPublisherId;

                                                        var savedComposerOriginalPublisherAffiliation = _composerOriginalPublisherAffiliationRepository
                                                            .SaveComposerOriginalPublisherAffiliation(
                                                                composerOriginalPublisherAffiliation);

                                                        if (composerOpAffiliationBases != null)
                                                        {
                                                            foreach (
                                                                var composerOpAffiliationBase in
                                                                composerOpAffiliationBases)
                                                            {
                                                                composerOpAffiliationBase
                                                                        .SnapshotComposerOriginalPublisherAffiliationId
                                                                    =
                                                                    savedComposerOriginalPublisherAffiliation
                                                                        .SnapshotComposerOriginalPublisherAffiliationId;

                                                                _composerOriginalPublisherAffiliationBaseRepository
                                                                    .SaveComposerOriginalPublisherAffiliationBase(
                                                                        composerOpAffiliationBase);
                                                            }
                                                        }
                                                    }
                                                }

                                                if (composerOriginalPubliserAdmins != null)
                                                {
                                                    foreach (
                                                        var composerOriginalPubliserAdmin in
                                                        composerOriginalPubliserAdmins)
                                                    {
                                                        var composerOpAdminAffiliations =
                                                            composerOriginalPubliserAdmin.Affiliation;
                                                        composerOriginalPubliserAdmin.Affiliation = null;

                                                        var composerOpadminKnownAs =
                                                            composerOriginalPubliserAdmin.KnownAs;
                                                        composerOriginalPubliserAdmin.KnownAs = null;

                                                        //add fk
                                                        composerOriginalPubliserAdmin
                                                                .SnapshotComposerOriginalPublisherId =
                                                            savedComposerOriginalPublisher
                                                                .SnapshotComposerOriginalPublisherId;

                                                        var savedComposerOPadmin = _composerOriginalPublisherAdministratorRepository
                                                            .SaveComposerOriginalPublisherAdministrator(
                                                                composerOriginalPubliserAdmin);

                                                        if (composerOpAdminAffiliations != null)
                                                        {
                                                            foreach (
                                                                var composerOpAdminAffiliation in
                                                                composerOpAdminAffiliations)
                                                            {
                                                                var affiliationBases =
                                                                    composerOpAdminAffiliation.Affiliations;
                                                                composerOpAdminAffiliation.Affiliations = null;

                                                                composerOpAdminAffiliation
                                                                        .SnapshotComposerOriginalPublisherAdministratorId
                                                                    =
                                                                    savedComposerOPadmin
                                                                        .SnapshotComposerOriginalPublisherAdministratorId;

                                                                var savedComposerOPAdminAffiliation = _composerOriginalPublisherAdminAffiliation
                                                                    .SaveComposerOriginalPublisherAdminAffiliation(
                                                                        composerOpAdminAffiliation);

                                                                if (affiliationBases != null)
                                                                {
                                                                    foreach (var affiliationBase in affiliationBases)
                                                                    {
                                                                        //assign FK
                                                                        affiliationBase
                                                                                .SnapshotComposerOriginalPublisherAdminAffiliationId
                                                                            =
                                                                            savedComposerOPAdminAffiliation
                                                                                .SnapshotComposerOriginalPublisherAdminAffiliationId;

                                                                        //save
                                                                        _composerOriginalPublisherAdminAffiliationBaseRepository
                                                                            .SaveComposerOriginalPublisherAdminAffiliationBase
                                                                            (affiliationBase);
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        if (composerOpadminKnownAs != null)
                                                        {
                                                            foreach (var known in composerOpadminKnownAs)
                                                            {
                                                                //assign fk
                                                                known.SnapshotComposerOriginalPublisherAdministratorId =
                                                                    savedComposerOPadmin
                                                                        .SnapshotComposerOriginalPublisherAdministratorId;

                                                                //save
                                                                _composerOriginalPublisherAdminKnownAsRepository
                                                                    .SaveSnapshotComposerOriginalPublisherAdminKnownAs(
                                                                        known);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }

                                        if (composerKnownAs != null)
                                        {
                                            foreach (var knwon in composerKnownAs)
                                            {
                                                //assign fk
                                                knwon.SnapshotComposerId = savedComposer.SnapshotComposerId;

                                                //save known
                                                _composerKnownAsRepository.SaveComposerKnownAs(knwon);
                                            }
                                        }

                                        if (composerAffiliations != null)
                                        {
                                            foreach (var composerAffiliation in composerAffiliations)
                                            {
                                                var composerAffiliationBases = composerAffiliation.Affiliations;
                                                composerAffiliation.Affiliations = null;

                                                //assign fk to composerAffiliation
                                                composerAffiliation.SnapshotComposerId =
                                                    savedComposer.SnapshotComposerId;

                                                //save composer affilliation and get PK
                                                var savedComposedAffiliation =
                                                    _composerAffiliationRepository.SaveComposerAffiliationSnapshot(
                                                        composerAffiliation);

                                                foreach (var composerAffiliationBase in composerAffiliationBases)
                                                {
                                                    //assign fk
                                                    composerAffiliationBase.SnapshotComposerAffiliationId =
                                                        savedComposedAffiliation.SnapshotComposerAffiliationId;
                                                    //save
                                                    _composerAffiliationBaseRepository
                                                        .SaveComposerAffiliatioBasenSnapshot(composerAffiliationBase);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    var savedWorksRecording = _snapshotWorksRecordingRepository.SaveSnapshotWorksRecording(workRec);
                    if (writerList != null)
                    {
                        foreach (var writer in writerList)
                        {
                            var affiliations = writer.Affiliation;
                            writer.Affiliation = null;

                            var knownAs = writer.KnownAs;
                            writer.KnownAs = null;

                            var originalPubs = writer.OriginalPublishers;
                            writer.OriginalPublishers = null;

                            //Set Foriegn Key
                            writer.CloneWorksTrackId = track.CloneWorksTrackId;
                            writer.SnapshotWorksRecordingId = savedWorksRecording.SnapshotWorksRecodingId;
                            //save writer, get key
                            var saveWorksWriter = _snapshotWorksWriterRepository.SaveWorksWriter(writer);
                            //save writer affiliation

                            if (affiliations != null)
                            {
                                foreach (var affilation in affiliations)
                                {
                                    var affiliationBases = affilation.Affiliations;
                                    affilation.Affiliations = null;

                                    affilation.SnapshotWorksWriterId = saveWorksWriter.SnapshotWorksWriterId;
                                    var savedAffiliation = _snapshotAffiliationRepository.SaveSnapshotAffiliation(affilation);

                                    if (affiliationBases != null)
                                    {
                                        foreach (var affiliationBase in affiliationBases)
                                        {
                                            affiliationBase.SnapshotAffiliationId =
                                                savedAffiliation.SnapshotAffiliationId;

                                            _affiliationBaseRepository.SaveSnapshotAffiliationBase(affiliationBase);
                                        }
                                    }
                                }
                            }
                            //save writer knownAs

                            if (knownAs != null)
                            {
                                foreach (var knwn in knownAs)
                                {
                                    _snapshotKnownAsRepository.SaveKnownAs(knwn);
                                }
                            }

                            if (originalPubs != null)
                            {
                                //save original pub
                                //save knownAs (not implemented)
                                foreach (var oPub in originalPubs)
                                {
                                    oPub.SnapshotWorksWriterId = saveWorksWriter.SnapshotWorksWriterId;
                                    var administrators = oPub.Administrator;
                                    oPub.Administrator = null;

                                    var opKnownAs = oPub.KnownAs;
                                    oPub.KnownAs = null;

                                    var opAffiliations = oPub.Affiliation;
                                    oPub.Affiliation = null;

                                    var savedSnapshotOriginalPublisher =
                          _snapshotOriginalPublisherRepository.SaveSnapshotOriginalPublisher(oPub);

                                    if (opKnownAs != null)
                                    {
                                        foreach (var knwn in opKnownAs)
                                        {
                                            _snapshotKnownAsRepository.SaveKnownAs(knwn);
                                        }
                                    }

                                    if (opAffiliations != null)
                                    {
                                        foreach (var opAffiliation in opAffiliations)
                                        {
                                            var affiliationBases = opAffiliation.Affiliations;
                                            opAffiliation.Affiliations = null;

                                            //assign fk
                                            opAffiliation.SnapshotOriginalPublisherId =
                                               savedSnapshotOriginalPublisher.SnapshotOriginalPublisherId;

                                            //save
                                            var savedOriginalPubAffiliation =
                                                _originalPublisherAffiliationRepository
                                                    .SaveSnapshotOriginalPublisherAffiliation(opAffiliation);

                                            if (affiliationBases != null)
                                            {
                                                foreach (var affiliationBase in affiliationBases)
                                                {
                                                    //Assign foriegn key
                                                    affiliationBase.SnapshotOriginalPublisherAffiliationId =
                                                        savedOriginalPubAffiliation
                                                            .SnapshotOriginalPublisherAffiliationId;

                                                    //save
                                                    _originalPubAffiliationBaseRepository
                                                        .SaveSnapshotAdminAffiliation(affiliationBase);
                                                }
                                            }
                                        }
                                    }

                                    if (administrators != null)
                                    {
                                        foreach (var admin in administrators)
                                        {
                                            var adminKnownAs = admin.KnownAs;
                                            admin.KnownAs = null;

                                            var adminAffiliations = admin.Affiliation;
                                            admin.Affiliation = null;

                                            //Assign foriegn Key
                                            admin.SnapshotOriginalPublisherId =
                                                savedSnapshotOriginalPublisher.SnapshotOriginalPublisherId;

                                            var snapshotAdmin =
                                                _snapshotAdministratorRepository.SaveSnapshotAdministrator(admin);
                                            if (adminKnownAs != null)
                                            {
                                                foreach (var knwn in adminKnownAs)
                                                {
                                                    knwn.SnapshotAdministratorId =
                                                        snapshotAdmin.SnapshotAdministratorId;
                                                    _adminKnownAsRepository.SaveSnapshotAdminKnownAs(knwn);
                                                }
                                            }

                                            if (adminAffiliations != null)
                                            {
                                                foreach (var adminAffiliation in adminAffiliations)
                                                {
                                                    var adminAffiliationBases = adminAffiliation.Affiliations;
                                                    adminAffiliation.Affiliations = null;

                                                    //Assign Foriegn Key
                                                    adminAffiliation.SnapshotAdministratorId =
                                                        snapshotAdmin.SnapshotAdministratorId;

                                                    //Save
                                                    var snapshotAdminAffiliation =
                                                        _snapshotAdminAffiliationRepository
                                                            .SaveSnapshotAdminAffiliation
                                                            (
                                                                adminAffiliation);
                                                    if (adminAffiliationBases != null)
                                                    {
                                                        foreach (var adminAffiliationBase in adminAffiliationBases)
                                                        {
                                                            //Assign FK
                                                            adminAffiliationBase.SnapshotAdminAffiliationId =
                                                                snapshotAdminAffiliation.SnapshotAdminAffiliationId;

                                                            _adminAffiliationBaseRepository
                                                                .SaveSnapshotAdministrator(
                                                                    adminAffiliationBase);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public Snapshot_LicenseProduct GetSnapshotLicenseProductBySnapshotLicenseProductId(int snapshotLicenseProductId)
        {
            return _snapshotLicenseProductRepository.GetLicenseProductSnapShotById(snapshotLicenseProductId);
        }

        public Snapshot_LicenseProduct GetSnapshotLicenseProductByLicenseProductId(int licenseProductId)
        {
            return _snapshotLicenseProductRepository.GetSnapshotLicenseProductByLicenseProductId(licenseProductId);
        }


        public int GetProductHeaderIdForSnapshotLicenseProductId(int id)
        {
            return Convert.ToInt32(_snapshotLicenseProductRepository.GetLicenseProductSnapShotByLPSnapId(id).ProductHeaderId);
        }


        public void DeleteLicenseProductAndChildEntities(Snapshot_License license, int snapshotLicenseProductId)
        {
            //getAllLicenseProducts
            var licenseProducts =
                _snapshotLicenseProductRepository.GetAllLicenseProductsForLicenseId(license.CloneLicenseId);

            //Massive delete

            foreach (var licenseProduct in licenseProducts)
            {
                if (licenseProduct.SnapshotLicenseProductId == snapshotLicenseProductId)
                {
                    //Delete All RecsConfiguration and children
                    DeleteSnapshotRecsRecordingandChildren(licenseProduct);

                    //Delete worksRecording and Children
                    DeleteAllWorksRecordingAndChildren(licenseProduct);

                    //Delete Product HEader and children
                    DeleteProductHeaderAndChildren(licenseProduct);

                    //Delete License Product
                    _snapshotLicenseProductRepository.DeleteLicenseProductSnapshot(
                        licenseProduct.SnapshotLicenseProductId);
                }
            }
        }

        private void DeleteProductHeaderAndChildren(Snapshot_LicenseProduct licenseProduct)
        {
            if (licenseProduct.ProductHeaderId != null)
            {
                var id = (int)licenseProduct.ProductHeaderId;

                var productHeader =
                    _snapshotProductHeaderRepository.GetProductHeaderByProductHeaderId(
                        id);

                if (productHeader != null)
                {
                    //Delete artist
                    var artist =
                        _snapshotArtistRecsRepository.GetSnapshotArtistRecsByArtistId(productHeader.ArtistRecsId);
                    if (artist != null)
                    {
                        _snapshotArtistRecsRepository.DeleteRecsArtisByArtistSnapshotId(
                            artist.SnapshotArtistRecsId);
                    }
                    //Delete label and child (label group)
                    DeleteLabelAndAllChuldren(productHeader);

                    //Delete RecsConfigurations 'Configurations'
                    DeleteAllRecsConfigAndChildrenForProductHeader(productHeader);

                    _snapshotProductHeaderRepository.DeleteProductHeaderSnapshotBySnapshotId(
                        productHeader.SnapshotProductHeaderId);
                }
            }
        }

        private void DeleteLabelAndAllChuldren(Snapshot_ProductHeader productHeader)
        {
            if (productHeader.LabelId > 0)
            {
                var label = _snapshotLabelRepository.GetSnapshotLabelByLabelId(productHeader.LabelId);

                //labelGroups
                var labelGroups =
                    _snapshotLabelGroupRepository.GetAllALabelGroupsForLabelId(
                        label.CloneLabelId);
                if (labelGroups != null)
                {
                    foreach (var labelGroup in labelGroups)
                    {
                        _snapshotLabelGroupRepository.DeleteLabelGroupByLabelGroupSnapshotId(
                            labelGroup.SnapshotLabelGroupId);
                    }
                }

                //Delete label
                _snapshotLabelRepository.DeleteLabelSnapshotBySnapshotId(label.SnapshotLabelId);
            }
        }

        private void DeleteAllRecsConfigAndChildrenForProductHeader(Snapshot_ProductHeader productHeader)
        {
            var recConfigs =
                _snapshotRecsConfigurationRepository.GetAllRecsConfigurationsRecordingsForProductHeaderId(
                    productHeader.CloneProductHeaderId);
            if (recConfigs != null)
            {
                foreach (var config in recConfigs)
                {
                    if (config.ConfigurationId != null)
                    {
                        var id = (int)config.ConfigurationId;
                        //Delete Config
                        var config2 =
                            _snapshotConfigurationRepository.GetSnapshotConfigurationByConfigurationId(id);
                        _snapshotConfigurationRepository.DeleteConfigurationSnapshot(config2.SnapshotConfigId);
                    }

                    //Delete LicenseProductConfig if exists
                    if (config.LicenseProductConfigurationId != null)
                    {
                        var id = (int)config.LicenseProductConfigurationId;

                        /* Mechs data, do not delete
                        var licenseProductConfig =
                            _licenseProductConfigurationRepository
                                .GetSnapshotLicenseProductConfigurationByLicenseProductConfigurationId(
                                    id);

                        //Delete licenseProductConfiguration
                        _licenseProductConfigurationRepository.DeleteLicenseProductConfigurationBySnapshot(licenseProductConfig);
                        */
                    }

                    //delete recConfig
                    _snapshotRecsConfigurationRepository.DeleteWorkRecordingByRecordignSnapshotId(
                        config.SnapshotRecsConfigurationId);
                }
            }
        }

        private void DeleteAllWorksRecordingAndChildren(Snapshot_LicenseProduct licenseProduct)
        {
            if (licenseProduct.CloneLicenseProductId != null)
            {
                var workRecordings =
                    _snapshotWorksRecordingRepository.GetAllWorksRecordingsForLicenseProductId(
                        licenseProduct.CloneLicenseProductId);
                if (workRecordings != null)
                {
                    foreach (var recording in workRecordings)
                    {
                        //delete works track
                        var track = _snapshotWorkTrackRepository.GetTrackForCloneTrackId(recording.TrackId);
                        if (track != null)
                        {
                            //get artist and delte
                            var artist =
                                _snapshotArtistRecsRepository.GetSnapshotArtistRecsByArtistId(track.ArtistRecsId);
                            if (artist != null)
                            {
                                _snapshotArtistRecsRepository.DeleteRecsArtisByArtistSnapshotId(
                                    artist.SnapshotArtistRecsId);
                            }

                            //delete copyrights
                            var copyrights =
                                _snapshotRecsCopyrightRespository.GetAllRecsCopyrightsForCloneTrackId(
                                    track.CloneWorksTrackId);

                            if (copyrights != null)
                            {
                                foreach (var copyRight in copyrights)
                                {
                                    //get all samples
                                    var samples =
                                        _snapshotSampleRepository.GetAllSamplesForRecCopyrightId(
                                            copyRight.SnapshotRecsCopyrightsId);

                                    if (samples != null)
                                    {
                                        foreach (var sample in samples)
                                        {
                                            _snapshotSampleRepository.DeleteSampleSnapshot(sample);
                                        }
                                    }
                                    //get all composers
                                    var composers =
                                        _snapshotComposerRepository.GetAllComposersByRecsCopyrightid(
                                            copyRight.SnapshotRecsCopyrightsId);

                                    if (composers != null)
                                    {
                                        foreach (var composer in composers)
                                        {
                                            var affiliations =
                                                _composerAffiliationRepository
                                                    .GetAllComposersAffiliationsByComposerSnapshotId(
                                                        composer.SnapshotComposerId);
                                            if (affiliations != null)
                                            {
                                                foreach (var affiliation in affiliations)
                                                {
                                                    var affiliationBases =
                                                        _composerAffiliationBaseRepository
                                                            .GetAllComposersAffiliationBasesByComposerAffiliationSnapshotId
                                                            (
                                                                affiliation.SnapshotComposerAffiliationId);

                                                    if (affiliationBases != null)
                                                    {
                                                        foreach (var affiliationBase in affiliationBases)
                                                        {
                                                            _composerAffiliationBaseRepository
                                                                .DeleteComposerAffiliationBaseSnapshotByComposer(
                                                                    affiliationBase);
                                                        }
                                                    }
                                                }
                                                foreach (var affiliation in affiliations)
                                                {
                                                    _composerAffiliationRepository
                                                        .DeleteComposerAffiliationSnapshotByComposer(
                                                            affiliation);
                                                }
                                            }

                                            var originalPublishers =
                                                _composerOriginalPublisherRepository
                                                    .GetAllComposerOriginalPublishersForComposerId(
                                                        composer.SnapshotComposerId);
                                            if (originalPublishers != null)
                                            {
                                                foreach (var originalPublisher in originalPublishers)
                                                {
                                                    var admins =
                                                        _composerOriginalPublisherAdministratorRepository
                                                            .GetAllComposerOriginalPublisherAdministratorsForComposerOriginalPublisherId
                                                            (originalPublisher.SnapshotComposerOriginalPublisherId);

                                                    if (admins != null)
                                                    {
                                                        foreach (var admin in admins)
                                                        {
                                                            var adminAffiliations =
                                                                _composerOriginalPublisherAdminAffiliation
                                                                    .GetAllComposerOriginalPublisherAdminAffiliationsorAdminId
                                                                    (
                                                                        admin
                                                                            .SnapshotComposerOriginalPublisherAdministratorId);
                                                            if (adminAffiliations != null)
                                                            {
                                                                foreach (var adminAffiliation in adminAffiliations)
                                                                {
                                                                    var adminBasse =
                                                                        _composerOriginalPublisherAdminAffiliationBaseRepository
                                                                            .GetAllComposerOriginalPublisherAdminAffiliationBasesForAffiliationId
                                                                            (adminAffiliation
                                                                                .SnapshotComposerOriginalPublisherAdminAffiliationId);
                                                                    if (adminBasse != null)
                                                                    {
                                                                        foreach (var adminBase in adminBasse)
                                                                        {
                                                                            _composerOriginalPublisherAdminAffiliationBaseRepository
                                                                                .DeleteComposerOriginalPublisherAdminAffiliationBase
                                                                                (adminBase);
                                                                        }
                                                                    }
                                                                    _composerOriginalPublisherAdminAffiliation
                                                                        .DeleteComposerOriginalPublisherAdminAffiliation
                                                                        (
                                                                            adminAffiliation);
                                                                }
                                                            }

                                                            var compOriginalPublisherAffilaitions =
                                                                _composerOriginalPublisherAffiliationRepository
                                                                    .GetAllComposerOriginalPublisherAffiliationsForComposerOriginalPublisherId
                                                                    (originalPublisher
                                                                        .SnapshotComposerOriginalPublisherId);

                                                            if (compOriginalPublisherAffilaitions != null)
                                                            {
                                                                foreach (
                                                                    var compOriginalPublisherAffilaition in
                                                                    compOriginalPublisherAffilaitions)
                                                                {
                                                                    var compOpAffBase =
                                                                        _composerOriginalPublisherAffiliationBaseRepository
                                                                            .GetComposerOriginalPublisherAffiliationBasesFComposerOriginalPublisherAffiliationId
                                                                            (compOriginalPublisherAffilaition
                                                                                .SnapshotComposerOriginalPublisherAffiliationId);
                                                                    if (compOpAffBase != null)
                                                                    {
                                                                        foreach (var affBase in compOpAffBase)
                                                                        {
                                                                            _composerOriginalPublisherAffiliationBaseRepository
                                                                                .DeleteComposerOriginalPublisherAffiliationBase
                                                                                (
                                                                                    affBase);
                                                                        }
                                                                    }
                                                                    _composerOriginalPublisherAffiliationRepository
                                                                        .DeleteComposerOriginalPubhliserAffiliation(
                                                                            compOriginalPublisherAffilaition);
                                                                }
                                                            }

                                                            _composerOriginalPublisherAdministratorRepository
                                                                .DeleteComposerOriginalPublisherAdministrator(admin);
                                                        }
                                                    }

                                                    var knownAs =
                                                        _composerOriginalPublisherKnownAsRepository
                                                            .GetAllComposerOriginalPublisherKnownAsByComposerOriginalPublisherSnapshotId
                                                            (composer.SnapshotComposerId);

                                                    if (knownAs != null)
                                                    {
                                                        foreach (var snapshotComposerOriginalPublisherKnownAs in knownAs
                                                        )
                                                        {
                                                            _composerOriginalPublisherKnownAsRepository
                                                                .DeleteComposerOriginalPublisherKnownAs(
                                                                    snapshotComposerOriginalPublisherKnownAs);
                                                        }
                                                    }
                                                    _composerOriginalPublisherRepository.DeleteComposerOriginalPublisher
                                                    (
                                                        originalPublisher);
                                                }
                                            }

                                            var localClients =
                                                _snapshotLocalClientCopyrightRepository.GetAllLocalCopyrightsForTrackId(
                                                    track.CloneWorksTrackId);
                                            if (localClients != null)
                                            {
                                                foreach (var localClient in localClients)
                                                {
                                                    _snapshotLocalClientCopyrightRepository
                                                        .DeleteLocalClientCopyrightBySnapshotId(
                                                            localClient.SnapshotLocalClientCopyrightId);
                                                }
                                            }

                                            var locationCodes =
                                                _aquisitionLocationCodeRepository
                                                    .GetAllAquisitionLocationCodesForTrackId(
                                                        track.CloneWorksTrackId);
                                            if (locationCodes != null)
                                            {
                                                foreach (var locationCode in locationCodes)
                                                {
                                                    _aquisitionLocationCodeRepository
                                                        .DeleteAquisitionLocationCodeBySnashotId(
                                                            locationCode.SnapshotAquisitionLocationCode);
                                                }
                                            }

                                            _snapshotRecsCopyrightRespository
                                                .DeleteRecsCopyrightByRecsCopyrightSnapshotId(
                                                    copyRight.SnapshotRecsCopyrightsId);
                                            _snapshotComposerRepository.DeleteComposerSnapshotByComposer(composer);
                                        }
                                    }

                                    //Delete worksRecordiing
                                    _snapshotWorksRecordingRepository.DeleteWorkRecordingByRecordignSnapshotId(
                                        recording.SnapshotWorksRecodingId);
                                }
                            }
                        }
                        if (track != null && track.SnapshotWorkTrackId != null)
                        {
                            _snapshotWorkTrackRepository.DeleteTrackBySnapshotTrackId(track.SnapshotWorkTrackId);
                        }
                        if (recording != null)
                        {
                            DeleteWorksWritersAndChildren(recording);
                        }
                    }
                }
            }
        }

        private void DeleteWorksWritersAndChildren(Snapshot_WorksRecording recording)
        {
            var writersForRecording = _snapshotWorksWriterRepository.GetAllWritersForCloneTrackId(recording.CloneTrackId);

            foreach (var writer in writersForRecording)
            {
                //Delete all affiliation list and child if not null
                //delete affil;iatio base list
                DeleteAllAffiliationsAndChildren(writer);

                //Delete knownAs list if not null
                var knownAsAll = _snapshotKnownAsRepository.GetAllKnownAsForWriterCaeCode(writer.CloneCaeNumber);
                if (knownAsAll != null)
                {
                    foreach (var knownAs in knownAsAll)
                    {
                        _snapshotKnownAsRepository.DeleteKnownAsBySnapshotId(knownAs.SnapshotKnownAsId);
                    }
                }
                //Delete Original Publiliser list and child if not null
                DeleteOriginalPublisherSnapshotAndChildern(writer);

                //delete writer
                _snapshotWorksWriterRepository.DeleteWorksWriterSnapshotBySnapshotId(writer.SnapshotWorksWriterId);
            }
        }

        private void DeleteOriginalPublisherSnapshotAndChildern(Snapshot_WorksWriter writer)
        {
            var allOriginalPublishers =
                _snapshotOriginalPublisherRepository.GetAllOriginalPublishersForSnapshotWriterId(writer.SnapshotWorksWriterId);

            foreach (var originalPublisher in allOriginalPublishers)
            {
                var affiliationList =
                    _originalPublisherAffiliationRepository.GetAllOriginalPublisherAffiliationsByOriginalBuplisherId(
                        originalPublisher.SnapshotOriginalPublisherId);
                if (affiliationList != null)
                {
                    foreach (var affiliation in affiliationList)
                    {
                        var affiliationBases =
                            _originalPubAffiliationBaseRepository.GetAllOriginalPubAffiliationBasesByAffilationId(
                                affiliation.SnapshotOriginalPublisherAffiliationId);
                        if (affiliationBases != null)
                        {
                            foreach (var affiliationBase in affiliationBases)
                            {
                                _originalPubAffiliationBaseRepository.DeletePhoneBySnapshotPhoneId(
                                    affiliationBase.SnapshotOriginalPubAffiliationBaseId);
                            }
                        }
                        _originalPublisherAffiliationRepository.DeleteOriginalPublisherSnapshotById(
                            affiliation.SnapshotOriginalPublisherAffiliationId);
                    }
                }

                var admins =
                    _snapshotAdministratorRepository.GetAllAdministratorsForOriginalPublisherId(
                        originalPublisher.SnapshotOriginalPublisherId);

                if (admins != null)
                {
                    foreach (var admin in admins)
                    {
                        var adminAffiliations =
                            _snapshotAdminAffiliationRepository.GetAllAdminAffiliationsForSnapshotAdminId(
                                admin.SnapshotAdministratorId);
                        if (adminAffiliations != null)
                        {
                            foreach (var adminAffiliation in adminAffiliations)
                            {
                                var adminAFfiliationBase =
                                    _adminAffiliationBaseRepository.GetAllAdminAffiliationBaseForSnapshotAdminId(
                                        adminAffiliation.SnapshotAdminAffiliationId);
                                if (adminAFfiliationBase != null)
                                {
                                    foreach (var adminBase in adminAFfiliationBase)
                                    {
                                        _adminAffiliationBaseRepository.DeleteConfigurationSnapshot(
                                            adminBase.SnapshotAdminAffiliationBaseId);
                                    }
                                }
                                _snapshotAdminAffiliationRepository.DeletePhoneBySnapshotPhoneId(
                                    adminAffiliation.SnapshotAdminAffiliationId);
                            }
                        }
                        _snapshotAdministratorRepository.DeleteConfigurationSnapshot(admin.SnapshotAdministratorId);
                    }
                }

                var knownAsList =
                    _snapshotKnownAsRepository.GetAllKnownAsForWriterCaeCode(
                        originalPublisher.CloneCaeNumber);
                if (knownAsList != null)
                {
                    foreach (var knownAs in knownAsList)
                    {
                        _snapshotKnownAsRepository.DeleteKnownAsBySnapshotId(knownAs.SnapshotKnownAsId);
                    }
                }
                _snapshotOriginalPublisherRepository.DeleteOriginalPublisherSnapshotBySnapshotId(
                    originalPublisher.SnapshotOriginalPublisherId);
            }
        }

        private void DeleteAllAffiliationsAndChildren(Snapshot_WorksWriter writer)
        {
            var allAffiliations = _snapshotAffiliationRepository.GetAllAffiliationsForWriterSnapshotId(writer.SnapshotWorksWriterId);
            if (allAffiliations != null)
            {
                foreach (var affilation in allAffiliations)
                {
                    var baseAffiliations =
                        _affiliationBaseRepository.GetAllAffiliationBasesForAffilationId(
                            affilation.SnapshotAffiliationId);
                    if (baseAffiliations != null)
                    {
                        foreach (var baseAffiliation in baseAffiliations)
                        {
                            _affiliationBaseRepository.DeleteAffilationByAffiliationBaseSnapshotId(
                                baseAffiliation.SnapshotAffiliationBaseId);
                        }
                    }

                    _snapshotAffiliationRepository.DeleteAffilationByAffiliationSnapshotId(
                        affilation.SnapshotAffiliationId);
                }
            }
        }

        private void DeleteSnapshotRecsRecordingandChildren(Snapshot_LicenseProduct licenseProduct)
        {
            var licenseConfigurations =
                _snapshotRecsConfigurationRepository.GetAllRecsConfigurationsRecordingsForLicenseProductId(licenseProduct.CloneLicenseProductId);
            if (licenseConfigurations != null)
            {
                //Delete all children
                foreach (var licenseConfig in licenseConfigurations)
                {
                    //Delete Config
                    _snapshotConfigurationRepository.DeleteConfigurationSnapshot(licenseConfig.SnapshotConfigurationId);
                }

                var ids = licenseConfigurations.Select(_ => _.SnapshotRecsConfigurationId).ToList();
                foreach (var id in ids)
                {
                    //delete recConfig
                    _snapshotRecsConfigurationRepository.GetAllRecsConfigurationsRecordingsForLicenseProductId(
                        id);
                }
            }
        }
    }
}