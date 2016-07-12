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
using UMPG.USL.API.Controllers.LicenseCTRL;


namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
   public class LicensesSolrControllerTests
    {
       [Test]
       public void SolrUpdateLicenseStatus_ReturnBoolTRUE()
       {
           //Arrange
           var mockLicenseSolrManager = A.Fake<ILicenseSolrManager>();

           //Build expected
           const bool expected = true;

           A.CallTo(() => mockLicenseSolrManager.UpdateLicenseStatus(A<int>.Ignored)).WithAnyArguments().Returns(expected);

           //Act
           LicenseSolrController controller = new LicenseSolrController(mockLicenseSolrManager);
           var result = controller.SolrUpdateLicenseStatus(A<int>.Ignored);

           //Assert
           Assert.AreEqual(expected, result);

       }
    }
}
