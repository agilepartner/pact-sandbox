using AgilePartner.Pact.Lib;
using System.Threading.Tasks;

namespace AgilePartner.Pact.Client1
{
    public class Client1
    {
        private readonly IRestProxy restProxy;
        private readonly string baseUri;

        public Client1(
            IRestProxy restProxy,
            string baseUri)
        {
            this.restProxy = restProxy;
            this.baseUri = baseUri + "/api/v1.0/helloworld";
        }

        public async Task<string> GetHello() => await restProxy.GetStringAsync(baseUri);
    }
}