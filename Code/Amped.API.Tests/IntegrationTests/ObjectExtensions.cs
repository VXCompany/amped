using Newtonsoft.Json;
using System.Net.Http;
using System.Text;

namespace Amped.API.Tests.IntegrationTests
{
    public static class ObjectExtensions
    {
        public static StringContent ToStringContent(this object input)
        {
            var serialized = JsonConvert.SerializeObject(input);
            return new StringContent(serialized, Encoding.UTF8, "application/json");
        }
    }
}
