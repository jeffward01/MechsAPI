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
using UMPG.USL.API.Business.Lookups;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.API.Controllers.LookUpCTRL;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Controllers.RECsCTRL;
using UMPG.USL.API.Business;
using System.Web.Http;
using UMPG.USL.API.Controllers;
 
using UMPG.USL.Security.Safe;
using System.Net;
using UMPG.USL.API.Data.AuditData;


namespace UMPG.USL.API.Tests.Repository_Tests
{
    [TestFixture]
   public class AuditLicenseProductConfigurationRepositoryTests
    {
        [Test]
        public void GetLicenseProductConfiguration_ReturnAuditLicenseProductConfiguration()
        {
            //Arrange
            var mockAuditLicenseProductConfigurationRepository = A.Fake<IAuditLicenseProductConfigurationRepository>();

            //Build expected
            AuditLicenseProductConfiguration expected = new AuditLicenseProductConfiguration { };

            A.CallTo(() => mockAuditLicenseProductConfigurationRepository.GetLicenseProductConfiguration(A<int>.Ignored, A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAuditLicenseProductConfigurationRepository.GetLicenseProductConfiguration(A<int>.Ignored, A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAuditLicenseProductConfigurationRepository.GetLicenseProductConfiguration(A<int>.Ignored, A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetLicenseProductConfiguration_ReturnListAuditLicenseProductConfiguration()
        {
            //Arrange
            var mockAuditLicenseProductConfigurationRepository = A.Fake<IAuditLicenseProductConfigurationRepository>();

            //Build expected
            List<AuditLicenseProductConfiguration> expected = new List<AuditLicenseProductConfiguration> { };

            A.CallTo(() => mockAuditLicenseProductConfigurationRepository.GetLicenseProductConfigurations(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAuditLicenseProductConfigurationRepository.GetLicenseProductConfigurations(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAuditLicenseProductConfigurationRepository.GetLicenseProductConfigurations(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void Get_ReturnAuditLicenseProductConfiguration()
        {
            //Arrange
            var mockAuditLicenseProductConfigurationRepository = A.Fake<IAuditLicenseProductConfigurationRepository>();

            //Build expected
            AuditLicenseProductConfiguration expected = new AuditLicenseProductConfiguration { };

            A.CallTo(() => mockAuditLicenseProductConfigurationRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAuditLicenseProductConfigurationRepository.Get(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAuditLicenseProductConfigurationRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetLicenseConfigurationList_ReturnListAuditLicenseProductConfiguration()
        {
            //Arrange
            var mockAuditLicenseProductConfigurationRepository = A.Fake<IAuditLicenseProductConfigurationRepository>();

            //Build expected
            List<AuditLicenseProductConfiguration> expected = new List<AuditLicenseProductConfiguration> { };

            A.CallTo(() => mockAuditLicenseProductConfigurationRepository.GetLicenseConfigurationList(A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAuditLicenseProductConfigurationRepository.GetLicenseConfigurationList(A<List<int>>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAuditLicenseProductConfigurationRepository.GetLicenseConfigurationList(A<List<int>>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetProductIdsWithConfiguration_ReturnListAuditLicenseProductConfiguration()
        {
            //Arrange
            var mockAuditLicenseProductConfigurationRepository = A.Fake<IAuditLicenseProductConfigurationRepository>();

            //Build expected
            List<int> expected = new List<int> { };

            A.CallTo(() => mockAuditLicenseProductConfigurationRepository.GetProductIdsWithConfiguration(A<List<int>>.Ignored, A<List<int>>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var result = mockAuditLicenseProductConfigurationRepository.GetProductIdsWithConfiguration(A<List<int>>.Ignored, A<List<int>>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockAuditLicenseProductConfigurationRepository.GetProductIdsWithConfiguration(A<List<int>>.Ignored, A<List<int>>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
