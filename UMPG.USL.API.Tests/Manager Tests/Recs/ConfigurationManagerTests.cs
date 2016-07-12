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
using UMPG.USL.API.Data.Recs;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.Models.Security;

namespace UMPG.USL.API.Tests.Manager_Tests.Recs
{
    [TestFixture]
    public class ConfigurationManagerTests
    {
        [Test]
        public void GetConfigurations_ReturnListRecsConfigurations()
        {
            //Arrange
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            List<RecsConfigurations> expected = new List<RecsConfigurations> { };

            A.CallTo(() => mockIRecsDataProvider.RetrieveConfigurations()).Returns(expected);

            //Act
            ConfigurationManager manager = new ConfigurationManager(mockIRecsDataProvider);
            var result = manager.GetConfigurations();

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
