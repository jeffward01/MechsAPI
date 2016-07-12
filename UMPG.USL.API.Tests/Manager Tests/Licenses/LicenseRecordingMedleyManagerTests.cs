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

namespace UMPG.USL.API.Tests.Manager_Tests.Licenses
{
    [TestFixture]
    public class LicenseRecordingMedleyManagerTests
    {
        [Test]
        public void AddMedleys_ReturnListRecordingMedley()
        {
            //Arrange 
            var mockILicenseRecordingMedleyRepository = A.Fake<ILicenseRecordingMedleyRepository>();

            //Build request
            List<LicenseRecordingMedley> request = new List<LicenseRecordingMedley> { };
            LicenseRecordingMedley melody = new LicenseRecordingMedley { TrackId = 99 };
            request.Add(melody);

            A.CallTo(() => mockILicenseRecordingMedleyRepository.GetMedleysByTrackId(A<long>.Ignored)).WithAnyArguments().Returns(request);

            //Act
            LicenseRecordingMedleyManager manager = new LicenseRecordingMedleyManager(mockILicenseRecordingMedleyRepository);
            manager.AddMedleys(request);
            mockILicenseRecordingMedleyRepository.GetMedleysByTrackId(22);

            //Assert
            A.CallTo(() => mockILicenseRecordingMedleyRepository.GetMedleysByTrackId(A<long>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetMedleysByTrackId_ReturnListRecordingMedley()
        {
            //Arrange 
            var mockILicenseRecordingMedleyRepository = A.Fake<ILicenseRecordingMedleyRepository>();

            //Build request
            List<LicenseRecordingMedley> request = new List<LicenseRecordingMedley> { };
            LicenseRecordingMedley melody = new LicenseRecordingMedley { TrackId = 99 };
            request.Add(melody);

            A.CallTo(() => mockILicenseRecordingMedleyRepository.GetMedleysByTrackId(A<long>.Ignored)).WithAnyArguments().Returns(request);

            //Act
            LicenseRecordingMedleyManager manager = new LicenseRecordingMedleyManager(mockILicenseRecordingMedleyRepository);
            var result = manager.GetMedleysByTrackId(A<long>.Ignored);
            
            //Assert
            Assert.AreEqual(request, result);
        }

    }
}
