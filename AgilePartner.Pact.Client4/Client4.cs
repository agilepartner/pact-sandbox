using AgilePartner.Pact.Lib;
using System.Threading.Tasks;

namespace AgilePartner.Pact.Consumers
{
    public class Client4
    {
        private readonly IRestProxy restProxy;
        private readonly string baseUri;

        public Client4(
            IRestProxy restProxy,
            string baseUri)
        {
            this.restProxy = restProxy;
            this.baseUri = baseUri + "/api/v3.0/helloworld";
        }

        public async Task<string> GetHello() => await restProxy.GetStringAsync(baseUri);
    }
}