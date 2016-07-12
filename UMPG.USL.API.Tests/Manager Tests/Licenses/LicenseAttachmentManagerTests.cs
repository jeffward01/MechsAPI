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

namespace UMPG.USL.API.Tests.Manager_Tests.Licenses
{
    [TestFixture]
    public class LicenseAttachmentManagerTests
    {
        [Test]
        public void GetLicenseAttachment_ReturnLicesneAttachement()
        {
            //Arrange
            var mockLicenseAttachementRepository = A.Fake<ILicenseAttachmentRepository>();

            //Build expected
            LicenseAttachment expected = new LicenseAttachment { };

            A.CallTo(() => mockLicenseAttachementRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseAttachmentManager manager = new LicenseAttachmentManager(mockLicenseAttachementRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAll_ReturnListLicesneAttachement()
        {
            //Arrange
            var mockLicenseAttachementRepository = A.Fake<ILicenseAttachmentRepository>();

            //Build expected
            List<LicenseAttachment> expected = new List<LicenseAttachment> { };

            A.CallTo(() => mockLicenseAttachementRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            LicenseAttachmentManager manager = new LicenseAttachmentManager(mockLicenseAttachementRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAllAttachmentsByLicenseId_ReturnListLicesneAttachement()
        {
            //Arrange
            var mockLicenseAttachementRepository = A.Fake<ILicenseAttachmentRepository>();

            //Build expected
            List<LicenseAttachment> expected = new List<LicenseAttachment> { };

            A.CallTo(() => mockLicenseAttachementRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            LicenseAttachmentManager manager = new LicenseAttachmentManager(mockLicenseAttachementRepository);
            var result = manager.GetAllAttachmentsByLicenseId(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseAttachement_ReturnLicesneAttachement()
        {
            //Arrange
            var mockLicenseAttachementRepository = A.Fake<ILicenseAttachmentRepository>();

            //Build expected
            LicenseAttachment expected = new LicenseAttachment { };

            A.CallTo(() => mockLicenseAttachementRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseAttachmentManager manager = new LicenseAttachmentManager(mockLicenseAttachementRepository);
            var result = manager.GetLicenseAttachement(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddLicenseAttachment_ReturnVoid()
        {
            //Arrange
            var mockLicenseAttachementRepository = A.Fake<ILicenseAttachmentRepository>();

            //Build expected
            LicenseAttachment expected = new LicenseAttachment { };

            //Build request
            LicenseAttachment request = new LicenseAttachment { fileName = "test", licenseId = 99 };

            A.CallTo(() => mockLicenseAttachementRepository.Get(A<string>.Ignored,A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseAttachmentManager manager = new LicenseAttachmentManager(mockLicenseAttachementRepository);
            manager.AddLicenseAttachment(request);

            //Assert
            A.CallTo(() => mockLicenseAttachementRepository.Get(A<string>.Ignored, A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void RemoveLicenseAttachment_ReturnVoid()
        {
            //Arrange
            var mockLicenseAttachementRepository = A.Fake<ILicenseAttachmentRepository>();

            //Build expected
            LicenseAttachment expected = new LicenseAttachment { };

            //Build request
            LicenseAttachment request = new LicenseAttachment { fileName = "test", licenseId = 99 };

            A.CallTo(() => mockLicenseAttachementRepository.Get(A<string>.Ignored, A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseAttachmentManager manager = new LicenseAttachmentManager(mockLicenseAttachementRepository);
            manager.RemoveLicenseAttachment(request);

            //Assert
            A.CallTo(() => mockLicenseAttachementRepository.Get(A<string>.Ignored, A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }


        [Test]
        public void Search_ReturnListLicesneAttachement()
        {
            //Arrange
            var mockLicenseAttachementRepository = A.Fake<ILicenseAttachmentRepository>();

            //Build expected
            List<LicenseAttachment> expected = new List<LicenseAttachment> { };

            A.CallTo(() => mockLicenseAttachementRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseAttachmentManager manager = new LicenseAttachmentManager(mockLicenseAttachementRepository);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
