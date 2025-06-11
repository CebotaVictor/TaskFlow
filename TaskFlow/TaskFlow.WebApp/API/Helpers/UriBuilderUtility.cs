using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtmHttp.Utility
{
    public static class UriBuilderUtility
    {
        private static Uri builder;

        public static Uri BuildUri(string postfix)
        {
            return new Uri("https://localhost:7129" + "/api/" + postfix);
        }
    }
}
