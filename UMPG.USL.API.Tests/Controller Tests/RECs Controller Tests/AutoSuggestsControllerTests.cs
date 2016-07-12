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
    public class AutoSuggestsControllerTests
    {
        [Test]
        public void GetArtist_ReturnListResultArtistRecs()
        {
            //Arrange
            var mockAutosuggestManager = A.Fake<IAutosuggestManager>();

            //Build expected
            ListResult<ArtistRecs> expected = new ListResult<ArtistRecs> { };

            A.CallTo(() => mockAutosuggestManager.Artist(A<string>.Ignored)).Returns(expected);

            //Call  
            AutosuggestsController controller = new AutosuggestsController(mockAutosuggestManager);
            var result = controller.Artist(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetProduct_ReturnListResultAlbumSkinny()
        {
            //Arrange
            var mockAutosuggestManager = A.Fake<IAutosuggestManager>();

            //Build expected
            ListResult<AlbumSkinny> expected = new ListResult<AlbumSkinny> { };

            A.CallTo(() => mockAutosuggestManager.Product(A<AlbumAutosuggestRequest>.Ignored)).Returns(expected);

            //Call  
            AutosuggestsController controller = new AutosuggestsController(mockAutosuggestManager);
            var result = controller.Product(A<AlbumAutosuggestRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetProduct_ReturnListResultTrackRecs()
        {
            //Arrange
            var mockAutosuggestManager = A.Fake<IAutosuggestManager>();

            //Build expected
            ListResult<TrackRecs> expected = new ListResult<TrackRecs> { };

            A.CallTo(() => mockAutosuggestManager.Track(A<TrackAutosuggestRequest>.Ignored)).Returns(expected);

            //Call  
            AutosuggestsController controller = new AutosuggestsController(mockAutosuggestManager);
            var result = controller.Product(A<TrackAutosuggestRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void Work_ReturnListResultWorksSearchResult()
        {
            //Arrange
            var mockAutosuggestManager = A.Fake<IAutosuggestManager>();

            //Build expected
            ListResult<WorksSearchResult> expected = new ListResult<WorksSearchResult> { };

            A.CallTo(() => mockAutosuggestManager.Work(A<WorksSearchRequest>.Ignored)).Returns(expected);

            //Call  
            AutosuggestsController controller = new AutosuggestsController(mockAutosuggestManager);
            var result = controller.Work(A<WorksSearchRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RetrieveLabelGroups_ReturnListLabelGroup()
        {
            //Arrange
            var mockAutosuggestManager = A.Fake<IAutosuggestManager>();

            //Build expected
            List<LabelGroup> expected = new List<LabelGroup> { };

            A.CallTo(() => mockAutosuggestManager.RetrieveLabelGroups(A<string>.Ignored)).Returns(expected);

            //Call  
            AutosuggestsController controller = new AutosuggestsController(mockAutosuggestManager);
            var result = controller.GetLabelGroups(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RetrieveLabelGroups_ReturnListVersionType()
        {
            //Arrange
            var mockAutosuggestManager = A.Fake<IAutosuggestManager>();

            //Build expected
            List<VersionType> expected = new List<VersionType> { };

            A.CallTo(() => mockAutosuggestManager.GetVersionTypes()).Returns(expected);

            //Call  
            AutosuggestsController controller = new AutosuggestsController(mockAutosuggestManager);
            var result = controller.GetVersionTypes();

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
