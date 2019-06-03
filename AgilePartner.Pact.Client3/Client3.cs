using AgilePartner.Pact.Lib;
using System.Threading.Tasks;

namespace AgilePartner.Pact.Client3
{
    public class Client3
    {
        private readonly IRestProxy restProxy;
        private readonly string baseUri;

        public Client3(
            IRestProxy restProxy,
            string baseUri)
        {
            this.restProxy = restProxy;
            this.baseUri = baseUri + "/api/v2.0/helloworld";
        }

        public async Task<string> GetHello() => await restProxy.GetStringAsync(baseUri);
    }
}