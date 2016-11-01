using UMPG.USL.API.Data.DataHarmonization;
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
        private readonly ISnapshotRecsConfigurationRepository _snapshotRecsConfiguration;
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


        public SnapshotLicenseManager(ISnapshotLicenseRepository snapshotLicenseRepository,
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
            ISnapshotRecsConfigurationRepository snapshotRecsConfiguration,
            ISnapshotProductHeaderRepository snapshotProductHeaderRepository,
            ISnapshotConfigurationRepository snapshotConfigurationRepository,
            ISnapshotArtistRecsRepository snapshotArtistRecsRepository, ISnapshotLabelRepository snapshotLabelRepository,
            ISnapshotLabelGroupRepository snapshotLabelGroupRepository,
            ISnapshotLicenseeLabelGroupRepository snalshotLabelGroupRepository)
        {
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
            _snapshotRecsConfiguration = snapshotRecsConfiguration;
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

            foreach (var lp in licenseInformation.LicenseProducts)
            {
                var recConfig =
                    _snapshotRecsConfiguration.GetAllRecsConfigurationsRecordingsForProductHeaderId(
                        lp.ProductHeader.CloneProductHeaderId);
                lp.ProductHeader.Configurations = recConfig;
            }
            return licenseInformation;
        }

        //Jeffs new method.  V-1.0

        public bool DeleteLicenseSnapshotAndAllChildren(int licenseId)
        {
            //get license
            var license = _snapshotLicenseRepository.GetSnapshotLicenseByCloneLicenseId(licenseId);

            //Delete all license Contacts
            DeleteAllLicenseContactChildren(license);

            //Delet Licensee Label Group
            DeleteLicenseeLabelGroup(license);

            //Delete License Product List and children
            DeleteLicenseProductAndChildEntities(license);

            //Delete LicenseNotes and Children
            DeleteLicenseNotesAndChildren(license);

            //Delete license
            return _snapshotLicenseRepository.DeleteSnapshotLicense(licenseId);
        }

        public void DeleteLicenseeLabelGroup(Snapshot_License license)
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

        public void DeleteAllLicenseContactChildren(Snapshot_License license)
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

        public void DeleteLicenseNotesAndChildren(Snapshot_License license)
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

        public void DeleteLicenseProductAndChildEntities(Snapshot_License license)
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



                //Delete License Product 
                _snapshotLicenseProductRepository.DeleteLicenseProductSnapshot(licenseProduct.SnapshotLicenseProductId);
            }
        }

        public void DeleteProductHeaderAndChildren(Snapshot_LicenseProduct licenseProduct)
        {
            var productHeader =
                _snapshotProductHeaderRepository.GetSnapshotProductHeaderBySnapshotProductHeaderId(
                    licenseProduct.SnapshotLicenseProductId);

            //Delete artist
            var artist = _snapshotArtistRecsRepository.GetSnapshotArtistRecsByArtistId(productHeader.ArtistRecsId);
            _snapshotArtistRecsRepository.DeleteRecsArtistByProductHeaderSnapshotId(artist.SnapshotArtistRecsId);

            //Delete label and child (label group)
            DeleteLabelAndAllChuldren(productHeader);

            //Delete RecsConfigurations 'Configurations'
            DeleteAllRecsConfigAndChildrenForProductHeader(productHeader);
        }

        private void DeleteLabelAndAllChuldren(Snapshot_ProductHeader productHeader)
        {
            var label = _snapshotLabelRepository.GetSnapshotLabelByLabelId(productHeader.LabelId);

            //labelGroups
            var labelGroups =
                _snapshotLabelGroupRepository.GetAllLabelGroupsForProductHeaderSnapshotId(
                    productHeader.SnapshotProductHeaderId);
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
                _snapshotRecsConfiguration.GetAllRecsConfigurationsRecordingsForProductHeaderId(
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
                        var licenseProductConfig =
                            _licenseProductConfigurationRepository
                                .GetSnapshotLicenseProductConfigurationByLicenseProductConfigurationId(
                                    id);

                        //Delete licenseProductConfiguration
                        _licenseProductConfigurationRepository.DeleteLicenseProductConfigurationBySnapshot(licenseProductConfig);
                    }

                    //delete recConfig
                    _snapshotRecsConfiguration.DeleteWorkRecordingByRecordignSnapshotId(
                        config.SnapshotRecsConfigurationId);
                }
            }
        }
        public void DeleteAllWorksRecordingAndChildren(Snapshot_LicenseProduct licenseProduct)
        {
            
            var workRecordings = _snapshotWorksRecordingRepository.GetAllWorksRecordingsForProductId(licenseProduct.ProductId);
            foreach (var recording in workRecordings)
            {
                //delete works track
                var track = _snapshotWorkTrackRepository.GetTrackForCloneTrackId(recording.CloneTrackId);
                _snapshotWorkTrackRepository.DeleteTrackBySnapshotTrackId(track.SnapshotWorkTrackId);

                //detel licenseProdcut Recording if exists
                var licenseRecording =
                    _snapshotLicenseProductRecordingRepository.GetLicenseProductRecordingForCloneTrackId(
                        recording.CloneTrackId);
                _snapshotLicenseProductRecordingRepository.DeletePhoneBySnapshotPhoneId(
                    licenseRecording.SnapshotLicenseProductRecordingId);
                
                //Delete work writer list and children
                DeleteWorksWritersAndChildren(recording);

                //Delete worksRecordiing
                _snapshotWorksRecordingRepository.DeleteWorkRecordingByRecordignSnapshotId(
                    recording.SnapshotWorksRecodingId);
            }


        }

        public void DeleteWorksWritersAndChildren(Snapshot_WorksRecording recording)
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
                _snapshotWorksWriterRepository.DeleteWorksWriterSnapshotBySnapshotId(writer.SnapshotWorksWriter);
            }
        }

        public void DeleteOriginalPublisherSnapshotAndChildern(Snapshot_WorksWriter writer)
        {
            var allOriginalPublishers =
                _snapshotOriginalPublisherRepository.GetAllOriginalPublishersForCaeCode(writer.CloneCaeNumber);

            foreach (var originalPublisher in allOriginalPublishers)
            {
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
            }
         }

        public void DeleteAllAffiliationsAndChildren(Snapshot_WorksWriter writer)
        { 
            var allAffiliations = _snapshotAffiliationRepository.GetAllAFfiliationsForCAENumber(writer.CloneCaeNumber);
            foreach (var affilation in allAffiliations)
            {
                //get all base
              //  var baseAffiliations = _Not impplememnted
                //delete all base
                _snapshotAffiliationRepository.DeleteAffilationByAffiliationSnapshotId(affilation.SnapshotAffiliationId);
            }
        }

        public void DeleteSnapshotRecsRecordingandChildren(Snapshot_LicenseProduct licenseProduct)
        {
            var licenseConfigurations =
                _snapshotRecsConfiguration.GetAllRecsConfigurationsRecordingsForLicenseProductId(licenseProduct.CloneLicenseProductId);

            foreach (var licenseConfig in licenseConfigurations)
            {
                //Delete Config
                _snapshotConfigurationRepository.DeleteConfigurationSnapshot(licenseProduct.SnapshotLicenseProductId);

                //Delete LicenseProductConfig if exists
                if (licenseConfig.LicenseProductConfigurationId != null)
                {
                    var id = (int)licenseConfig.LicenseProductConfigurationId;
                    var licenseProductConfig =
                        _licenseProductConfigurationRepository
                            .GetSnapshotLicenseProductConfigurationByLicenseProductConfigurationId(
                                id);

                    //Delete licenseProductConfiguration
                    _licenseProductConfigurationRepository.DeleteLicenseProductConfigurationBySnapshot(licenseProductConfig);
                }

                //delete recConfig
                _snapshotRecsConfiguration.DeleteWorkRecordingByRecordignSnapshotId(
                    licenseConfig.SnapshotRecsConfigurationId);
            }
        }

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
                        _snapshotRecsConfiguration.GetAllRecsConfigurationsRecordingsForLicenseProductId(
                            licenseProductId);
                    //Deelte all RecsConfigurations
                    foreach (var rec in recConfigurations)
                    {
                        //delete config
                        _snapshotConfigurationRepository.DeleteConfigurationSnapshot(rec.Configuration.SnapshotConfigId);

                        _snapshotRecsConfiguration.DeleteWorkRecordingByRecordignSnapshotId(
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
                    _snapshotRecsConfiguration.DoesRecConfigurationrecordignsExistForProductHeaderSnapshotId(
                        productHeaderPrimaryKey);
                if (result)
                {
                    var recConfiguationsOnProductHeader =
                        _snapshotRecsConfiguration.GetAllRecsConfigurationsRecordingsForProductHeaderSnapshotId(
                            productHeaderPrimaryKey);
                    foreach (var config in recConfiguationsOnProductHeader)
                    {
                        //delete config
                        _snapshotConfigurationRepository.DeleteConfigurationSnapshot(
                            config.Configuration.SnapshotConfigId);

                        _snapshotRecsConfiguration.DeleteWorkRecordingByRecordignSnapshotId(
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