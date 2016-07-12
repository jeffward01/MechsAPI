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
    public class LicenseProductWriterNoteManagerTests
    {
        [Test]
        public void AddLicenseWriterRequest_ReturnLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();

            //Build expected
            LicenseProductRecordingWriterNote expected = new LicenseProductRecordingWriterNote { };

            //Build Request
            LicenseWriterNoteRequest request = new LicenseWriterNoteRequest
            {
                LicenseWriterId = 99,
                Configuration_id = 99,
                Note = "string"
            };
            LicenseProductRecordingWriterNote newNote = new LicenseProductRecordingWriterNote
            {
                LicenseWriterId = request.LicenseWriterId,
                Configuration_Id = request.Configuration_id,
                CreatedDate = DateTime.Now,
                Note = request.Note
            };
            LicenseProductRecordingWriterNote returned = new LicenseProductRecordingWriterNote { LicenseWriterNoteId  = 99};


            A.CallTo(() => mockILicensePRWriterNoteRepository.Add(newNote)).WithAnyArguments().Returns(returned);
            A.CallTo(() => mockILicensePRWriterNoteRepository.Get(returned.LicenseWriterNoteId)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductWriterNoteManager manager = new LicenseProductWriterNoteManager(mockILicensePRWriterNoteRepository);
            var result = manager.Add(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void EditLicenseWriterRequest_ReturnLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();

            //Build expected
            LicenseProductRecordingWriterNote expected = new LicenseProductRecordingWriterNote { };

            //Build Request
            LicenseWriterNoteRequest request = new LicenseWriterNoteRequest
            {
                LicenseWriterId = 99,
                Configuration_id = 99,
                Note = "string"
            };
            LicenseProductRecordingWriterNote newNote = new LicenseProductRecordingWriterNote
            {
                LicenseWriterId = request.LicenseWriterId,
                Configuration_Id = request.Configuration_id,
                CreatedDate = DateTime.Now,
                Note = request.Note
            };
            LicenseProductRecordingWriterNote returned = new LicenseProductRecordingWriterNote { LicenseWriterNoteId = 99 };


            A.CallTo(() => mockILicensePRWriterNoteRepository.Add(newNote)).WithAnyArguments().Returns(returned);
            A.CallTo(() => mockILicensePRWriterNoteRepository.Get(returned.LicenseWriterNoteId)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductWriterNoteManager manager = new LicenseProductWriterNoteManager(mockILicensePRWriterNoteRepository);
            var result = manager.Edit(request);

            //Assert
            Assert.IsInstanceOf(typeof(LicenseProductRecordingWriterNote), result);
        }

        [Test]
        public void GetLicenseWriterRequest_ReturnLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();

            //Build expected
            LicenseProductRecordingWriterNote expected = new LicenseProductRecordingWriterNote { };

            //Build Request
            LicenseWriterNoteRequest request = new LicenseWriterNoteRequest
            {
                LicenseWriterId = 99,
                Configuration_id = 99,
                Note = "string"
            };
            LicenseProductRecordingWriterNote newNote = new LicenseProductRecordingWriterNote
            {
                LicenseWriterId = request.LicenseWriterId,
                Configuration_Id = request.Configuration_id,
                CreatedDate = DateTime.Now,
                Note = request.Note
            };
            LicenseProductRecordingWriterNote returned = new LicenseProductRecordingWriterNote { LicenseWriterNoteId = 99 };


            A.CallTo(() => mockILicensePRWriterNoteRepository.Add(newNote)).WithAnyArguments().Returns(returned);
            A.CallTo(() => mockILicensePRWriterNoteRepository.Get(returned.LicenseWriterNoteId)).WithAnyArguments().Returns(expected);

            //Act
            LicenseProductWriterNoteManager manager = new LicenseProductWriterNoteManager(mockILicensePRWriterNoteRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAllLicenseWriterRequest_ReturnLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();

            //Build expected
            List<LicenseProductRecordingWriterNote> expected = new List<LicenseProductRecordingWriterNote> { };

            //Build Request
            LicenseWriterNoteRequest request = new LicenseWriterNoteRequest
            {
                LicenseWriterId = 99,
                Configuration_id = 99,
                Note = "string"
            };
     
            LicenseProductRecordingWriterNote returned = new LicenseProductRecordingWriterNote { LicenseWriterNoteId = 99 };
            
            A.CallTo(() => mockILicensePRWriterNoteRepository.GetAll(A<int>.Ignored)).WithAnyArguments().Returns(expected);
            
            //Act
            LicenseProductWriterNoteManager manager = new LicenseProductWriterNoteManager(mockILicensePRWriterNoteRepository);
            var result = manager.GetAll(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void RemoveLicenseWriterRequest_ReturnLicenseProductRecordingWriterNote()
        {
            //Arrange
            var mockILicensePRWriterNoteRepository = A.Fake<ILicensePRWriterNoteRepository>();

            //Build expected
            LicenseProductRecordingWriterNote expected = new LicenseProductRecordingWriterNote { };

            //Build Request
            LicenseWriterNoteRequest request = new LicenseWriterNoteRequest
            {
                LicenseWriterId = 99,
                Configuration_id = 99,
                Note = "string"
            };

            LicenseProductRecordingWriterNote returned = new LicenseProductRecordingWriterNote { LicenseWriterNoteId = 99 };

            A.CallTo(() => mockILicensePRWriterNoteRepository.Update(A<LicenseProductRecordingWriterNote>.Ignored)).WithAnyArguments();

            //Act
            LicenseProductWriterNoteManager manager = new LicenseProductWriterNoteManager(mockILicensePRWriterNoteRepository);
            var result = manager.Remove(A<int>.Ignored);

            //Assert
            Assert.IsInstanceOf(typeof(LicenseProductRecordingWriterNote), result);
        }
    }
}
