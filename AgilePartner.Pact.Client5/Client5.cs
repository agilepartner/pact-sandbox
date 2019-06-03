using AgilePartner.Pact.Lib;
using System.Threading.Tasks;

namespace AgilePartner.Pact.Client5
{
    public class Client5
    {
        private readonly IRestProxy restProxy;
        private readonly string baseUri;

        public Client5(
            IRestProxy restProxy,
            string baseUri)
        {
            this.restProxy = restProxy;
            this.baseUri = baseUri + "/api/v4.0/helloworld";
        }

        public async Task<string> GetHello(string name) => await restProxy.GetStringAsync($"{ baseUri }?name={ name }");
    }
}