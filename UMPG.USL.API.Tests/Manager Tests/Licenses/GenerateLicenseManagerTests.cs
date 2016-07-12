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

namespace UMPG.USL.API.Tests.Manager_Tests.Contacts
{
    [TestFixture]
    public class GenerateLicenseManagerTests
    {
        [Test]
        [Description("Sealed Methods (Update) Cannot be tested.  Refactor if necessary.")]
        public void UpdateGenerateLicenseStatus_ReturnVoid()
        {
            //Arrange
            var mockGenerateLicenseQueueRepository = A.Fake<IGenerateLicenseQueueRepository>();

            LicenseUserAction request = new LicenseUserAction { licenseId = 99, userAction = 99 };
            //Act
            GenerateLicenseManager manager = new GenerateLicenseManager(mockGenerateLicenseQueueRepository);
            manager.UpdateGenerateLicenseStatus(request);

            //Assert
            //A.CallTo(() => mockGenerateLicenseQueueRepository.UpdateGenerateLicenseStatus(request)).MustHaveHappened();
            //A.CallTo(() => manager.Update(A<GenerateLicenseQueue>.Ignored)).MustHaveHappened();
            // Assert.Pass();
        }


        [Test]
        public void GetByLicenseId_ReturnInt()
        {
            //Arrange
            var mockGenerateLicenseQueueRepository = A.Fake<IGenerateLicenseQueueRepository>();

            List<GenerateLicenseQueue> request = new List<GenerateLicenseQueue> { };

            //Build Expected
            List<GenerateLicenseQueue> expected = new List<GenerateLicenseQueue> { };

            A.CallTo(() => mockGenerateLicenseQueueRepository.GetByLicenseId(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            GenerateLicenseManager manager = new GenerateLicenseManager(mockGenerateLicenseQueueRepository);
            var result = manager.GetByLicenseId(A<int>.Ignored);

            //Assert
            Assert.AreSame(expected, result);
            A.CallTo(() => mockGenerateLicenseQueueRepository.GetByLicenseId(A<int>.Ignored)).MustHaveHappened();
        }

        [Test]
        public void Update_ReturnVoid()
        {
            //Arrange
            var mockGenerateLicenseQueueRepository = A.Fake<IGenerateLicenseQueueRepository>();

            List<GenerateLicenseQueue> request = new List<GenerateLicenseQueue> { };

            //Build Expected
            List<GenerateLicenseQueue> expected = new List<GenerateLicenseQueue> { };

            A.CallTo(() => mockGenerateLicenseQueueRepository.Update(A<GenerateLicenseQueue>.Ignored)).WithAnyArguments();

            //Act
            GenerateLicenseManager manager = new GenerateLicenseManager(mockGenerateLicenseQueueRepository);
            manager.Update(A<GenerateLicenseQueue>.Ignored);

            //Assert
            A.CallTo(() => mockGenerateLicenseQueueRepository.Update(A<GenerateLicenseQueue>.Ignored)).MustHaveHappened();
        }
    }
}
