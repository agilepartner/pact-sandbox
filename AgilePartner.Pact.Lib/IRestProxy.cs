using System.Threading;
using System.Threading.Tasks;

namespace AgilePartner.Pact.Lib
{
    public interface IRestProxy
    {
        Task<TResult> GetAsync<TResult>(string requestUri, CancellationToken? cancellationToken = null);
        Task<string> GetStringAsync(string requestUri, CancellationToken? cancellationToken = null);
        Task<byte[]> GetByteArrayAsync(string requestUri, CancellationToken? cancellationToken = null);
    }
}
