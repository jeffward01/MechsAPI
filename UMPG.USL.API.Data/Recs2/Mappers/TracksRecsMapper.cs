using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Linq.Expressions;
using UMPG.USL.Models.ProductSearchModel;
using UMPG.USL.Models.Recs;

namespace UMPG.USL.Common.Mappers
{
    public class TracksRecsMapper : IMapper<Recording, WorksRecording>
    {

        public Recording Map(WorksRecording source)
        {
            var recording = new Recording
            {
                PipsCode = source.Track.Copyrights.FirstOrDefault().WorkCode,
                cd_index = source.CdIndex,
                cd_number = source.CdNumber,
                //duration = source.Duration,
                title = source.Track.Title,
                track_id = source.TrackId,
                //isrc = source.Isrc,
                writersNo = source.Track.WritersNo,
                artist_id = source.Track.Artists.id,
                artistname = source.Track.Artists.name
            };
            return recording;
        }
    }
}