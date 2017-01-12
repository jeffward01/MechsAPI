using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using System.Web.Script.Serialization;

namespace UMPG.USL.API.Tests.Controller_Tests.RECs_Controller_Tests
{
    [TestFixture]
    public class ProductsControllerTests
    {
        [Test]
        public void PagedSearch_ReturnPagedResponseProduct()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();

            //Build expected
            PagedResponse<Product> expected = new PagedResponse<Product> { };

            A.CallTo(() => mockProductManager.PagedSearch(A<ProductRequest>.Ignored)).Returns(expected);

            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var result = controller.PagedSearch(A<ProductRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test, Description("Note: Mock testing of JavaScriptDeserializer is needed.")]
        public void PagedSearchDummy_ReturnPagedResponseProduct()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();
            var mockJSerializer = A.Fake<JavaScriptSerializer>();

            //Build request
            ProductRequest request = new ProductRequest
            {

            };
            //Build expected
            PagedResponse<Product> expected = new PagedResponse<Product> { };
          
            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var returnedResult = controller.PagedSearchDummy(request);

            //Assert
            Assert.IsInstanceOf(typeof(PagedResponse<Product>), returnedResult);
        }

        [Test]
        public void GetProductHeader_ReturnProductHeader()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();

            //Build expected
            ProductHeader expected = new ProductHeader { };

            A.CallTo(() => mockProductManager.GetProductHeader(A<int>.Ignored)).Returns(expected);

            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var result = controller.GetProductHeader(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        //[Test]
        //public void UpdateProductPriority_ReturnBoolTRUE()
        //{
        //    //Arrange
        //    var mockProductManager = A.Fake<IProductManager>();

        //    //Build expected
        //    const bool expected = true;

        //    A.CallTo(() => mockProductManager.UpdateProductPriority(A<UpdatePriorityRequest>.Ignored, "")).Returns(expected);

        //    //Call  
        //    ProductController controller = new ProductController(mockProductManager);
        //    var result = controller.UpdateProductPriority(A<UpdatePriorityRequest>.Ignored,
        //        A<HttpRequestMessage>.Ignored);

        //    //Assert
        //    Assert.AreEqual(expected, result);
        //}

        [Test]
        public void GetProductRecsRecordings_ReturnListWorksRecording()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();

            //Build expected
            List<WorksRecording> expected = new List<WorksRecording> { };

            A.CallTo(() => mockProductManager.GetProductRecsRecordings(A<int>.Ignored)).Returns(expected);

            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var result = controller.GetProductRecsRecordings(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void GetWorksWriters_ReturnListWorksWriter()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();

            //Build expected
            List<WorksWriter> expected = new List<WorksWriter> { };

            A.CallTo(() => mockProductManager.GetWorksWriters(A<string>.Ignored)).Returns(expected);

            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var result = controller.GetWorksWriters(A<string>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void AddProduct_ReturnAddProductResult()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();

            //Build expected
            AddProductResult expected = new AddProductResult { };

            A.CallTo(() => mockProductManager.AddProduct(A<AddProductRequest>.Ignored)).Returns(expected);

            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var result = controller.AddProduct(A<AddProductRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void RetrieveTracks_ReturnListWorksRecording()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();

            //Build expected
            List<WorksRecording> expected = new List<WorksRecording> { };

            A.CallTo(() => mockProductManager.RetrieveTracks(A<int>.Ignored)).Returns(expected);

            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var result = controller.RetrieveTracks(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RetrieveLabels_ReturnListRecordLabel()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();

            //Build expected
            List<RecordLabel> expected = new List<RecordLabel> { };

            A.CallTo(() => mockProductManager.RetrieveLabels()).Returns(expected);

            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var result = controller.RetrieveLabels();

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void RetrieveTrack_ReturnUMPGUSLModelsRecsSingleResultTrack()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();

            //Build expected
            UMPG.USL.Models.Recs.SingleResult<Track> expected = new UMPG.USL.Models.Recs.SingleResult<Track> { };

            A.CallTo(() => mockProductManager.RetrieveTrack(A<RetrieveTrackRequest>.Ignored)).Returns(expected);

            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var result = controller.RetrieveTrack(A<RetrieveTrackRequest>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        //[Test]
        //public void SaveProduct_ReturnAddProductResult()
        //{
        //    //Arrange
        //    var mockProductManager = A.Fake<IProductManager>();

        //    //Build expected
        //    AddProductResult expected = new AddProductResult { };

        //    A.CallTo(() => mockProductManager.SaveProduct(A<ProductHeader>.Ignored)).Returns(expected);

        //    //Call  
        //    ProductController controller = new ProductController(mockProductManager);
        //    var result = controller.SaveProduct(A<ProductHeader>.Ignored);

        //    //Assert
        //    Assert.AreEqual(expected, result);
        //}

        //[Test]
        //public void SaveProductLink_ReturnUpdateProductLinkResult()
        //{
        //    //Arrange
        //    var mockProductManager = A.Fake<IProductManager>();

        //    //Build expected
        //    UpdateProductLinkResult expected = new UpdateProductLinkResult { };

        //    A.CallTo(() => mockProductManager.SaveProductLink(A<ProductLink>.Ignored)).Returns(expected);

        //    //Call  
        //    ProductController controller = new ProductController(mockProductManager);
        //    var result = controller.SaveProductLink(A<ProductLink>.Ignored);

        //    //Assert
        //    Assert.AreEqual(expected, result);
        //}

        [Test]
        public void GetProductLinks_ReturnListGetProductLink()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();

            //Build expected
            List<GetProductLink> expected = new List<GetProductLink> { };

            A.CallTo(() => mockProductManager.GetProductLinks(A<int>.Ignored)).Returns(expected);

            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var result = controller.GetProductLinks(A<int>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }

        [Test]
        public void DeleteProductLink_ReturnUpdateProductLinkResult()
        {
            //Arrange
            var mockProductManager = A.Fake<IProductManager>();

            //Build expected
            UpdateProductLinkResult expected = new UpdateProductLinkResult { };

            A.CallTo(() => mockProductManager.DeleteProductLink(A<ProductLink>.Ignored)).Returns(expected);

            //Call  
            ProductController controller = new ProductController(mockProductManager);
            var result = controller.DeleteProductLink(A<ProductLink>.Ignored);

            //Assert
            Assert.AreEqual(expected, result);
        }
    }
}
