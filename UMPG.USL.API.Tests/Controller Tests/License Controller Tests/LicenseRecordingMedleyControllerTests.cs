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


namespace UMPG.USL.API.Tests.Controller_Tests.License_Controller_Tests
{
    public class LicenseRecordingMedleyControllerTests
    {
        [Test]
        public void AddRecordingMelody_ReturnBoolTRUE()
        {
            //Arrange
            var mockLicenseRecordingMedleyManager = A.Fake<ILicenseRecordingMedleyManager>();

            A.CallTo(() => mockLicenseRecordingMedleyManager.AddMedleys(A<List<LicenseRecordingMedley>>.Ignored)).WithAnyArguments();

            //Act
            LicenseRecordingMedleyController controller = new LicenseRecordingMedleyController(mockLicenseRecordingMedleyManager);
            controller.AddRecordingMedley(A<List<LicenseRecordingMedley>>.Ignored);

            //Assert
            A.CallTo(() => mockLicenseRecordingMedleyManager.AddMedleys(A<List<LicenseRecordingMedley>>.Ignored)).WithAnyArguments().MustHaveHappened();
        }

        [Test]
        public void GetMedleysByTrackId_ReturnListLicenseRecordingMedley()
        {
            //Arrange
            var mockLicenseRecordingMedleyManager = A.Fake<ILicenseRecordingMedleyManager>();

            //Build expected
            List<LicenseRecordingMedley> expected = new List<LicenseRecordingMedley> { };

            A.CallTo(() => mockLicenseRecordingMedleyManager.GetMedleysByTrackId(A<long>.Ignored)).WithAnyArguments();

            //Act
            LicenseRecordingMedleyController controller = new LicenseRecordingMedleyController(mockLicenseRecordingMedleyManager);
            controller.GetMedleysByTrackId(A<int>.Ignored);

            //Assert
            A.CallTo(() => mockLicenseRecordingMedleyManager.GetMedleysByTrackId(A<int>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
}
