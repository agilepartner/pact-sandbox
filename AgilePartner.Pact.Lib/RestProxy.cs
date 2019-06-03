using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AgilePartner.Pact.Lib
{
    public sealed class RestProxy : IRestProxy
    {
        private HttpClient CreateHttpClient() => new HttpClient { Timeout = TimeSpan.FromSeconds(10) };

        public async Task<TResult> GetAsync<TResult>(string requestUri, CancellationToken? cancellationToken = null)
            => await GetAsync(requestUri, async (c) => await ReadAsync<TResult>(c), cancellationToken);

        public async Task<string> GetStringAsync(string requestUri, CancellationToken? cancellationToken = null)
            => await GetAsync(requestUri, (c) => c.ReadAsStringAsync(), cancellationToken);

        public async Task<byte[]> GetByteArrayAsync(string requestUri, CancellationToken? cancellationToken = null)
            => await GetAsync(requestUri, (c) => c.ReadAsByteArrayAsync(), cancellationToken);

        private async Task<TResult> GetAsync<TResult>(
            string requestUri, 
            Func<HttpContent, Task<TResult>> readMethod, 
            CancellationToken? cancellationToken = null)
        {
            using (HttpClient client = CreateHttpClient())
            {
                var response = await (cancellationToken.HasValue ? client.GetAsync(requestUri, cancellationToken.Value) : client.GetAsync(requestUri));
                return await readMethod(response.Content);
            }
        }

        private async Task<T> ReadAsync<T>(HttpContent content)
        {
            var json = await content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
