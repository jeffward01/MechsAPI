using System.Collections.Generic;
using UMPG.USL.Models.ContactModel;

namespace UMPG.USL.API.Data.ContactData
{
    public interface IAddressRepository
    {
        int Add(Address address);

        Address Get(int Id);

        List<Address> GetAll();

        List<Address> Search(string query);


        List<Address> GetAddressesForLicensee(int licenseeId);

        void Update(Address address);
    }
}
