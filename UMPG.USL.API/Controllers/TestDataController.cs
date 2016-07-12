using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using UMPG.USL.Models;

namespace UMPG.USL.API.Controllers
{
    [RoutePrefix("api/TestData")]
  //  [EnableCors(origins: "http://spa.local", headers: "*", methods: "*")]
    public class TestDataController:ApiController
    {
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