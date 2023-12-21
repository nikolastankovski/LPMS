using Newtonsoft.Json;
using System.Text;
using LPMS.Domain.CustomModels;

namespace LPMS.Web.Helpers.API
{
    public class BaseHttpClient : IBaseHttpClient
    {
        private readonly HttpClient _client;

        public BaseHttpClient(HttpClient client)
        {
            _client = client;
        }

        public void AddHeader(KeyValuePair<string, string> header)
        {
            _client.DefaultRequestHeaders.Remove(header.Key);
            _client.DefaultRequestHeaders.Add(header.Key, header.Value);
        }

        public void RemoveHeader(string header)
        {
            _client.DefaultRequestHeaders.Remove(header);
        }

        public void SetBaseAddress(Uri domain)
        {
            _client.BaseAddress = domain;
        }

        public async Task<ActionResponse<T>> GetAsync<T>(string url)
        {
            using (var response = await _client.GetAsync(url))
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return new ActionResponse<T>()
                    {
                        IsSuccess = false,
                        Message = responseBody
                    };

                return new ActionResponse<T>()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Data = string.IsNullOrEmpty(responseBody) ? default : JsonConvert.DeserializeObject<T>(responseBody)
                };
            }
        }

        public async Task<ActionResponse<Stream>> GetStreamAsync(string url)
        {
            using (var response = await _client.GetAsync(url))
            {
                var responseBody = await response.Content.ReadAsStreamAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var errorMsg = await response.Content.ReadAsStringAsync();

                    return new ActionResponse<Stream>()
                    {
                        IsSuccess = false,
                        Message = errorMsg
                    };
                }

                return new ActionResponse<Stream>()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Data = responseBody
                };
            }
        }

        public async Task<ActionResponse<T>> PostAsync<T>(string url, string body, string? contentType = "application/json")
        {
            HttpContent content = new StringContent(content: body, encoding: Encoding.UTF8, mediaType: contentType);

            using (var response = await _client.PostAsync(url, content))
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return new ActionResponse<T>()
                    {
                        IsSuccess = false,
                        Message = responseBody
                    };

                return new ActionResponse<T>()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Data = string.IsNullOrEmpty(responseBody) ? default : JsonConvert.DeserializeObject<T>(responseBody)
                };
            }
        }

        public async Task<ActionResponse<T>> PutAsync<T>(string url, string body, string? contentType = "application/json")
        {
            HttpContent content = new StringContent(content: body, encoding: Encoding.UTF8, mediaType: contentType);

            using (var response = await _client.PutAsync(url, content))
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return new ActionResponse<T>()
                    {
                        IsSuccess = false,
                        Message = responseBody
                    };

                return new ActionResponse<T>()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Data = string.IsNullOrEmpty(responseBody) ? default : JsonConvert.DeserializeObject<T>(responseBody)
                };
            }
        }

        public async Task<ActionResponse<T>> DeleteAsync<T>(string url)
        {
            using (var response = await _client.DeleteAsync(url))
            {
                var responseBody = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return new ActionResponse<T>()
                    {
                        IsSuccess = false,
                        Message = responseBody
                    };

                return new ActionResponse<T>()
                {
                    IsSuccess = true,
                    Message = string.Empty,
                    Data = string.IsNullOrEmpty(responseBody) ? default : JsonConvert.DeserializeObject<T>(responseBody)
                };
            }
        }


        /*public async Task<string> GetAsync(string appBasePath, string actionPath, Dictionary<string, string?>? queryParams = null)
        {
            try
            {
                _client.DefaultRequestHeaders.Accept.Clear();
                _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAuthTokenForAnanas());

                var requestUrl = BuildUrl(appBasePath: appBasePath, actionPath: actionPath, queryParams: queryParams);

                var response = await _client.GetAsync(requestUrl);

                return await response.Content.ReadAsStringAsync();
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }*/

        //public async Task<string> PostAsync(string appBasePath, string actionPath, string body)
        //{
        //    _client.DefaultRequestHeaders.Accept.Clear();
        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAuthTokenForAnanas());

        //    var requestUrl = BuildUrl(appBasePath: appBasePath, actionPath: actionPath);
        //    HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");

        //    var response = await _client.PostAsync(requestUrl, content);

        //    return await response.Content.ReadAsStringAsync();
        //}

        //public async Task<string> PutAsync(string appBasePath, string actionPath, string body, Dictionary<string, string?>? queryParams = null)
        //{
        //    _client.DefaultRequestHeaders.Accept.Clear();
        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAuthTokenForAnanas());

        //    var requestUrl = BuildUrl(appBasePath: appBasePath, actionPath: actionPath, queryParams: queryParams);
        //    HttpContent content = new StringContent(body, Encoding.UTF8, "application/json");

        //    var response = await _client.PutAsync(requestUrl, content);

        //    return await response.Content.ReadAsStringAsync();
        //}

        //public async Task<string> DeleteAsync(string appBasePath, string actionPath, Dictionary<string, string?>? queryParams = null)
        //{
        //    _client.DefaultRequestHeaders.Accept.Clear();
        //    _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //    //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", await GetAuthTokenForAnanas());

        //    var requestUrl = BuildUrl(appBasePath: appBasePath, actionPath: actionPath, queryParams: queryParams);

        //    var response = await _client.DeleteAsync(requestUrl);

        //    return await response.Content.ReadAsStringAsync();
        //}

        /*private string BuildUrl(string appBasePath, string actionPath, Dictionary<string, string?>? queryParams = null)
        {
            var url = new UriBuilder();
            url.Path = $"{appBasePath}/{actionPath}";

            if (queryParams != null && queryParams.Count > 0)
            {
                var queryString = QueryHelpers.ParseQuery("");

                foreach (var qp in queryParams)
                {
                    queryString.Add(qp.Key, qp.Value);
                }

                url.Query = QueryString.Create(queryString).ToString();
            }

            return url.Uri.PathAndQuery.ToString();
        }*/
    }
}