using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace Better.Tools.HttpFactory
{
    public class HttpFactoryService : IHttpFactoryService
    {
        private readonly HttpClient _httpClient;

        public HttpFactoryService(IHttpClientFactory clientFactory, IOptions<HttpFactoryOptions> httpFactoryOptionsValue)
        {
            _httpClient = clientFactory.CreateClient(nameof(HttpFactoryService));
            var httpFactoryOptions = httpFactoryOptionsValue;

            _httpClient.BaseAddress = new Uri(httpFactoryOptions.Value.BaseUrl);

            if (httpFactoryOptions.Value.Header != null && httpFactoryOptions.Value.Header.Any())
            {
                var firstHeader = httpFactoryOptions.Value.Header.FirstOrDefault();
                _httpClient.DefaultRequestHeaders.Add(firstHeader.Key, firstHeader.Value);
            }

            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public HttpRequestMessage CreateFactoryRequest(string resource,
                                                       HttpMethod method,
                                                       object body = null,
                                                       IDictionary<string, string> queryParams = null,
                                                       IDictionary<string, string> header = null)
        {
            var request = new HttpRequestMessage(method, resource);

            if (header != null)
            {
                header.ToList().ForEach(kv =>
                {
                    request.Headers.Add(kv.Key, kv.Value);
                });
            }

            if (queryParams != null)
            {
                queryParams.ToList().ForEach(kv =>
                {
                    resource = resource.Replace(kv.Key, kv.Value);
                });
            }

            if (body != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, "application/json");
            }

            return request;
        }

        public async Task<HttpResponseMessage> ExecuteGetRequestAsync(string url,
                                                                      string resource = null,
                                                                      (string UserName, string Password) basicAuth = default,
                                                                      IDictionary<string, string> queryParams = null,
                                                                      IDictionary<string, string> headerParams = null)
        {
            var request = CreateFactoryRequest(resource, HttpMethod.Get, null, queryParams, headerParams);

            return await ExecuteRequestAsync(_httpClient, request);
        }

        public async Task<HttpResponseMessage> ExecuteGetRequestAsync(string url,
                                                                      string resource = null,
                                                                      string basicAuthBase64 = null,
                                                                      IDictionary<string, string> queryParams = null,
                                                                      IDictionary<string, string> headerParams = null)
        {
            var request = CreateFactoryRequest(resource, HttpMethod.Get, null, queryParams, headerParams);

            return await ExecuteRequestAsync(_httpClient, request);
        }

        public async Task<HttpResponseMessage> ExecutePostBasicAuthRequestAsync(string url,
                                                                                string resource = null,
                                                                                object body = null,
                                                                                (string UserName, string Password) basicAuth = default,
                                                                                IDictionary<string, string> queryParams = null,
                                                                                IDictionary<string, string> headerParams = null)
        {
            var request = CreateFactoryRequest(resource, HttpMethod.Post, body, queryParams, headerParams);

            return await ExecuteRequestAsync(_httpClient, request);
        }

        public Task<HttpResponseMessage> PostThat(string route, object body)
        {
            var result = ExecutePostBasicAuthRequestAsync(null, route, body, null, null);

            return result;
        }

        public Task<HttpResponseMessage> GetThat(string route)
        {
            (string UserName, string Password) basicAuth = (default, default);
            var result = ExecuteGetRequestAsync(null, route, basicAuth);

            return result;
        }

        public async Task<HttpResponseMessage> ExecutePostBasicAuthRequestAsync(string url,
                                                                                string resource = null,
                                                                                object body = null,
                                                                                string basicAuthBase64 = default,
                                                                                IDictionary<string, string> queryParams = null,
                                                                                IDictionary<string, string> headerParams = null)
        {
            var request = CreateFactoryRequest(resource, HttpMethod.Post, body, queryParams, headerParams);

            return await ExecuteRequestAsync(_httpClient, request);
        }

        public async Task<HttpResponseMessage> ExecuteRequestAsync(HttpClient client, HttpRequestMessage request)
        {
            return request.Method switch
            {
                HttpMethod m when m == HttpMethod.Post => await client.PostAsync(request.RequestUri, request.Content),
                HttpMethod m when m == HttpMethod.Put => await client.PutAsync(request.RequestUri, request.Content),
                HttpMethod m when m == HttpMethod.Delete => await client.DeleteAsync(request.RequestUri),
                _ => await client.GetAsync(request.RequestUri)
            };
        }
    }
}
