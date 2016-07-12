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

using UMPG.USL.API.Controllers.AuditCTRL;

namespace UMPG.USL.API.Tests.Controller_Tests
{
    [TestFixture]
    public class AuditControllerTests
    {
        [Test]
        public void GetAuditForLicense_ReturnListAuditLicenseProcedureResult()
        {
            //Arrange
            var mockAuditManager = A.Fake<IAuditManager>();
            
            //Build expected
            List<AuditLicenseProcedureResult> expected = new List<AuditLicenseProcedureResult> { };

            //Build request
            AuditGenericRequest request = new AuditGenericRequest { };

            A.CallTo(() => mockAuditManager.GetAuditForLicense(A<AuditGenericRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            AuditController mockCtrl = new AuditController(mockAuditManager);
            var returned = mockCtrl.GetAuditForLicense(A<AuditGenericRequest>.Ignored);
            
            //Assert
            Assert.AreEqual(expected, returned);
        }

        [Test]
        public void GetProductAudit_ReturnListAuditProductProcedureResult()
        {
            //Arrange
            var mockAuditManager = A.Fake<IAuditManager>();

            //Build expected
            List<AuditProductProcedureResult> expected = new List<AuditProductProcedureResult> { };

            //Build request
            AuditGenericRequest request = new AuditGenericRequest { };

            A.CallTo(() => mockAuditManager.GetAuditForProducts(A<AuditGenericRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            AuditController mockCtrl = new AuditController(mockAuditManager);
            var returned = mockCtrl.GetProductAudit(A<AuditGenericRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, returned);
        }
    }
}
