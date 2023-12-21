using LPMS.Domain.CustomModels;

namespace LPMS.Web.Helpers.API
{
    public interface IBaseHttpClient
    {
        void AddHeader(KeyValuePair<string, string> header);
        void RemoveHeader(string header);
        void SetBaseAddress(Uri domain);
        Task<ActionResponse<T>> GetAsync<T>(string url);
        Task<ActionResponse<Stream>> GetStreamAsync(string url);
        Task<ActionResponse<T>> PostAsync<T>(string url, string body, string? contentType = "application/json");
        Task<ActionResponse<T>> PutAsync<T>(string url, string body, string? contentType = "application/json");
        Task<ActionResponse<T>> DeleteAsync<T>(string url);
    }
}
