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


namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
    public class LicenseProductWriterNoteControllerTests
    {
        [Test]
        public void AddLicenseNote_ReturnLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockLicenseProductRecordingWriterNoteManager = A.Fake<ILicenseProductWriterNoteManager>();

            //Build expected
            LicenseProductRecordingWriterNote expected = new LicenseProductRecordingWriterNote { };

            A.CallTo(() => mockLicenseProductRecordingWriterNoteManager.Add(A<LicenseWriterNoteRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductWriterNoteController controller = new LicenseProductWriterNoteController(mockLicenseProductRecordingWriterNoteManager);
            var result =  controller.AddLicenseNote(A<LicenseWriterNoteRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditLicenseNote_ReturnLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockLicenseProductRecordingWriterNoteManager = A.Fake<ILicenseProductWriterNoteManager>();

            //Build expected
            LicenseProductRecordingWriterNote expected = new LicenseProductRecordingWriterNote { };

            A.CallTo(() => mockLicenseProductRecordingWriterNoteManager.Edit(A<LicenseWriterNoteRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductWriterNoteController controller = new LicenseProductWriterNoteController(mockLicenseProductRecordingWriterNoteManager);
            var result = controller.EditLicenseNote(A<LicenseWriterNoteRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditLicenseNote_ReturnListLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockLicenseProductRecordingWriterNoteManager = A.Fake<ILicenseProductWriterNoteManager>();

            //Build expected
            List<LicenseProductRecordingWriterNote> expected = new List<LicenseProductRecordingWriterNote> { };

            A.CallTo(() => mockLicenseProductRecordingWriterNoteManager.GetAll(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductWriterNoteController controller = new LicenseProductWriterNoteController(mockLicenseProductRecordingWriterNoteManager);
            var result = controller.GetAllLicenseNote(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RemoveNote_ReturnLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockLicenseProductRecordingWriterNoteManager = A.Fake<ILicenseProductWriterNoteManager>();

            //Build expected
            LicenseProductRecordingWriterNote expected = new LicenseProductRecordingWriterNote { };

            A.CallTo(() => mockLicenseProductRecordingWriterNoteManager.Remove(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductWriterNoteController controller = new LicenseProductWriterNoteController(mockLicenseProductRecordingWriterNoteManager);
            var result = controller.RemoveNote(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
