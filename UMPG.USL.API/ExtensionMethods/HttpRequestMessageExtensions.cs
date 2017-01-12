using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace UMPG.USL.API.ExtensionMethods
{
    public static class HttpRequestMessageExtensions
    {
        public static string GetHeaderValue(this HttpRequestMessage request, string name)
        {
            IEnumerable<string> values;
            var found = request.Headers.TryGetValues(name, out values);
            return found ? values.FirstOrDefault() : null;
        }
    }
}