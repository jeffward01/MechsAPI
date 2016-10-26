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
        public SnapshotLicenseManager(ISnapshotLicenseRepository snapshotLicenseRepository, ISnapshotLicenseProductRepository snapshotLicenseProductRepository, ISnapshotLicenseNoteRepository snapshotLicenseNoteRepository, ISnapshotContactRepository snapshotContactRepository, ISnapshotRoleRepository snapshotRoleRepository, ISnapshotAddressRepository snapshotAddressRepository, ISnapshotPhoneRepository snapshotPhoneRepository, ISnapshotContactEmailRepository snapshotContactEmailRepository)
        {
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
            return _snapshotLicenseRepository.GetLicenseSnapShotById(snapshotLicenseId);
        }

        public bool DeleteLicenseSnapshot(int licenseId)
        {
            var licenseProductIds = _snapshotLicenseProductRepository.GetLicenseProductIds(licenseId);
            //Delete all children of license
            //Delete LicenseProducts
            foreach (var id in licenseProductIds)
            { 
                _snapshotLicenseProductRepository.DeleteLicenseProductSnapshot(id);
                //Delete each child LicenseProducts


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
                var cloneContactId = _snapshotContactRepository.GetCloneContactIdForContactId(contactId);

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