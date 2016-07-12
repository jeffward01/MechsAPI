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
    public class LabelManagerTests
    {
        [Test]
        public void GetAll_ReturnListLabel()
        {
            //Arrange
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build Expected
            List<Label> expected = new List<Label> { };

            A.CallTo(() => mockIRecsDataProvider.GetLabels()).Returns(expected);

            //Act
            LabelManager manager = new LabelManager(mockIRecsDataProvider);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetPublishers_ReturnLisPublisher()
        {
            //Arrange
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build Expected
            List<Publisher> expected = new List<Publisher> { };

            A.CallTo(() => mockIRecsDataProvider.GetPublshers(A<string>.Ignored)).Returns(expected);

            //Act
            LabelManager manager = new LabelManager(mockIRecsDataProvider);
            var result = manager.GetPublishers(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetRecsConfigurations_ReturnListConfiguration()
        {
            //Arrange
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build Expected
            List<Configuration> expected = new List<Configuration> { };

            A.CallTo(() => mockIRecsDataProvider.GetRecsConfigurations()).Returns(expected);

            //Act
            LabelManager manager = new LabelManager(mockIRecsDataProvider);
            var result = manager.GetRecsConfigurations();

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}
