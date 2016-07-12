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
using UMPG.USL.Models.LookupModel;


namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
    public class LicenseNoteControllerTests
    {
        [Test]
        public void Search_ReturnListLicenseNote()
        {
            //Arrange
            var mockLicenseNoteManager = A.Fake<ILicenseNoteManager>();

            //Build expected
            List<LicenseNote> expected = new List<LicenseNote> { };

            A.CallTo(() => mockLicenseNoteManager.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteController controller = new LicenseNoteController(mockLicenseNoteManager);
            var result = controller.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddLicenseNote_ReturnLicenseNote()
        {
            //Arrange
            var mockLicenseNoteManager = A.Fake<ILicenseNoteManager>();

            //Build expected
           LicenseNote expected = new LicenseNote { };

            A.CallTo(() => mockLicenseNoteManager.Add(A<LicenseNoteRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteController controller = new LicenseNoteController(mockLicenseNoteManager);
            var result = controller.AddLicenseNote(A<LicenseNoteRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseNotes_ReturnListLicenseNote()
        {
            //Arrange
            var mockLicenseNoteManager = A.Fake<ILicenseNoteManager>();

            //Build expected
            List<LicenseNote> expected = new List<LicenseNote> { };

            A.CallTo(() => mockLicenseNoteManager.GetLicenseNotes(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteController controller = new LicenseNoteController(mockLicenseNoteManager);
            var result = controller.GetLicenseNotes(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseNote_ReturnLicenseNote()
        {
            //Arrange
            var mockLicenseNoteManager = A.Fake<ILicenseNoteManager>();

            //Build expected
            LicenseNote expected = new LicenseNote { };

            A.CallTo(() => mockLicenseNoteManager.GetLicenseNote(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteController controller = new LicenseNoteController(mockLicenseNoteManager);
            var result = controller.GetLicenseNote(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseNoteTypes_ReturnListLU_NoteType()
        {
            //Arrange
            var mockLicenseNoteManager = A.Fake<ILicenseNoteManager>();

            //Build expected
            List<LU_NoteType> expected = new List<LU_NoteType> { };

            A.CallTo(() => mockLicenseNoteManager.GetLicenseNoteTypes()).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteController controller = new LicenseNoteController(mockLicenseNoteManager);
            var result = controller.GetLicenseNoteTypes();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DeleteLicenseNotes_ReturBoolTRUE()
        {
            //Arrange
            var mockLicenseNoteManager = A.Fake<ILicenseNoteManager>();

            //Build expected
            const bool expected = true;

            A.CallTo(() => mockLicenseNoteManager.UpdateLicenseNote(A<LicenseNote>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteController controller = new LicenseNoteController(mockLicenseNoteManager);
            var result = controller.EditLicenseNote(A<LicenseNote>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
