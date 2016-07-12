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

namespace UMPG.USL.API.Tests.Controller_Tests.RECs_Controller_Tests
{
    [TestFixture]
    public class LabelsControllerTests
    {
        [Test]
        public void GetAll_ReturnListLabel()
        {
            //Arrange
            var mockLabelManager = A.Fake<ILabelManager>();

            //Build expected
            List<Label> expected = new List<Label> { };

            A.CallTo(() => mockLabelManager.GetAll()).Returns(expected);

            //Call  
            LabelController controller = new LabelController(mockLabelManager);
            var result = controller.Get();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetPublishers_ReturnListPublisher()
        {
            //Arrange
            var mockLabelManager = A.Fake<ILabelManager>();

            //Build expected
            List<Publisher> expected = new List<Publisher> { };

            A.CallTo(() => mockLabelManager.GetPublishers(A<string>.Ignored)).Returns(expected);

            //Call  
            LabelController controller = new LabelController(mockLabelManager);
            var result = controller.GetPublishers(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetRecsConfigurations_ReturnLisConfiguration()
        {
            //Arrange
            var mockLabelManager = A.Fake<ILabelManager>();

            //Build expected
            List<Configuration> expected = new List<Configuration> { };

            A.CallTo(() => mockLabelManager.GetRecsConfigurations()).Returns(expected);

            //Call  
            LabelController controller = new LabelController(mockLabelManager);
            var result = controller.GetRecsConfigurations();

            //Assert
            Assert.AreEqual(expected, result);
        }

    }
}
