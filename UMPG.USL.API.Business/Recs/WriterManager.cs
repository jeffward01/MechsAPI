using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UMPG.USL.API.Data.LicenseData;
using UMPG.USL.API.Data.Recs;
using UMPG.USL.Models;
using UMPG.USL.Models.LicenseModel;
using UMPG.USL.Models.Recs;


namespace UMPG.USL.API.Business.Recs
{
    public class WriterManager : IWriterManager
    {

        private readonly IWriterRepository _writerRepository;

        public WriterManager(IWriterRepository writerRepository)
        {
            _writerRepository = writerRepository;
        }

        public Writer Get(string id)
        {
            return _writerRepository.Get(id);
        }

        public List<Writer> GetAll()
        {
            return _writerRepository.GetAll();
        }

        //public Writer Add(Writer writer)
        //{
        //    return _writerRepository.Add(writer);
        //}

        public List<Writer> Search(string query)
        {
            return _writerRepository.Search(query);
            
        }

    }
}
