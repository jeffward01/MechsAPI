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
using UMPG.USL.API.Business.LookUps;

namespace UMPG.USL.API.Tests.Manager_Tests.LookUps
{
    [TestFixture]
    public class LicenseTypeManagerTests
    {
        [Test]
        public void GetAll_RetrunListLicenseType()
        {
            //Arrange
            var mockILicenseTypeRepository = A.Fake<ILicenseTypeRepository>();

            //Build expected
            List<LU_LicenseType> expected = new List<LU_LicenseType> { };

            A.CallTo(() => mockILicenseTypeRepository.GetAll()).Returns(expected);

            //Act
            LicenseTypeManager manager = new LicenseTypeManager(mockILicenseTypeRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
            
            

        }
    }
}
