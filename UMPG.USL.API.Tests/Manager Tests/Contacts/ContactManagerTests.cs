using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using NUnit;
using NUnit.Framework;
using UMPG.USL.API.Business.Audits;
using UMPG.USL.Models;
using UMPG.USL.Models.AuditModel;
using UMPG.USL.Models.Recs;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.API.Controllers.LicenseCTRL;
using UMPG.USL.Common;
using System.Net.Http;
using System.Web.Http;
 
using System.Net;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.API.Data.AuditData;
using UMPG.USL.API.Data.ContactData;
using UMPG.USL.Security;
using UMPG.USL.API.Business.Contacts;
using UMPG.USL.Models.RegistrationModel;

namespace UMPG.USL.API.Tests.Manager_Tests.Contacts
{
    [TestFixture]
    public class ContactManagerTests
    {
        [Test]
        public void GetAllContacts_ReturnListContacts()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactRepository.GetAll()).Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreSame(expected, result);
        }

        [Test]
        public void GetAssignees_ReturnListContacts()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactRepository.GetAssignees()).Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.GetAssignees();

            //Assert
            Assert.AreSame(expected, result);
        }

        [Test]
        public void DoesEmailExist_ReturnBoolTRUE()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            const bool expected = true;

            A.CallTo(() => mockContactRepository.EmailExists(A<string>.Ignored, A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.EmailExists(A<string>.Ignored, A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
            Assert.IsTrue(result);
        }

        [Test]
        public void GetContactByID_ReturnContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            Contact expected = new Contact { } ;

            A.CallTo(() => mockContactRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.Get( A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddContact_ReturnContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            Contact expected = new Contact { };

            A.CallTo(() => mockContactRepository.Add(A<Contact>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.Add(A<Contact>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddContact_ReturnListContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetContactEmail_ReturnContactEmail()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            ContactEmail expected = new ContactEmail { };

            A.CallTo(() => mockContactEmailRepository.GetContactEmail(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.GetContactEmail(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetContactsWithRole_ReturnListContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactRepository.GetContactsWithRole(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.GetContactsWithRole(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetContactsWithRole_ReturnListLicenseeLabelGroup()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            List<LicenseeLabelGroup> expected = new List<LicenseeLabelGroup> { };

            A.CallTo(() => mockContactRepository.GetAlLabelGroups()).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.GetAllLabelGroups();

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetLabelsForLicensee_ReturnListLicenseeLabelGroup()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            List<LicenseeLabelGroup> expected = new List<LicenseeLabelGroup> { };

            A.CallTo(() => mockContactRepository.GetLabelsForLicensee(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.GetLabelsForLicensee(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLabelsForLicensee_ReturnListLicenseeLabelGroupLink()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            List<LicenseeLabelGroupLink> expected = new List<LicenseeLabelGroupLink> { };

            A.CallTo(() => mockContactRepository.GetContactsForLicenseeLabel(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.GetContactsForLicenseeLabel(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetContactsForLicensee_ReturnListContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactRepository.GetContactsForLicensee(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.GetContactsForLicensee(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetContactsForLicensee_ReturnRegistrationResult()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            Contact contact = new Contact { };
            RegistrationResult regResult = new RegistrationResult { };
            Contact conact1 = new Contact { };
            ContactDefault contactDefault = new ContactDefault { };

            //Build Expected
            ContactRegistration request = new ContactRegistration
            {
                EmailAddress = "string@example.com"
            };

            A.CallTo(() => mockContactEmailRepository.Get(A<string>.Ignored)).WithAnyArguments().Returns(contact);
            A.CallTo(() => mockRegistrationHandler.RegisterExternal(A<ExternalContactRegistration>.Ignored)).WithAnyArguments().Returns(regResult);
            A.CallTo(() => mockContactRepository.Add(A<Contact>.Ignored)).WithAnyArguments().Returns(conact1);
            A.CallTo(() => mockContactDefaultRepository.Add(A<ContactDefault>.Ignored)).WithAnyArguments().Returns(contactDefault);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.Register(request);

            //Assert
            //Assert.AreSame(regResult, result);
            //Assert.AreEqual(regResult, result);
            Assert.IsInstanceOf(typeof(RegistrationResult), result);
        }


        [Test]
        public void EditContact_ReturnListContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            Contact expected = new Contact { };

            //Build Request
              List<Phone> phoneList = new List<Phone>{};
            List<Address> addressList = new List<Address>{};
            List<ContactEmail> contactEmailList = new List<ContactEmail>{};

            Contact request = new Contact
            {
                ContactId = 99,
                FirstName = "Test",
                LastName = "Test",
                ModifiedBy = 99,
                Phone = phoneList,
                Address = addressList,
                Email = contactEmailList
            };
            
            A.CallTo(() => mockContactRepository.EditContact(request)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.EditContact(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddLabelGroup_ReturnLicenseeLabelGroup()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            LicenseeLabelGroup expected = new LicenseeLabelGroup { };

            //Build request
            LicenseeLabelGroup request = new LicenseeLabelGroup { };

            A.CallTo(() => mockContactRepository.AddLabelGroup(A<LicenseeLabelGroup>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.AddLabelGroup(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditLabelGroup_ReturnLicenseeLabelGroup()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            LicenseeLabelGroup expected = new LicenseeLabelGroup { };

            //Build request
            LicenseeLabelGroup request = new LicenseeLabelGroup { };

            A.CallTo(() => mockContactRepository.EditLabelGroup(A<LicenseeLabelGroup>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.EditLabelGroup(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DeleteLabelGroup_ReturnLicenseeLabelGroup()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            LicenseeLabelGroup expected = new LicenseeLabelGroup { };

            //Build Request
            LicenseeLabelGroup request = new LicenseeLabelGroup { ModifiedDate = DateTime.Now, Deleted = DateTime.Now };

            A.CallTo(() => mockContactRepository.EditLabelGroup(request)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.DeleteLabelGroup(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddLicenseeContactAndLink_ReturnContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            Contact expected = new Contact { };
            
            //Build Request
            List<Phone> phoneList = new List<Phone>{};
            List<Address> addressList = new List<Address> { };
            List<ContactEmail> contactEmailList = new List<ContactEmail> { };
            Contact contact = new Contact { FirstName = "Test", LastName = "test", FullName = "Test", Phone = phoneList, Email = contactEmailList, Address = addressList };
            AddLicenseeContactAndLinqRequest request = new AddLicenseeContactAndLinqRequest { Contact =contact, CreatedBy =99, LicenseeId = 99  };

            Contact contact2 = new Contact { };
            const int phoneResult = 99;
            ContactEmail contactEmail = new ContactEmail { };
            const int addressResult = 99;

            A.CallTo(() => mockContactRepository.Add(contact)).WithAnyArguments().Returns(contact2);
            A.CallTo(() => mockPhoneRepository.Add(A<Phone>.Ignored)).WithAnyArguments().Returns(phoneResult);
            A.CallTo(() => mockEmailRepository.Add(A<ContactEmail>.Ignored)).WithAnyArguments().Returns(contactEmail);
            A.CallTo(() => mockAddressRepository.Add(A<Address>.Ignored)).WithAnyArguments().Returns(addressResult);
            A.CallTo(() => mockContactRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.AddLicenseeContactAndLink(request);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void AddContactAndLink_ReturnContact()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            Contact expected = new Contact { };

            //Build Request
            List<Phone> phoneList = new List<Phone> { };
            List<Address> addressList = new List<Address> { };
            List<ContactEmail> contactEmailList = new List<ContactEmail> { };
            Contact contact = new Contact { FirstName = "Test", LastName = "test", FullName = "Test", Phone = phoneList, Email = contactEmailList, Address = addressList };
            AddContactAndLinqRequest request = new AddContactAndLinqRequest { Contact = contact, CreatedBy = 99, LicenseeId = 99 };

            Contact contact2 = new Contact { };
            const int phoneResult = 99;
            ContactEmail contactEmail = new ContactEmail { };
            const int addressResult = 99;

            A.CallTo(() => mockContactRepository.Add(contact)).WithAnyArguments().Returns(contact2);
            A.CallTo(() => mockPhoneRepository.Add(A<Phone>.Ignored)).WithAnyArguments().Returns(phoneResult);
            A.CallTo(() => mockEmailRepository.Add(A<ContactEmail>.Ignored)).WithAnyArguments().Returns(contactEmail);
            A.CallTo(() => mockAddressRepository.Add(A<Address>.Ignored)).WithAnyArguments().Returns(addressResult);
            A.CallTo(() => mockContactRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.AddContactAndLink(request);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void DeleteContactandLink_ReturnBoolTRUE()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            const bool expected = true;
            Contact contactToDelete =new Contact{};
            Contact deletedContact = new Contact { };
            List<LicenseeLabelGroupLink> links = new List<LicenseeLabelGroupLink> { };
            //Build Request
            DeleteContactRequest request = new DeleteContactRequest
            {
                ContactId = 99,
                ModifiedBy = 99,

            };

            A.CallTo(() => mockContactRepository.Get(request.ContactId)).WithAnyArguments().Returns(contactToDelete);
            A.CallTo(() => mockContactRepository.GetLinksForContact(request.ContactId)).WithAnyArguments().Returns(links);
            A.CallTo(() => mockContactRepository.EditContact(contactToDelete)).WithAnyArguments().Returns(deletedContact);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.DeleteContactandLink(request);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void DeleteLicenseeContactAndLink_ReturnBoolTRUE()
        {
            //Arrange
            var mockContactRepository = A.Fake<IContactRepository>();
            var mockContactDefaultRepository = A.Fake<IContactDefaultRepository>();
            var mockRegistrationHandler = A.Fake<IRegistrationHandler>();
            var mockContactEmailRepository = A.Fake<IContactEmailRepository>();

            var mockAddressRepository = A.Fake<IAddressRepository>();
            var mockPhoneRepository = A.Fake<IPhoneRepository>();
            var mockEmailRepository = A.Fake<IContactEmailRepository>();

            //Build Expected
            const bool expected = true;
            Contact contactToDelete = new Contact { };
            Contact deletedContact = new Contact { };
            List<LicenseeLabelGroupLink> links = new List<LicenseeLabelGroupLink> { };
            //Build Request
            DeleteContactRequest request = new DeleteContactRequest
            {
                ContactId = 99,
                ModifiedBy = 99,

            };

            A.CallTo(() => mockContactRepository.Get(request.ContactId)).WithAnyArguments().Returns(contactToDelete);
            A.CallTo(() => mockContactRepository.GetLinksForContact(request.ContactId)).WithAnyArguments().Returns(links);
            A.CallTo(() => mockContactRepository.EditContact(contactToDelete)).WithAnyArguments().Returns(deletedContact);

            //Act
            ContactManager manager = new ContactManager(mockContactRepository, mockContactDefaultRepository, mockContactEmailRepository, mockRegistrationHandler, mockAddressRepository, mockPhoneRepository, mockEmailRepository);
            var result = manager.DeleteLicenseeContactAndLink(request);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
