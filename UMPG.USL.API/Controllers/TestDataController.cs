using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Tracing;
using UMPG.USL.API.Business.DataHarmonization;
using UMPG.USL.API.Business.Licenses;
using UMPG.USL.API.ExtensionMethods;
using UMPG.USL.Models;

namespace UMPG.USL.API.Controllers
{
    [RoutePrefix("api/testCTRL/testMethods")]
    //  [EnableCors(origins: "http://spa.local", headers: "*", methods: "*")]
    public class TestDataController:ApiController
    {

        private readonly ITraceWriter _tracer;
        private readonly IDataHarmonizationManager _dataHarmonizationManager;

        public TestDataController(IDataHarmonizationManager dataHarmonizationManager)
        {
            _dataHarmonizationManager = dataHarmonizationManager;
            _tracer = GlobalConfiguration.Configuration.Services.GetTraceWriter();

        }
        [Authorize]
        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(SongHelper.CreateSongList());
        }
        [Authorize]
        [Route("update")]
        public IHttpActionResult Update(Song song)
        {
            return Ok(song);
        }


        //ToDo Start Exception testing here
        [Route("test")]
        [HttpGet]
        public HttpResponseMessage Retrieve()
        {
            // _tracer.Warn(Request, this.ControllerContext.ControllerDescriptor.ControllerType.FullName, "Cannot find id to delete: " + 2);
            // _tracer.Debug(Request, this.ControllerContext.ControllerDescriptor.ControllerType.FullName, "Cannot find id to delete: " + 2);
            // _tracer.Error(Request, this.ControllerContext.ControllerDescriptor.ControllerType.FullName,
            //     "Cannot find id to delete: " + 2);
            // _tracer.Info(Request, this.ControllerContext.ControllerDescriptor.ControllerType.FullName,
            //     "Cannot find id to delete: " + 2);
            _tracer.Error(Request, this.ControllerContext.ControllerDescriptor.ControllerType.FullName,
           "Cannot find id to delete: " + 2); //This is an example of a custo loggin message
            throw new ApplicationException("Ooops!");
            //throw new ArgumentOutOfRangeException();
        }


        [Route("header")]
        [HttpGet]
        public string Header(HttpRequestMessage headers)
        {
            return headers.GetHeaderValue("x-modified-by");

        }

    }
    #region Helpers

    public class SongHelper
    {

        public static List<Song> CreateSongList()
        {
            var songList = new List<Song> 
            {
                new Song {SongId = 12, SongName = "Graceland", Artist = "Paul Simon", Writers = "Paul Simon", Country = "UK",Ownership = 100},
                new Song {SongId = 123, SongName = "Graceland", Artist = "Paul Simon", Writers = "Paul Simon", Country = "UK",Ownership = 100},
                new Song {SongId = 1, SongName = "Graceland", Artist = "Paul Simon", Writers = "Paul Simon", Country = "UK",Ownership = 100},
                new Song {SongId = 1234, SongName = "Graceland", Artist = "Paul Simon", Writers = "Paul Simon", Country = "UK",Ownership = 100},
                new Song {SongId = 333, SongName = "Graceland", Artist = "Paul Simon", Writers = "Paul Simon", Country = "UK",Ownership = 100},
                new Song {SongId = 231, SongName = "Graceland", Artist = "Paul Simon", Writers = "Paul Simon", Country = "UK",Ownership = 100},
            };
            for (var i = 0; i < 5000;i++)
            {
                songList.Add(new Song { SongId = 10 + i, SongName = "Graceland", Artist = "Paul Simon", Writers = "Paul Simon", Country = "UK", Ownership = 100 });
            }

                return songList;
        }
    }

    #endregion
}