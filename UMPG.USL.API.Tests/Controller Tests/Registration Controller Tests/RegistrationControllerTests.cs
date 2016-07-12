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
using UMPG.USL.API.Business.Contacts;
using UMPG.USL.Models.RegistrationModel;
using UMPG.USL.API.Controllers.RegistrationCTRL;

namespace UMPG.USL.API.Tests.Controller_Tests
{
    [TestFixture]
    public class RegistrationControllerTests
    {
        [Test]
        public void Register_ReturnContactRegistration()
        {
            //Arrange
            var mockContactManager = A.Fake<IContactManager>();

            //Build expected
            RegistrationResult expected = new RegistrationResult { };

            A.CallTo(() => mockContactManager.Register(A<ContactRegistration>.Ignored)).Returns(expected);

            //Call  
            RegistrationController controller = new RegistrationController(mockContactManager);
            var result = controller.Register(A<ContactRegistration>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
