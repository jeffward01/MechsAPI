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
using UMPG.USL.API.Business.Contacts;
using UMPG.USL.Models.ContactModel;
using UMPG.USL.API.Controllers.ContactCTRL;
using UMPG.USL.Models.LicenseModel;


namespace UMPG.USL.API.Tests.Controller_Tests.Contact_Controller_Tests
{
    [TestFixture]
    public class ContactsControllerTests
    {
        [Test]
        public void GetAllContacts_ReturnListContact()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactManager.GetAll()).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.Get();

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void GetAssignees_ReturnListContact()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactManager.GetAssignees()).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.GetAssignees();

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void Search_ReturnListContact()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactManager.Search(A<string>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void Add_ReturnContact()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            Contact expected = new Contact { };

            A.CallTo(() => mockContactManager.Add(A<Contact>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.Add(A<Contact>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void GetProductLicenses_ReturnListContact()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactManager.GetContactsForLicensee(A<int>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.GetProductLicenses(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void GetContactsWithRole_ReturnListContact()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            List<Contact> expected = new List<Contact> { };

            A.CallTo(() => mockContactManager.GetContactsWithRole(A<int>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.GetContactsWithRole(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void GetLabelsForLicensee_ReturnListLicenseeLabelGroup()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            List<LicenseeLabelGroup> expected = new List<LicenseeLabelGroup> { };

            A.CallTo(() => mockContactManager.GetLabelsForLicensee(A<int>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.GetLabelsForLicensee(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void GetContactsForLicenseeLabel_ReturnListLicenseeLabelGroupLink()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            List<LicenseeLabelGroupLink> expected = new List<LicenseeLabelGroupLink> { };

            A.CallTo(() => mockContactManager.GetContactsForLicenseeLabel(A<int>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.GetContactsForLicenseeLabel(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void GetAllLabelGroups_ReturnListLicenseeLabelGroup()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            List<LicenseeLabelGroup> expected = new List<LicenseeLabelGroup> { };

            A.CallTo(() => mockContactManager.GetAllLabelGroups()).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.GetAllLabelGroups();

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void EditContact_ReturnContact()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            Contact expected = new Contact { };

            A.CallTo(() => mockContactManager.EditContact(A<Contact>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.EditContact(A<Contact>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void AddLabelGroup_ReturnLicenseeLabelGroup()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            LicenseeLabelGroup expected = new LicenseeLabelGroup { };

            A.CallTo(() => mockContactManager.AddLabelGroup(A<LicenseeLabelGroup>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.AddLabelGroup(A<LicenseeLabelGroup>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void EditLabelGroup_ReturnLicenseeLabelGroup()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            LicenseeLabelGroup expected = new LicenseeLabelGroup { };

            A.CallTo(() => mockContactManager.EditLabelGroup(A<LicenseeLabelGroup>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.EditLabelGroup(A<LicenseeLabelGroup>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void DeleteLabelGroup_ReturnLicenseeLabelGroup()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            LicenseeLabelGroup expected = new LicenseeLabelGroup { };

            A.CallTo(() => mockContactManager.DeleteLabelGroup(A<LicenseeLabelGroup>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.DeleteLabelGroup(A<LicenseeLabelGroup>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void AddContactAndLink_ReturnContact()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            Contact expected = new Contact { };

            A.CallTo(() => mockContactManager.AddContactAndLink(A<AddContactAndLinqRequest>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.AddContactAndLink(A<AddContactAndLinqRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void AddLicenseeContactAndLink_ReturnContact()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            Contact expected = new Contact { };

            A.CallTo(() => mockContactManager.AddLicenseeContactAndLink(A<AddLicenseeContactAndLinqRequest>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.AddContactAndLink(A<AddLicenseeContactAndLinqRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void DeleteContactAndLink_Returnbool()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            bool expected = true;

            A.CallTo(() => mockContactManager.DeleteContactandLink(A<DeleteContactRequest>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.DeleteContactAndLink(A<DeleteContactRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void DeleteLicenseeContactAndLink_Returnbool()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            bool expected = true;

            A.CallTo(() => mockContactManager.DeleteLicenseeContactAndLink(A<DeleteContactRequest>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.DeleteLicenseeContactAndLink(A<DeleteContactRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void EmailExists_Returnbool()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build Expected
            bool expected = true;

            A.CallTo(() => mockContactManager.EmailExists(A<string>.Ignored, A<int>.Ignored)).Returns(expected);

            //Act
            ContactController contactController = new ContactController(mockContactManager);
            var returned = contactController.EmailExists(A<string>.Ignored, A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }
    }
}
