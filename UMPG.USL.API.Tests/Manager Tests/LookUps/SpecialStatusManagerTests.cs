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
    public class SpecialStatusManagerTests
    {
        [Test]
        public void Get_ReturnSpecialStatus()
        {
            //Arrange
            var mockISpecialStatusRepository = A.Fake<ISpecialStatusRepository>();

            //Build expected
            LU_SpecialStatus expected = new LU_SpecialStatus { };

            A.CallTo(() => mockISpecialStatusRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            SpecialStatusManager manager = new SpecialStatusManager(mockISpecialStatusRepository);
            var result =manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAll_ReturnSpecialStatus()
        {
            //Arrange
            var mockISpecialStatusRepository = A.Fake<ISpecialStatusRepository>();

            //Build expected
            List<LU_SpecialStatus> expected = new List<LU_SpecialStatus> { };

            A.CallTo(() => mockISpecialStatusRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            SpecialStatusManager manager = new SpecialStatusManager(mockISpecialStatusRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void Search_ReturnSpecialStatus()
        {
            //Arrange
            var mockISpecialStatusRepository = A.Fake<ISpecialStatusRepository>();

            //Build expected
            List<LU_SpecialStatus> expected = new List<LU_SpecialStatus> { };

            A.CallTo(() => mockISpecialStatusRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            SpecialStatusManager manager = new SpecialStatusManager(mockISpecialStatusRepository);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
