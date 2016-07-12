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
    public class LicensePRWriterRateManagerTests
    {
        [Test]
        public void Get_ReturnLicenseProductRecordingWriterRate()
        {
            //Arrange
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();

            //Build expected
            LicenseProductRecordingWriterRate expected = new LicenseProductRecordingWriterRate { };

            A.CallTo(() => mockILicensePRWriterRateRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicensePRWriterRateManager manager = new LicensePRWriterRateManager(mockILicensePRWriterRateRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetLicenseProductRecordingWriterRates_ReturnLicenseProductRecordingWriterRate()
        {
            //Arrange
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();

            //Build expected
            LicenseProductRecordingWriterRate expected = new LicenseProductRecordingWriterRate { };

            A.CallTo(() => mockILicensePRWriterRateRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicensePRWriterRateManager manager = new LicensePRWriterRateManager(mockILicensePRWriterRateRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository);
            var result = manager.GetLicenseProductRecordingWriterRates(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAll_ReturnLicenseProductRecordingWriterRate()
        {
            //Arrange
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();

            //Build expected
            List<LicenseProductRecordingWriterRate> expected = new List<LicenseProductRecordingWriterRate> { };

            A.CallTo(() => mockILicensePRWriterRateRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            LicensePRWriterRateManager manager = new LicensePRWriterRateManager(mockILicensePRWriterRateRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreSame(expected, result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Add_ReturnLicenseProductRecordingWriterRate()
        {
            //Arrange
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();

            //Build expected
            LicenseProductRecordingWriterRate expected = new LicenseProductRecordingWriterRate { };

            A.CallTo(() => mockILicensePRWriterRateRepository.Add(A<LicenseProductRecordingWriterRate>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicensePRWriterRateManager manager = new LicensePRWriterRateManager(mockILicensePRWriterRateRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository);
            var result = manager.Add(A<LicenseProductRecordingWriterRate>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void Search_ReturnLicenseProductRecordingWriterRate()
        {
            //Arrange
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();

            //Build expected
            List<LicenseProductRecordingWriterRate> expected = new List<LicenseProductRecordingWriterRate> { };

            A.CallTo(() => mockILicensePRWriterRateRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicensePRWriterRateManager manager = new LicensePRWriterRateManager(mockILicensePRWriterRateRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetEditWriterRates_ReturnLicenseProductRecordingWriterRate()
        {
            //Arrange
            var mockILicensePRWriterRateRepository = A.Fake<ILicensePRWriterRateRepository>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicensePRWriterRepository = A.Fake<ILicensePRWriterRepository>();

            //Build request
            List<int> numbers = new List<int>{1,2,3,4};
            GetWritersRatesRequest request = new GetWritersRatesRequest { LicenseConfigIds = numbers, LicenseProductIds = numbers, LicenseWriterIds = numbers  };

            //Build expected
            List<LicenseProductRecordingWriterRate> expected = new List<LicenseProductRecordingWriterRate> { };
            List<LicenseProductRecording> list = new List<LicenseProductRecording> { };
            A.CallTo(() => mockILicensePRWriterRateRepository.GetLicenseRecordingWriterRatesFromIds(A<List<int>>.Ignored, A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicensePRWriterRateManager manager = new LicensePRWriterRateManager(mockILicensePRWriterRateRepository, mockILicenseProductRecordingRepository, mockILicensePRWriterRepository);
            var result = manager.GetEditWriterRates(request);

            //Assert
            Assert.AreSame(expected, result);
            Assert.AreEqual(expected, result);
        }
    }
}
