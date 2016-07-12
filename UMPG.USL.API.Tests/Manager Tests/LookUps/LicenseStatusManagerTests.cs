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
using UMPG.USL.API.Business.Lookups;
using UMPG.USL.Models.LookupModel;
using UMPG.USL.API.Data.LookupData;


namespace UMPG.USL.API.Tests.Manager_Tests.LookUps
{

    [TestFixture]
    public class LicenseStatusManagerTests
    {
        [Test]
        public void Get_ReturnLicenseStatus()
        {
            //Arrange
            var mockILicenseStatusRepository = A.Fake<ILicenseStatusRepository>();

            //Build expected
            LU_LicenseStatus expected = new LU_LicenseStatus { };

            A.CallTo(() => mockILicenseStatusRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseStatusManager manager = new LicenseStatusManager(mockILicenseStatusRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Add_ReturnLicenseStatus()
        {
            //Arrange
            var mockILicenseStatusRepository = A.Fake<ILicenseStatusRepository>();

            //Build expected
            LU_LicenseStatus expected = new LU_LicenseStatus { };

            A.CallTo(() => mockILicenseStatusRepository.Add(A<LU_LicenseStatus>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            LicenseStatusManager manager = new LicenseStatusManager(mockILicenseStatusRepository);
            var result = manager.Add(A<LU_LicenseStatus>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAll_ReturnListLicenseStatus()
        {
            //Arrange
            var mockILicenseStatusRepository = A.Fake<ILicenseStatusRepository>();

            //Build expected
            List<LU_LicenseStatus> expected = new List<LU_LicenseStatus> { };

            A.CallTo(() => mockILicenseStatusRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            LicenseStatusManager manager = new LicenseStatusManager(mockILicenseStatusRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
