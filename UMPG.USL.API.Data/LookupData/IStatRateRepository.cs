using System;

namespace UMPG.USL.API.Data.LookupData
{
    public interface IStatRateRepository
    {
        float GetStatRate(float duration, DateTime date);
    }
}
