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
        public SnapshotLicenseManager(ISnapshotLicenseRepository snapshotLicenseRepository, ISnapshotLicenseProductRepository snapshotLicenseProductRepository, ISnapshotLicenseNoteRepository snapshotLicenseNoteRepository, ISnapshotContactRepository snapshotContactRepository)
        {
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
                var roleId = 

                //delete role 
                //delet address
                //delete phone 
                //dete contact emailsdsgh


            }s


            

            return _snapshotLicenseRepository.DeleteSnapshotLicense(licenseId);
        }
    }
}