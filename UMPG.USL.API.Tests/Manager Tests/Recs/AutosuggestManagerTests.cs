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
using UMPG.USL.API.Data.Recs;
using UMPG.USL.API.Business.Recs;


namespace UMPG.USL.API.Tests.Manager_Tests.Recs
{
    [TestFixture]
    public class AutosuggestManagerTests
    {
        [Test]
        public void GetArtist_ReturnListResultArtist()
        {
            //Arrange
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            ListResult<ArtistRecs> expected = new ListResult<ArtistRecs> { };

            A.CallTo(() => mockIRecsDataProvider.ArtistAutosuggest(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            AutosuggestManager manager = new AutosuggestManager(mockIRecsDataProvider);
            var result = manager.Artist(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetProduct_ReturnListResultAlbumSkinny()
        {
            //Arrange
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            ListResult<AlbumSkinny> expected = new ListResult<AlbumSkinny> { };

            A.CallTo(() => mockIRecsDataProvider.AlbumAutosuggest(A<AlbumAutosuggestRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            AutosuggestManager manager = new AutosuggestManager(mockIRecsDataProvider);
            var result = manager.Product(A<AlbumAutosuggestRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetTrack_ReturnListResultTrackRecs()
        {
            //Arrange
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            ListResult<TrackRecs> expected = new ListResult<TrackRecs> { };

            A.CallTo(() => mockIRecsDataProvider.TrackAutosuggest(A<TrackAutosuggestRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            AutosuggestManager manager = new AutosuggestManager(mockIRecsDataProvider);
            var result = manager.Track(A<TrackAutosuggestRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void GetWork_ReturnListResultWorksSearchResult()
        {
            //Arrange
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            ListResult<WorksSearchResult> expected = new ListResult<WorksSearchResult> { };

            A.CallTo(() => mockIRecsDataProvider.WorksSearch(A<WorksSearchRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            AutosuggestManager manager = new AutosuggestManager(mockIRecsDataProvider);
            var result = manager.Work(A<WorksSearchRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }


        [Test]
        public void RetrieveLabelGroups_ReturnListLabelGroup()
        {
            //Arrange
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            List<LabelGroup> expected = new List<LabelGroup> { };

            A.CallTo(() => mockIRecsDataProvider.RetrieveLabelGroups(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            AutosuggestManager manager = new AutosuggestManager(mockIRecsDataProvider);
            var result = manager.RetrieveLabelGroups(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void VersionTypeRetrieveLabelGroups_ReturnListVersionType()
        {
            //Arrange
            var mockIRecsDataProvider = A.Fake<IRecsDataProvider>();

            //Build expected
            List<VersionType> expected = new List<VersionType> { };

            A.CallTo(() => mockIRecsDataProvider.GetVersionTypes()).WithAnyArguments().Returns(expected);

            //Act
            AutosuggestManager manager = new AutosuggestManager(mockIRecsDataProvider);
            var result = manager.GetVersionTypes();

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
