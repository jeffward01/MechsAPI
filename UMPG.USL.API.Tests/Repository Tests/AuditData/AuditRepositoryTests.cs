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
    public class AuditRepositoryTests
    {
        [Test]
        public void AddAudit_ReturnInt()
        {
            //Arrange
            var mockAuditRepository = A.Fake<IAuditRepository>();

            //Build expected
            int expected = 99;

            A.CallTo(() => mockAuditRepository.Add(A<Audit>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditRepository.Add(A<Audit>.Ignored);

            //Assert
            Assert.AreEqual(expected, returnedResult);
            A.CallTo(() => mockAuditRepository.Add(A<Audit>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAudit_ReturnAudit()
        {
            //Arrange
            var mockAuditRepository = A.Fake<IAuditRepository>();

            //Build expected
            Audit expected = new Audit { };

            A.CallTo(() => mockAuditRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditRepository.Get(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditRepository.Get(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetAll_ReturnListAudit()
        {
            //Arrange
            var mockAuditRepository = A.Fake<IAuditRepository>();

            //Build expected
            List<Audit> expected = new List<Audit> { };

            A.CallTo(() => mockAuditRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditRepository.GetAll();

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditRepository.GetAll()).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void Search_ReturnListAudit()
        {
            //Arrange
            var mockAuditRepository = A.Fake<IAuditRepository>();

            //Build expected
            List<Audit> expected = new List<Audit> { };

            A.CallTo(() => mockAuditRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            var returnedResult = mockAuditRepository.Search(A<string>.Ignored);

            //Assert
            Assert.AreSame(expected, returnedResult);
            A.CallTo(() => mockAuditRepository.Search(A<string>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
