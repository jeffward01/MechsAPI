using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Net;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.API.Business.Recs;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.Recs;
using UMPG.USL.Models.Security;

namespace UMPG.USL.API.Tests.Manager_Tests.Recs
{
    public class ProductManagerTests
    {
        /*
        //[Test]
        //public void PagedSearch_ReturnPagedResponseProduct()
        //{
        //    //Arrange
        //    var mockRecISearchProvider = A.Fake<ISearchProvider>();
        //    var mockRecsProvider = A.Fake<IRecsDataProvider>();
        //    var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
        //    var mockILicenseProductRepository, mockILicenseProductManager = A.Fake<ILicenseProductRepository>();

        //    //Build expected
        //    PagedResponse<Product> expected = new PagedResponse<Product> { };

        //    A.CallTo(() => mockRecISearchProvider.SearchProducts(A<ProductRequest>.Ignored, 1)).WithAnyArguments().Returns(expected);

        //    //Act
        //    ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
        //    var result = manager.PagedSearch(A<ProductRequest>.Ignored);

        //    //Assert
        //    Assert.AreEqual(expected, result);
        //}

        [Test]
        public void GetProductHeader_ReturnProductHeader()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            var mockIProductManager = A.Fake<IProductManager>();

            //Build expected
            ProductHeader expected = new ProductHeader { };
            const int licenseNum = 99;

            A.CallTo(() => mockRecsProvider.RetrieveProductHeader(A<int>.Ignored)).WithAnyArguments().Returns(expected);
            A.CallTo(() => mockILicenseProductRepository.GetLicensesNo(A<int>.Ignored)).WithAnyArguments().Returns(licenseNum);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.GetProductHeader(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateProductPriority_ReturnBoolTRUE()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            var mockIProductManager = A.Fake<IProductManager>();
            //Build expected
            const bool expected = true;

            A.CallTo(() => mockRecsProvider.UpdateProductPriority(A<UpdatePriorityRequest>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.UpdateProductPriority(A<UpdatePriorityRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetProductRecsRecordings_ReturnListWorksRecording()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            var mockIProductManager = A.Fake<IProductManager>();
            //Build expected
            List<WorksRecording> expected = new List<WorksRecording> { };

            A.CallTo(() => mockRecsProvider.RetrieveProductRecordings(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.GetProductRecsRecordings(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetWorksWriters_ReturnListWorksWriter()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            var mockIProductManager = A.Fake<IProductManager>();
            //Build expected
            List<WorksWriter> expected = new List<WorksWriter> { };

            A.CallTo(() => mockRecsProvider.RetrieveTrackWriters(A<string>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.GetWorksWriters(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void UpdateProduct_ReturnProductHeader()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            var mockIProductManager = A.Fake<IProductManager>();
            //Build expected
            ProductHeader expected = new ProductHeader { };

            A.CallTo(() => mockRecsProvider.UpdateProduct(A<object>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.UpdateProduct(A<object>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RetrieveTracks_ReturnListWorksRecording()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            var mockIProductManager = A.Fake<IProductManager>();
            //Build expected
            List<WorksRecording> expected = new List<WorksRecording> { };

            A.CallTo(() => mockRecsProvider.RetrieveTracks(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.RetrieveTracks(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RetrieveTracks_ReturnListRecordLabel()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            var mockIProductManager = A.Fake<IProductManager>();
            //Build expected
            List<RecordLabel> expected = new List<RecordLabel> { };

            A.CallTo(() => mockRecsProvider.RetrieveLabels()).WithAnyArguments().Returns(expected);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.RetrieveLabels();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SaveProduct_ReturnListRecordLabel()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            var mockIProductManager = A.Fake<IProductManager>();
            //Build expected
            AddProductResult expected = new AddProductResult { };

            // A.CallTo(() => mockRecsProvider.SaveProduct(A<ProductHeader>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.SaveProduct(A<ProductHeader>.Ignored);

            //Assert
            Assert.IsInstanceOf(typeof(AddProductResult), result);
        }

        [Test]
        public void RetrieveTrack_ReturnListRecordLabel()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockCallerInfo = A.Fake<CallerInfo>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            var mockIProductManager = A.Fake<IProductManager>();
            //Build expected
            UMPG.USL.Models.Recs.SingleResult<Track> expected = new UMPG.USL.Models.Recs.SingleResult<Track> { };

            //Build request
            RetrieveTrackRequest request = new RetrieveTrackRequest
            {
                trackId = 99,
                callerInfo = mockCallerInfo
            };

            A.CallTo(() => mockRecsProvider.RetrieveTrack(request.trackId, request.callerInfo)).WithAnyArguments().Returns(expected);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.RetrieveTrack(request);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void SaveProductLink_ReturnListRecordLabel()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockIProductManager = A.Fake<IProductManager>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            //Build expected
            UpdateProductLinkResult expected = new UpdateProductLinkResult { };

            List<ProductLinkTrackCopyright> copywriteList = new List<ProductLinkTrackCopyright> { };

            ProductLinkArtist artist = new ProductLinkArtist
            {
                id = 99,
                name = "test",
            };

            ProductLinkTrack track = new ProductLinkTrack
            {
                id = 99,
                controlled = "NO",
                title = "test",
                copyrights = copywriteList,
                artist = artist,
                databaseVersion = 1
            };

            //Build request
            ProductLink request = new ProductLink
            {
                track = track,
                id = 99,
                databaseVersion = 1
            };

            HttpWebResponseWithStream response = new HttpWebResponseWithStream
            {
                statusCode = HttpStatusCode.OK
            };

            A.CallTo(() => mockRecsProvider.SaveProductLink(request)).WithAnyArguments().Returns(response);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.SaveProductLink(request);

            //Assert
            Assert.IsInstanceOf(typeof(UpdateProductLinkResult), result);
        }

        [Test]
        public void GetProductLinks_ReturnListGetProductLink()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockIProductManager = A.Fake<IProductManager>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            //Build expected
            List<GetProductLink> expected = new List<GetProductLink> { };

            A.CallTo(() => mockRecsProvider.GetProductLinks(A<int>.Ignored)).WithAnyArguments().Returns(expected);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.GetProductLinks(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DeleteProductLink_ReturnListGetProductLink()
        {
            //Arrange
            var mockRecISearchProvider = A.Fake<ISearchProvider>();
            var mockRecsProvider = A.Fake<IRecsDataProvider>();
            var mockILicenseProductRecordingRepository = A.Fake<ILicenseProductRecordingRepository>();
            var mockILicenseProductRepository = A.Fake<ILicenseProductRepository>();
            var mockIProductManager = A.Fake<IProductManager>();
            var mockILicenseProductManager = A.Fake<ILicenseProductManager>();
            //Build expected
            UpdateProductLinkResult expected = new UpdateProductLinkResult { };

            HttpWebResponseWithStream response = new HttpWebResponseWithStream
            {
                statusCode = HttpStatusCode.OK,
                responseStream = "test"
            };

            A.CallTo(() => mockRecsProvider.RemoveProductLink(A<ProductLink>.Ignored)).WithAnyArguments().Returns(response);

            //Act
            ProductManager manager = new ProductManager(mockRecISearchProvider, mockRecsProvider, mockILicenseProductRecordingRepository, mockILicenseProductRepository, mockILicenseProductManager);
            var result = manager.DeleteProductLink(A<ProductLink>.Ignored);

            //Assert
            Assert.IsInstanceOf(typeof(UpdateProductLinkResult), result);
            A.CallTo(() => mockRecsProvider.RemoveProductLink(A<ProductLink>.Ignored)).WithAnyArguments().MustHaveHappened();
        }
    }
    */
    }
}