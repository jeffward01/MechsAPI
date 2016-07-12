using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Licenses
{
    public class LicenseeManager : ILicenseeManager
    {

        private readonly ILicenseeRepository _licenseeRepository;
        private readonly IContactRepository _contactRepository;
        private readonly IAddressRepository _addressRepository;

        public LicenseeManager(ILicenseeRepository licenseeRepository, IContactRepository contactRepository, IAddressRepository addressRepository)
        {
            _licenseeRepository = licenseeRepository;
            _contactRepository = contactRepository;
            _addressRepository = addressRepository;

        }

        public Licensee Get(int id)
        {
            return _licenseeRepository.Get(id);
        }

        public List<Licensee> GetAll()
        {
            var licensees = _licenseeRepository.GetAll();
            
            return licensees;
        } 

        //public Licensee Add(Licensee licensee)
        //{
        //    return _licenseeRepository.Add(licensee);
        //}

        public List<Licensee> Search(string query)
        {
            return _licenseeRepository.Search(query);
            
        }

        public PagedResponse<Licensee> GetLicensees(LicenseeAdminRequest request)
        {
            return _licenseeRepository.GetLicensees(request);
        }

        public Licensee AddLicensee(AddLicenseeRequest request)
        {
            Licensee newLicensee = new Licensee();
            newLicensee.Name = request.Name;
            newLicensee.CreatedBy = request.CreatedBy;
            newLicensee.ContactId = request.CreatedBy;
            newLicensee.CreatedDate = DateTime.Now;
            newLicensee.IsActive = true;
            return _licenseeRepository.Add(newLicensee);
        }

        public Licensee EditLicensee(Licensee request)
        {
            var lLicensee = _licenseeRepository.Get(request.LicenseeId);
            lLicensee.Name = request.Name;
            lLicensee.ModifiedDate = DateTime.Now;
            lLicensee.ModifiedBy = request.ModifiedBy;
            return _licenseeRepository.EditLicensee(lLicensee);
        }

        public bool DeleteLicensee(Licensee licensee)
        {
            var lLicensee = _licenseeRepository.Get(licensee.LicenseeId);
            foreach (var contact in licensee.LicenseeContactsFiltered)
            {
                var lcontact = _contactRepository.Get(contact.ContactId);
                lcontact.Deleted = DateTime.Now;
                lcontact.ModifiedBy = licensee.ModifiedBy;
                var deletedContact = _contactRepository.EditContact(lcontact);

            }
            lLicensee.Deleted = DateTime.Now;
            lLicensee.ModifiedDate = DateTime.Now;
            if (licensee.LicenseeLabelGroup.Count > 0)
            {
                foreach (var licenseeLabelGroup in licensee.LicenseeLabelGroup)
                {
                    licenseeLabelGroup.Deleted = DateTime.Now;
                }
            }
            var l = _licenseeRepository.EditLicensee(lLicensee);
            return true;

        }
    }
}
