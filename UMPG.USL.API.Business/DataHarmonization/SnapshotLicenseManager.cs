using NLog;
using UMPG.USL.API.Data.DataHarmonization;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.Models.DataHarmonization;

namespace UMPG.USL.API.Business.DataHarmonization
{
    public class SnapshotLicenseManager : ISnapshotLicenseManager
    {
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
        private readonly ILicenseProductRecordingRepository _licenseProductRecordingRepository;
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
        private readonly ILicenseProductConfigurationRepository _licenseProductConfigurationRepository;
        private readonly ISnapshotAdministratorRepository _snapshotAdministratorRepository;
        private readonly ISnapshotAdminKnownAsRepository _adminKnownAsRepository;
        private readonly ISnapshotAdminAffiliationRepository _adminAffiliationRepository;
        private readonly ISnapshotAdminAffiliationBaseRepository _adminAffiliationBaseRepository;
        private readonly ISnapshotOriginalPublisherAffiliationRepository _originalPublisherAffiliationRepository;
        private readonly ISnapshotOriginalPubAffiliationBaseRepository _originalPubAffiliationBaseRepository;
        private readonly ISnapshotAffiliationBaseRepository _affiliationBaseRepository;
        private readonly ISnapshotComposerRepository _snapshotComposerRepository;
        private readonly ISnapshotComposerAffiliationRepository _composerAffiliationRepository;
        private readonly ISnapshotComposerAffiliationBaseRepository _composerAffiliationBaseRepository;
        private readonly ISnapshot_ComposerKnownAsRepository _composerKnownAsRepository;
        private readonly ISnapshotComposerOriginalPublisherAffiliationBaseRepository _composerOriginalPublisherAffiliationBaseRepository;
        private readonly ISnapshot_ComposerOriginalPublisherAffiliationRepository _composerOriginalPublisherAffiliationRepository;
        private readonly ISnapshot_ComposerOriginalPublisherRepository _composerOriginalPublisherRepository;
        private readonly ISnapshot_ComposerOriginalPublisherKnownAsRepository _composerOriginalPublisherKnownAsRepository;

        private readonly ISnapshotSampleAquisitionLocationCodeRepository _snapshotSampleAquisitionLocationCodeRepository;
        private readonly ISnapshotSampleLocalClientCopyrightRepository _snapshotSampleLocalClientCopyrightRepository;
        private readonly ISnapshotComposerOriginalPublisherAdministratorRepository _composerOriginalPublisherAdministratorRepository;
        private readonly ISnapshotComposerOriginalPublisherAdminAffiliationRepository _composerOriginalPublisherAdminAffiliation;
        private readonly ISnapshotComposerOriginalPublisherAdminAffiliationBaseRepository _composerOriginalPublisherAdminAffiliationBaseRepository;
        private readonly ISnapshotComposerOriginalPublisherAdminKnownAsRepository _composerOriginalPublisherAdminKnownAsRepository;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public SnapshotLicenseManager(ISnapshotLicenseRepository snapshotLicenseRepository,

            ISnapshotComposerOriginalPublisherAdminKnownAsRepository composerOriginalPublisherAdminKnownAsRepository,
        ISnapshotComposerRepository snapshotComposerRepository,
        ISnapshotComposerAffiliationRepository composerAffiliationRepository,
        ISnapshotComposerAffiliationBaseRepository composerAffiliationBaseRepository,
        ISnapshot_ComposerKnownAsRepository composerKnownAsRepository,
        ISnapshotComposerOriginalPublisherAffiliationBaseRepository composerOriginalPublisherAffiliationBaseRepository,
        ISnapshot_ComposerOriginalPublisherAffiliationRepository composerOriginalPublisherAffiliationRepository,
        ISnapshot_ComposerOriginalPublisherRepository composerOriginalPublisherRepository,
        ISnapshot_ComposerOriginalPublisherKnownAsRepository composerOriginalPublisherKnownAsRepository,

        ISnapshotSampleAquisitionLocationCodeRepository snapshotSampleAquisitionLocationCodeRepository,
        ISnapshotSampleLocalClientCopyrightRepository snapshotSampleLocalClientCopyrightRepository,
        ISnapshotComposerOriginalPublisherAdministratorRepository composerOriginalPublisherAdministratorRepository,
        ISnapshotComposerOriginalPublisherAdminAffiliationRepository composerOriginalPublisherAdminAffiliationRepository,
        ISnapshotComposerOriginalPublisherAdminAffiliationBaseRepository composerOriginalPublisherAdminAffiliationBaseRepository,

            ILicenseProductRecordingRepository licenseProductRecordingRepository,
            ISnapshotOriginalPubAffiliationBaseRepository snapshotOriginalPubAffiliationBaseRepository,
            ISnapshotAffiliationBaseRepository affiliationBaseRepository,
            ISnapshotOriginalPublisherAffiliationRepository snapshotOriginalPublisherAffiliationRepository,
            ISnapshotAdminAffiliationBaseRepository snapshotAdminAffiliationBaseRepository,
            ISnapshotAdminAffiliationRepository snapshotAdminAffiliationRepository,
            ISnapshotAdminKnownAsRepository snapshotAdminKnownAsRepository,
            ISnapshotAdministratorRepository snapshotAdministratorRepository,
            ILicenseProductConfigurationRepository licenseProductConfigurationRepository,
            ISnapshotAquisitionLocationCodeRepository aquisitionLocationCodeRepository,
            ISnapshotRecsCopyrightRespository snapshotRecsCopyrightRespository,
            ISnapshotSampleRepository snapshotSampleRepository,
            ISnapshotOriginalPublisherRepository snapshotOriginalPublisherRepository,
            ISnapshotKnownAsRepository snapshotKnownAsRepository,
            ISnapshotAffiliationRepository snapshotAffiliationRepository,
            ISnapshotWorksWriterRepository snapshotWorksWriterRepository,
            ISnapshotLicenseProductRecordingRepository snapshotLicenseProductRecordingRepository,
            ISnapshotWorkTrackRepository snapshotWorkTrackRepository,
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
            _composerOriginalPublisherAdminKnownAsRepository = composerOriginalPublisherAdminKnownAsRepository;
            _composerOriginalPublisherAdminAffiliationBaseRepository = composerOriginalPublisherAdminAffiliationBaseRepository;
            _composerOriginalPublisherAdminAffiliation = composerOriginalPublisherAdminAffiliationRepository;
            _composerOriginalPublisherAdministratorRepository = composerOriginalPublisherAdministratorRepository;

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
            _licenseProductRecordingRepository = licenseProductRecordingRepository;
            _affiliationBaseRepository = affiliationBaseRepository;
            _originalPublisherAffiliationRepository = snapshotOriginalPublisherAffiliationRepository;
            _originalPubAffiliationBaseRepository = snapshotOriginalPubAffiliationBaseRepository;
            _adminAffiliationBaseRepository = snapshotAdminAffiliationBaseRepository;
            _adminAffiliationRepository = snapshotAdminAffiliationRepository;
            _snapshotAdministratorRepository = snapshotAdministratorRepository;
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
            _adminKnownAsRepository = snapshotAdminKnownAsRepository;
        }

        public bool DoesSnapshotExists(int licenseId)
        {
            return _snapshotLicenseRepository.DoesLicenseSnapshotExist(licenseId);
        }

        public Snapshot_License SaveSnapshotLicense(Snapshot_License snapshotLicense)
        {
            return _snapshotLicenseRepository.SaveSnapshotLicense(snapshotLicense);
        }

        public Snapshot_License GetSnapshotLicenseBySnapshotLicenseId(int snapshotLicenseId)
        {
            var licenseInformation = _snapshotLicenseRepository.GetLicenseSnapShotById(snapshotLicenseId);
            var licenseProducts =
                _snapshotLicenseProductRepository.GetAllLicenseProductsForLicenseId(licenseInformation.CloneLicenseId);

            if (licenseProducts != null)
            {
                foreach (var lp in licenseProducts)
                {
                    //build product header
                    if (lp.ProductHeaderId != null)
                    {
                        var id = (int)lp.ProductHeaderId;
                        var productHeader =
                            _snapshotProductHeaderRepository.GetProductHeaderByProductHeaderId(id);
                        //Assign
                        lp.ProductHeader = productHeader;

                        var artist =
                            _snapshotArtistRecsRepository.GetSnapshotArtistRecsByArtistId(productHeader.ArtistRecsId);

                        var label = _snapshotLabelRepository.GetSnapshotLabelByLabelId(productHeader.LabelId);

                        var labelGroups =
                            _snapshotLabelGroupRepository.GetAllALabelGroupsForLabelId(
                                label.CloneLabelId);
                        label.RecordLabelGroups = labelGroups;

                        var configs =
                            _snapshotRecsConfigurationRepository.GetAllRecsConfigurationsRecordingsForProductHeaderId(
                                productHeader.CloneProductHeaderId);

                        if (configs != null)
                        {
                            foreach (var config in configs)
                            {
                                if (config.ConfigurationId != null)
                                {
                                    var configId = (int)config.ConfigurationId;
                                    var configuration =
                                        _snapshotConfigurationRepository.GetSnapshotConfigurationByConfigurationId(
                                            configId);
                                    config.Configuration = configuration;
                                }
                                if (config.LicenseProductConfigurationId != null)
                                {
                                    var lprid = (int)config.CloneRecsConfigurationId;
                                    var licenseProductId = (int)config.LicenseProductId;
                                    var licenseProductConfig =
                                        _licenseProductConfigurationRepository
                                            .GetLicenseProductConfiguration(licenseProductId, lprid);
                                    config.LicenseProductConfiguration = licenseProductConfig;
                                }
                            }
                        }

                        lp.ProductHeader.Artist = artist;
                        lp.ProductHeader.Label = label;
                        lp.ProductHeader.Configurations = configs;
                    }

                    //build worksRecording list
                    if (lp.ProductId != null)
                    {
                        var recordings =
                            _snapshotWorksRecordingRepository.GetAllWorksRecordingsForProductId(lp.ProductId);

                        foreach (var recording in recordings)
                        {
                            //build track and copy rights
                            var track = _snapshotWorkTrackRepository.GetTrackForCloneTrackId(recording.TrackId);
                            var copyrights =
                                _snapshotRecsCopyrightRespository.GetAllRecsCopyrightsForCloneTrackId(
                                    recording.CloneTrackId);

                            var artist =
                                _snapshotArtistRecsRepository.GetSnapshotArtistRecsByArtistId(track.ArtistRecsId);
                            track.Artist = artist;

                            var licenseProductRecording =
                        _licenseProductRecordingRepository.GetLicenseProductRecordingByLicenseProductRecordingId(
                            recording.LicenseProductRecordingId);

                            //This area may be wrong, it uses the same primary key as parent entites.  watch.
                            foreach (var copyright in copyrights)
                            {
                                //build samples

                                var samples =
                                    _snapshotSampleRepository.GetAllSamplesForRecCopyrightId(
                                        copyright.SnapshotRecsCopyrightsId);

                                //build composers
                                var composers =
                                _snapshotComposerRepository.GetAllComposersByRecsCopyrightid(
                                    copyright.SnapshotRecsCopyrightsId);

                                if (composers != null)
                                {
                                    foreach (var composer in composers)
                                    {
                                        //build original pubs
                                        var composerOriginalPublisers =
                                            _composerOriginalPublisherRepository
                                                .GetAllComposerOriginalPublishersForComposerId(
                                                    composer.SnapshotComposerId);

                                        if (composerOriginalPublisers != null)
                                        {
                                            foreach (var composerOriginalPubliser in composerOriginalPublisers)
                                            {
                                                var composerOriginalPublisherAdmins =
                                                    _composerOriginalPublisherAdministratorRepository
                                                        .GetAllComposerOriginalPublisherAdministratorsForComposerOriginalPublisherId
                                                        (composerOriginalPubliser.SnapshotComposerOriginalPublisherId);

                                                if (composerOriginalPublisherAdmins != null)
                                                {
                                                    foreach (var composerOriginalPublisherAdmin in composerOriginalPublisherAdmins)
                                                    {
                                                        var composerOrignalPublisherAdminAffiliations =
                                                            _composerOriginalPublisherAdminAffiliation
                                                                .GetAllComposerOriginalPublisherAdminAffiliationsorAdminId
                                                                (composerOriginalPublisherAdmin
                                                                    .SnapshotComposerOriginalPublisherAdministratorId);

                                                        if (composerOrignalPublisherAdminAffiliations != null)
                                                        {
                                                            foreach (var composerOrignalPublisherAdminAffiliation in composerOrignalPublisherAdminAffiliations)
                                                            {
                                                                var adminAffiliationBase =
                                                                    _composerOriginalPublisherAdminAffiliationBaseRepository
                                                                        .GetAllComposerOriginalPublisherAdminAffiliationBasesForAffiliationId
                                                                        (composerOrignalPublisherAdminAffiliation
                                                                            .SnapshotComposerOriginalPublisherAdminAffiliationId);
                                                                composerOrignalPublisherAdminAffiliation.Affiliations =
                                                                    adminAffiliationBase;
                                                            }
                                                        }
                                                        composerOriginalPublisherAdmin.Affiliation =
                                                            composerOrignalPublisherAdminAffiliations;
                                                    }
                                                }
                                                composerOriginalPubliser.Administrator = composerOriginalPublisherAdmins;

                                                var composerOriginalPublisherAffiliations =
                                                    _composerOriginalPublisherAffiliationRepository
                                                        .GetAllComposerOriginalPublisherAffiliationsForComposerOriginalPublisherId
                                                        (composerOriginalPubliser.SnapshotComposerOriginalPublisherId);

                                                if (composerOriginalPublisherAffiliations != null)
                                                {
                                                    foreach (var composerOriginalPublisherAffiliation in composerOriginalPublisherAffiliations)
                                                    {
                                                        var composerOriginalPublisherBaseAffiliations =
                                                            _composerOriginalPublisherAffiliationBaseRepository
                                                                .GetComposerOriginalPublisherAffiliationBasesFComposerOriginalPublisherAffiliationId
                                                                (composerOriginalPublisherAffiliation
                                                                    .SnapshotComposerOriginalPublisherAffiliationId);

                                                        composerOriginalPublisherAffiliation.Affiliations =
                                                            composerOriginalPublisherBaseAffiliations;
                                                    }
                                                    composerOriginalPubliser.Affiliation =
                                                        composerOriginalPublisherAffiliations;
                                                }
                                            }
                                            composer.OriginalPublishers = composerOriginalPublisers;
                                        }

                                        //build affiliations
                                        var composerAffiliations =
                                            _composerAffiliationRepository
                                                .GetAllComposersAffiliationsByComposerSnapshotId(composer.SnapshotComposerId);

                                        if (composerAffiliations != null)
                                        {
                                            foreach (var composerAffiliation in composerAffiliations)
                                            {
                                                var composerBaseAffiliations =
                                                    _composerAffiliationBaseRepository
                                                        .GetAllComposersAffiliationBasesByComposerAffiliationSnapshotId(composerAffiliation
                                                            .SnapshotComposerAffiliationId);
                                                composerAffiliation.Affiliations =
                                                    composerBaseAffiliations;
                                            }
                                            composer.Affiliation = composerAffiliations;
                                        }
                                    }
                                }

                                //local clients
                                var localClients =
                                    _snapshotLocalClientCopyrightRepository.GetAllLocalCopyrightsForRecsCopyrightSnapshotId(
                                        copyright.SnapshotRecsCopyrightsId);

                                //aquiLocationCodes
                                var aquisitionLocalCodes =
                                    _aquisitionLocationCodeRepository.GetAllAquisitionLocationCodesForRecsCopyrightId(
                                        copyright.SnapshotRecsCopyrightsId);

                                //assign
                                copyright.Samples = samples;
                                copyright.Composers = composers;
                                copyright.LocalClients = localClients;
                                copyright.AquisitionLocationCodes = aquisitionLocalCodes;
                            }

                            //assign track chunk
                            track.Copyrights = copyrights;

                            var worksWriterList =
                                _snapshotWorksWriterRepository.GetAllWritersForCloneTrackId(recording.CloneTrackId);  //This is clone track, but clone track is an identity column for some reason... refactor needed to add trackId to recordign

                            if (worksWriterList != null)
                            {
                                foreach (var writer in worksWriterList)
                                {
                                    var affiliationList =
                                        _snapshotAffiliationRepository.GetAllAffiliationsForWriterSnapshotId(
                                            writer.SnapshotWorksWriterId);

                                    if (affiliationList != null)
                                    {
                                        foreach (var affiliation in affiliationList)
                                        {
                                            var affiliationBase =
                                                _affiliationBaseRepository.GetAllAffiliationBasesForAffilationId(
                                                    affiliation.SnapshotAffiliationId);
                                            affiliation.Affiliations = affiliationBase;
                                        }
                                    }

                                    var originalPublisherList =
                                        _snapshotOriginalPublisherRepository.GetAllOriginalPublishersForSnapshotWriterId(
                                            writer.SnapshotWorksWriterId);

                                    foreach (var op in originalPublisherList)
                                    {
                                        var knownAs =
                                            _snapshotKnownAsRepository.GetAllKnownAsForWriterCaeCode(op.CloneCaeNumber);
                                        op.KnownAs = knownAs;

                                        var opAdmins =
                                            _snapshotAdministratorRepository.GetAllAdministratorsForOriginalPublisherId(
                                                op.SnapshotOriginalPublisherId);

                                        var opAffiliations =
                                            _originalPublisherAffiliationRepository
                                                .GetAllOriginalPublisherAffiliationsByOriginalBuplisherId(
                                                    op.SnapshotOriginalPublisherId);

                                        if (opAffiliations != null)
                                        {
                                            foreach (var opAffiliation in opAffiliations)
                                            {
                                                var opAffiliationBases =
                                                    _originalPubAffiliationBaseRepository
                                                        .GetAllOriginalPubAffiliationBasesByAffilationId(
                                                            opAffiliation.SnapshotOriginalPublisherAffiliationId);
                                                opAffiliation.Affiliations = opAffiliationBases;
                                            }
                                            op.Affiliation = opAffiliations;
                                        }

                                        //build admin children
                                        foreach (var admin in opAdmins)
                                        {
                                            var adminKnownAs =
                                                _adminKnownAsRepository.GetAllAdminKnownAsForAdminSnapshotId(
                                                    admin.SnapshotAdministratorId);

                                            admin.KnownAs = adminKnownAs;

                                            var adminAffiliation =
                                                _adminAffiliationRepository.GetAllAdminAffiliationsForSnapshotAdminId(
                                                    admin.SnapshotAdministratorId);
                                            if (adminAffiliation != null)
                                            {
                                                foreach (var adminAfilation in adminAffiliation)
                                                {
                                                    var adminBase =
                                                        _adminAffiliationBaseRepository
                                                            .GetAllAdminAffiliationBaseForSnapshotAdminId(
                                                                adminAfilation.SnapshotAdminAffiliationId);

                                                    adminAfilation.Affiliations = adminBase;
                                                }
                                            }

                                            admin.Affiliation = adminAffiliation;
                                        }
                                        op.Administrator = opAdmins;
                                    }

                                    var writerKnownAsList =
                                        _snapshotKnownAsRepository.GetAllKnownAsForWriterCaeCode(writer.CloneCaeNumber);

                                    //Assign
                                    writer.Affiliation = affiliationList;
                                    writer.KnownAs = writerKnownAsList;
                                    writer.OriginalPublishers = originalPublisherList;
                                }
                            }

                            //Assign
                            recording.Track = track;
                            recording.Writers = worksWriterList;
                            recording.LicenseRecording = licenseProductRecording;
                        }
                        lp.Recordings = recordings;
                    }

                    //build licenseRecordingCOnfigs
                    if (lp.CloneLicenseProductId != null)
                    {
                        var configId = (int)lp.CloneLicenseProductId;

                        var licenserecConfigs =
                            _snapshotRecsConfigurationRepository.GetAllRecsConfigurationsRecordingsForLicenseProductId(
                                configId);
                        if (licenserecConfigs != null)
                        {
                            foreach (var recConfig in licenserecConfigs)
                            {
                                if (recConfig.ConfigurationId != null)
                                {
                                    var recConfigId = (int)recConfig.ConfigurationId;

                                    var configuration =
                                        _snapshotConfigurationRepository.GetSnapshotConfigurationByConfigurationId(
                                            recConfigId);
                                    recConfig.Configuration = configuration;
                                }

                                if (recConfig.LicenseProductConfigurationId != null && recConfig.LicenseProductId != null)
                                {
                                    var lprId = (int)recConfig.LicenseProductConfigurationId;
                                    var lpId = (int)recConfig.LicenseProductId;
                                    var lprConfig =
                                        _licenseProductConfigurationRepository
                                            .GetLicenseProductConfiguration(lpId, lprId);
                                    recConfig.LicenseProductConfiguration = lprConfig;
                                }
                            }
                        }
                    }
                }

                licenseInformation.LicenseProducts = licenseProducts;
            }
            /*
            foreach (var lp in licenseInformation.LicenseProducts)
            {
                var recConfig =
                    _snapshotRecsConfigurationRepository.GetAllRecsConfigurationsRecordingsForProductHeaderId(
                        lp.ProductHeader.CloneProductHeaderId);
                lp.ProductHeader.Configurations = recConfig;
            }
            */
            return licenseInformation;
        }

        //Jeffs new method.  V-1.0

        public bool DeleteLicenseSnapshotAndAllChildren(int licenseId)
        {
            //get license
            var license = _snapshotLicenseRepository.GetSnapshotLicenseByCloneLicenseId(licenseId);

            //Delete all license Contacts
            //  DeleteAllLicenseContactChildren(license);  | temp off

            //Delet Licensee Label Group
            // DeleteLicenseeLabelGroup(license);  | temp off

            //Delete License Product List and children
            DeleteLicenseProductAndChildEntities(license);

            //Delete LicenseNotes and Children
            DeleteLicenseNotesAndChildren(license);

            //Delete license
            return _snapshotLicenseRepository.DeleteSnapshotLicense(licenseId);
        }

        private void DeleteLicenseeLabelGroup(Snapshot_License license)
        {
            //Delete LicenseLabel Group
            if (license.LicenseeLabelGroupId != null)
            {
                int id = (int)license.LicenseeLabelGroupId;
                var licenseeLabelGroup =
                    _licenseeLabelGroupRepository.GetLicenseeLabelGroupByCloneLicenseeLabelGroupId(id);
                _licenseeLabelGroupRepository.DeleteSnapshotLicenseeLabelGroupBySnapshotId(
                    licenseeLabelGroup.SnapshotLicenseeLabelGroupId);
            }
        }

        private void DeleteAllLicenseContactChildren(Snapshot_License license)
        {
            //Delete all child license Entities

            //Delete Contact
            if (license.ContactId != null)
            {
                int id = (int)license.ContactId;
                var contact = _snapshotContactRepository.GetSnapshotContactByContactId(id);
                _snapshotContactRepository.DeleteContactBySnapshotContactId(contact.SnapshotContactId);
            }

            //Delete Contact2  || not used

            //Delete LicenseeContact
            if (license.LicenseeId != null)
            {
                int id = (int)license.LicenseeId;
                var contact = _snapshotContactRepository.GetSnapshotContactByContactId(id);
                _snapshotContactRepository.DeleteContactBySnapshotContactId(contact.SnapshotContactId);
            }
        }

        private void DeleteLicenseNotesAndChildren(Snapshot_License license)
        {
            var licenseNotes = _snapshotLicenseNoteRepository.GetAllLicenseNoteForLicenseId(license.CloneLicenseId);
            foreach (var note in licenseNotes)
            {
                if (note.CreatedBy != null)
                {
                    var createdByContactId = (int)note.CreatedBy;
                    _snapshotContactRepository.DeleteContactBySnapshotContactId(createdByContactId);
                }
                _snapshotLicenseNoteRepository.DeleteLicenseNoteSnapshotByLicenseNoteId(note.SnapshotLicenseNoteId);
            }
        }

        private void DeleteLicenseProductAndChildEntities(Snapshot_License license)
        {
            //getAllLicenseProducts
            var licenseProducts =
                _snapshotLicenseProductRepository.GetAllLicenseProductsForLicenseId(license.CloneLicenseId);

            foreach (var licenseProduct in licenseProducts)
            {
                //Delete All RecsConfiguration and children
                DeleteSnapshotRecsRecordingandChildren(licenseProduct);

                //Delete worksRecording and Children
                DeleteAllWorksRecordingAndChildren(licenseProduct);

                //Delete Product HEader and children
                DeleteProductHeaderAndChildren(licenseProduct);

                //Delete License Product
                _snapshotLicenseProductRepository.DeleteLicenseProductSnapshot(licenseProduct.SnapshotLicenseProductId);
            }
        }

        private void DeleteProductHeaderAndChildren(Snapshot_LicenseProduct licenseProduct)
        {
         ;
            if (licenseProduct.ProductHeaderId != null)
            {
               var id = (int) licenseProduct.ProductHeaderId;

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
            var label = _snapshotLabelRepository.GetSnapshotLabelByLabelId(productHeader.LabelId);

            //labelGroups
            var labelGroups =
                _snapshotLabelGroupRepository.GetAllALabelGroupsForLabelId(
                    label.CloneLabelId);
            if (labelGroups != null)
            {
                foreach (var labelGroup in labelGroups)
                {
                    _snapshotLabelGroupRepository.DeleteLabelGroupByLabelGroupSnapshotId(labelGroup.SnapshotLabelGroupId);
                }
            }

            //Delete label
            _snapshotLabelRepository.DeleteLabelSnapshotBySnapshotId(label.SnapshotLabelId);
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
            var workRecordings =
                _snapshotWorksRecordingRepository.GetAllWorksRecordingsForLicenseProductId(licenseProduct.CloneLicenseProductId);
            if (workRecordings != null)
            {
                foreach (var recording in workRecordings)
                {
                    //delete works track
                    var track = _snapshotWorkTrackRepository.GetTrackForCloneTrackId(recording.TrackId);
                    
                    //get artist and delte
                    var artist = _snapshotArtistRecsRepository.GetSnapshotArtistRecsByArtistId(track.ArtistRecsId);
                    if (artist != null)
                    {
                        _snapshotArtistRecsRepository.DeleteRecsArtisByArtistSnapshotId(artist.SnapshotArtistRecsId);
                    }

                    //delete copyrights
                    var copyrights =
                        _snapshotRecsCopyrightRespository.GetAllRecsCopyrightsForCloneTrackId(track.CloneWorksTrackId);

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
                                        _composerAffiliationRepository.GetAllComposersAffiliationsByComposerSnapshotId(
                                            composer.SnapshotComposerId);
                                    if (affiliations != null)
                                    {
                                        foreach (var affiliation in affiliations)
                                        {
                                            var affiliationBases =
                                                _composerAffiliationBaseRepository
                                                    .GetAllComposersAffiliationBasesByComposerAffiliationSnapshotId(
                                                        affiliation.SnapshotComposerAffiliationId);

                                            if (affiliationBases != null)
                                            {
                                                foreach (var affiliationBase in affiliationBases)
                                                {
                                                    _composerAffiliationBaseRepository
                                                        .DeleteComposerAffiliationBaseSnapshotByComposer(affiliationBase);
                                                }
                                            }
                                        }
                                        foreach (var affiliation in affiliations)
                                        {
                                            _composerAffiliationRepository.DeleteComposerAffiliationSnapshotByComposer(
                                                affiliation);
                                        }
                                    }

                                    var originalPublishers =
                                        _composerOriginalPublisherRepository
                                            .GetAllComposerOriginalPublishersForComposerId(composer.SnapshotComposerId);
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
                                                            .GetAllComposerOriginalPublisherAdminAffiliationsorAdminId(
                                                                admin.SnapshotComposerOriginalPublisherAdministratorId);
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
                                                                .DeleteComposerOriginalPublisherAdminAffiliation(
                                                                    adminAffiliation);
                                                        }
                                                    }

                                                    var compOriginalPublisherAffilaitions =
                                                        _composerOriginalPublisherAffiliationRepository
                                                            .GetAllComposerOriginalPublisherAffiliationsForComposerOriginalPublisherId
                                                            (originalPublisher.SnapshotComposerOriginalPublisherId);

                                                    if (compOriginalPublisherAffilaitions != null)
                                                    {
                                                        foreach (var compOriginalPublisherAffilaition in compOriginalPublisherAffilaitions)
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
                                                                        .DeleteComposerOriginalPublisherAffiliationBase(
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
                                                foreach (var snapshotComposerOriginalPublisherKnownAs in knownAs)
                                                {
                                                    _composerOriginalPublisherKnownAsRepository
                                                        .DeleteComposerOriginalPublisherKnownAs(
                                                            snapshotComposerOriginalPublisherKnownAs);
                                                }
                                            }
                                            _composerOriginalPublisherRepository.DeleteComposerOriginalPublisher(
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
                                        _aquisitionLocationCodeRepository.GetAllAquisitionLocationCodesForTrackId(
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
                              

                                    _snapshotRecsCopyrightRespository.DeleteRecsCopyrightByRecsCopyrightSnapshotId(
                                        copyRight.SnapshotRecsCopyrightsId);
                                    _snapshotComposerRepository.DeleteComposerSnapshotByComposer(composer);
                                }
                               
                            }

                 
                       

                            //Delete worksRecordiing
                            _snapshotWorksRecordingRepository.DeleteWorkRecordingByRecordignSnapshotId(
                                recording.SnapshotWorksRecodingId);
                        }

                    }
                    _snapshotWorkTrackRepository.DeleteTrackBySnapshotTrackId(track.SnapshotWorkTrackId);
                    DeleteWorksWritersAndChildren(recording);
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
                            _adminAffiliationRepository.GetAllAdminAffiliationsForSnapshotAdminId(
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
                                _adminAffiliationRepository.DeletePhoneBySnapshotPhoneId(
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
                foreach (var licenseConfig in licenseConfigurations)
                {
                    //Delete Config
                    _snapshotConfigurationRepository.DeleteConfigurationSnapshot(licenseProduct.SnapshotLicenseProductId);

                    //Delete LicenseProductConfig if exists
                    if (licenseConfig.LicenseProductConfigurationId != null)
                    {
                        /* Mechs data, do not erase
                        var id = (int)licenseConfig.LicenseProductConfigurationId;
                        var licenseProductConfig =
                            _licenseProductConfigurationRepository
                                .GetSnapshotLicenseProductConfigurationByLicenseProductConfigurationId(
                                    id);
                        if (licenseProductConfig != null)
                        {
                            //Delete licenseProductConfiguration
                            _licenseProductConfigurationRepository.DeleteLicenseProductConfigurationBySnapshot(
                                licenseProductConfig);
                        }
                        */
                    }

                    //delete recConfig
                    _snapshotRecsConfigurationRepository.DeleteWorkRecordingByRecordignSnapshotId(
                        licenseConfig.SnapshotRecsConfigurationId);
                }
            }
        }
        /*
        //v.01
        //Delete License and all child entites
        public bool DeleteLicenseSnapshot(int licenseId)
        {
            var licenseProductIds = _snapshotLicenseProductRepository.GetLicenseProductIds(licenseId);
            //Delete all children of license
            //Delete LicenseProducts
            foreach (var id in licenseProductIds)
            {
                //___Delete each child LicenseProducts___

                //Delete all recordings blcock (snapshot_worksRecording)

                var productId = _snapshotLicenseProductRepository.GetProductIdFromSnapshotLicenseProductId(id);
                if (productId != null)
                {
                    var recordings = _snapshotWorksRecordingRepository.GetAllWorksRecordingsForProductId(productId);
                    foreach (var rec in recordings)
                    {
                        _snapshotWorksRecordingRepository.DeleteWorkRecordingByRecordignSnapshotId(
                            rec.SnapshotWorksRecodingId);
                    }
                }

                var licenseProductId =
                    _snapshotLicenseProductRepository.GetLicenseProductIdFromSnapshotLicenseProductId(id);
                if (licenseProductId != null)
                {
                    var recConfigurations =
                        _snapshotRecsConfigurationRepository.GetAllRecsConfigurationsRecordingsForLicenseProductId(
                            licenseProductId);
                    //Deelte all RecsConfigurations
                    foreach (var rec in recConfigurations)
                    {
                        //delete config
                        _snapshotConfigurationRepository.DeleteConfigurationSnapshot(rec.Configuration.SnapshotConfigId);

                        _snapshotRecsConfigurationRepository.DeleteWorkRecordingByRecordignSnapshotId(
                            rec.SnapshotRecsConfigurationId);
                    }
                }

                //____________Delete Product Header Block
                //------- Artist
                //--------Label
                //--------RecsConfigurations
                var productHeaderPrimaryKey =
                    _snapshotProductHeaderRepository.GetSnapshotProductHeaderBySnapshotLicenseProductId(id);

                //delete productHeader childres
                //delete all recConfigurations
                var result =
                    _snapshotRecsConfigurationRepository.DoesRecConfigurationrecordignsExistForProductHeaderSnapshotId(
                        productHeaderPrimaryKey);
                if (result)
                {
                    var recConfiguationsOnProductHeader =
                        _snapshotRecsConfigurationRepository.GetAllRecsConfigurationsRecordingsForProductHeaderSnapshotId(
                            productHeaderPrimaryKey);
                    foreach (var config in recConfiguationsOnProductHeader)
                    {
                        //delete config
                        _snapshotConfigurationRepository.DeleteConfigurationSnapshot(
                            config.Configuration.SnapshotConfigId);

                        _snapshotRecsConfigurationRepository.DeleteWorkRecordingByRecordignSnapshotId(
                            config.SnapshotRecsConfigurationId);
                    }

                    //delete artist
                    _snapshotArtistRecsRepository.DeleteRecsArtistByProductHeaderSnapshotId(productHeaderPrimaryKey);

                    //check if label groups exists
                    //delet label groups
                    var labelGroups =
                        _snapshotLabelGroupRepository.GetAllLabelGroupsForProductHeaderSnapshotId(productHeaderPrimaryKey);

                    foreach (var labelGroup in labelGroups)
                    {
                        _snapshotLabelGroupRepository.DeleteLabelGroupByLabelGroupSnapshotId(labelGroup.SnapshotLabelGroupId);
                    }
                    //delete label
                    _snapshotLabelRepository.DeleteLabelSnapshotByProductHeaderSnapshotId(productHeaderPrimaryKey);

                    //delete productHEader
                    _snapshotProductHeaderRepository.DeleteProductHeaderSnapshotBySnapshotId(productHeaderPrimaryKey);
                }
                //
                //Delete LicenseProducts
                _snapshotLicenseProductRepository.DeleteLicenseProductSnapshot(id);
            }

            //DeleteNotelist
            //Delete all notes with LicenseiD
            var licenseNoteIds = _snapshotLicenseNoteRepository.GetAllLicenseNoteIdsForLicenseId(licenseId);
            var contactIds = _snapshotLicenseNoteRepository.GetAllContactIdsRelatedToNote(licenseId);
            foreach (var licenseNoteId in licenseNoteIds)
            {
                _snapshotLicenseNoteRepository.DeleteLicenseNoteSnapshotByLicenseNoteId(licenseNoteId);
            }

            foreach (var contactId in contactIds)
            {
                _snapshotContactRepository.DeleteContactBySnapshotContactId(contactId);

                //Get role Id for contact ID
                var roleId = _snapshotContactRepository.GetRoleIdForCOntactId(contactId);
                //delete role
                _snapshotRoleRepository.DeleteRoleSnapshotByRoleId(roleId);

                //Get cloneCOntactId to delete adress, phone, contact emails
                var cloneContactId = _snapshotContactRepository.GetContactBySnapshotContactId(contactId);

                //Delete all address for clone contact id
                var addresses = _snapshotAddressRepository.GetAllAddressesForCloneContactId(cloneContactId);
                foreach (var address in addresses)
                {
                    _snapshotAddressRepository.DeleteAddressBySnapshotAddressId(address.SnapshotAddressId);
                }

                //Delete all phone for clone contact id
                var phones = _snapshotPhoneRepository.GetAllPhonesForCloneContactId(cloneContactId);
                foreach (var phone in phones)
                {
                    _snapshotPhoneRepository.DeletePhoneBySnapshotPhoneId(phone.SnapshotPhoneId);
                }

                //Delete all  contact emails for clone contact id
                var contactEmails = _snapshotContactEmailRepository.GetAllContactEmailsForCloneContactId(cloneContactId);
                foreach (var contactEmail in contactEmails)
                {
                    _snapshotContactEmailRepository.DeleteContactEmailBySnapshotContactEmailId(
                        contactEmail.SnapshotContactEmailId);
                }
            }

            //delete rest of license entities

            return _snapshotLicenseRepository.DeleteSnapshotLicense(licenseId);
        }
        */
    }

}

//License
//-Has
//LicenseeLabelGroup
//Contact
//Contact2
//LicenseContact
//LicenseNoteList
//-Has
//LicenseNote
//Contact
//LicenseProductList
//-Has
//ProductHeader
//-Has
//Artist
//Label
//-Has
//LabelGroup

//RecordingList
//ProductConfigutationsList