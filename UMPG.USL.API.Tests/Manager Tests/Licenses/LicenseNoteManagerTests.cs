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
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.Models.LicenseGenerate;
using UMPG.USL.API.Data.LookupData;
using UMPG.USL.Models.LookupModel;

namespace UMPG.USL.API.Tests.Manager_Tests.Licenses
{
    [TestFixture]
    public class LicenseNoteManagerTests
    {
        [Test]
        public void GetLicenseNote_ReturnLicenseNote()
        {
            //Arrange
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockNoteTypeRepository = A.Fake<INoteTypeRepository>();

            //Build expected
            LicenseNote expected = new LicenseNote { };

            A.CallTo(() => mockLicenseNoteRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteManager manager = new LicenseNoteManager(mockLicenseNoteRepository, mockNoteTypeRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Add_ReturnLicenseNote()
        {
            //Arrange
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockNoteTypeRepository = A.Fake<INoteTypeRepository>();

            //Build expected
            LicenseNote expected = new LicenseNote { };
            
            //Build requested
            const string licenseNote = "test";

            LicenseNoteRequest requested = new LicenseNoteRequest { LicenseNote = licenseNote, LicenseId = 99, ContactId = 99, NoteTypeId = 99 };

            A.CallTo(() => mockLicenseNoteRepository.Add(A<LicenseNote>.Ignored)).WithAnyArguments().Returns(expected);
            A.CallTo(() => mockLicenseNoteRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteManager manager = new LicenseNoteManager(mockLicenseNoteRepository, mockNoteTypeRepository);
            var result = manager.Add(requested);

            //Assert
            Assert.AreSame(expected, result);
        }

        [Test]
        public void Search_ReturnListLicenseNote()
        {
            //Arrange
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockNoteTypeRepository = A.Fake<INoteTypeRepository>();

            //Build expected
            List<LicenseNote> expected = new List<LicenseNote> { };

            A.CallTo(() => mockLicenseNoteRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);
         
            //Act
            LicenseNoteManager manager = new LicenseNoteManager(mockLicenseNoteRepository, mockNoteTypeRepository);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
        }

        [Test]
        public void GetLicenseNotes_ReturnListLicenseNote()
        {
            //Arrange
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockNoteTypeRepository = A.Fake<INoteTypeRepository>();

            //Build expected
            List<LicenseNote> expected = new List<LicenseNote> { };

            A.CallTo(() => mockLicenseNoteRepository.GetLicenseNotes(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteManager manager = new LicenseNoteManager(mockLicenseNoteRepository, mockNoteTypeRepository);
            var result = manager.GetLicenseNotes(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
        }

        [Test]
        public void GetLicenseNoteTypes_ReturnListLicenseNote()
        {
            //Arrange
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockNoteTypeRepository = A.Fake<INoteTypeRepository>();

            //Build expected
            List<LU_NoteType> expected = new List<LU_NoteType> { };

            A.CallTo(() => mockNoteTypeRepository.GetLicenseNoteTypes()).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteManager manager = new LicenseNoteManager(mockLicenseNoteRepository, mockNoteTypeRepository);
            var result = manager.GetLicenseNoteTypes();

            //Assert
            Assert.AreSame(expected, result);
        }

        [Test]
        public void DeleteLicenseNotes_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockNoteTypeRepository = A.Fake<INoteTypeRepository>();

            //Build expected
            List<LU_NoteType> expected = new List<LU_NoteType> { };

            //Build request
            List<int> request = new List<int> { };

            A.CallTo(() => mockLicenseNoteRepository.UpdateLicenseNote(A<LicenseNote>.Ignored)).WithAnyArguments();

            //Act
            LicenseNoteManager manager = new LicenseNoteManager(mockLicenseNoteRepository, mockNoteTypeRepository);
            var result = manager.DeleteLicenseNotes(request);

            //Assert
            Assert.IsTrue(result);
        }

        [Test]
        public void GetLicenseNote_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseNoteRepository = A.Fake<ILicenseNoteRepository>();
            var mockNoteTypeRepository = A.Fake<INoteTypeRepository>();

            //Build expected
            LicenseNote expected = new LicenseNote { };

            //Build request
            List<int> request = new List<int> { };

            A.CallTo(() => mockLicenseNoteRepository.GetLicenseNote(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseNoteManager manager = new LicenseNoteManager(mockLicenseNoteRepository, mockNoteTypeRepository);
            var result = manager.GetLicenseNote(99);

            //Assert
            Assert.AreSame(expected, result);
        }
    }
}
