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
    public class PaidQuarterManagerTests
    {
        [Test]
        public void Get_ReturnLU_PaidQuarter()
        {
            //Arrange
            var mockIPaidQuarterRepository = A.Fake<IPaidQuarterRepository>();

            //Build expected
            LU_PaidQuarter expected = new LU_PaidQuarter { };

            A.CallTo(() => mockIPaidQuarterRepository.Get(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            PaidQuarterManager manager = new PaidQuarterManager(mockIPaidQuarterRepository);
            var result = manager.Get(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Search_ReturnLU_PaidQuarter()
        {
            //Arrange
            var mockIPaidQuarterRepository = A.Fake<IPaidQuarterRepository>();

            //Build expected
            List<LU_PaidQuarter> expected = new List<LU_PaidQuarter> { };

            A.CallTo(() => mockIPaidQuarterRepository.Search(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            PaidQuarterManager manager = new PaidQuarterManager(mockIPaidQuarterRepository);
            var result = manager.Search(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }



        [Test]
        public void GetRolling10years_ReturnLU_PaidQuarter()
        {
            //Arrange
            var mockIPaidQuarterRepository = A.Fake<IPaidQuarterRepository>();

            //Build expected
            List<LU_PaidQuarter> expected = new List<LU_PaidQuarter> { };

            A.CallTo(() => mockIPaidQuarterRepository.GetRolling10years()).WithAnyArguments().Returns(expected);

            //Act
            PaidQuarterManager manager = new PaidQuarterManager(mockIPaidQuarterRepository);
            var result = manager.GetRolling10years();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetAll_ReturnLU_PaidQuarter()
        {
            //Arrange
            var mockIPaidQuarterRepository = A.Fake<IPaidQuarterRepository>();

            //Build expected
            List<LU_PaidQuarter> expected = new List<LU_PaidQuarter> { };

            A.CallTo(() => mockIPaidQuarterRepository.GetAll()).WithAnyArguments().Returns(expected);

            //Act
            PaidQuarterManager manager = new PaidQuarterManager(mockIPaidQuarterRepository);
            var result = manager.GetAll();

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
