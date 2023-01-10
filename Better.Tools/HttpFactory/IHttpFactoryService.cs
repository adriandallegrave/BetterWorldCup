namespace Better.Tools.HttpFactory
{
    public interface IHttpFactoryService
    {
        public Task<HttpResponseMessage> ExecuteGetRequestAsync(string url,
                                                                string resource = null,
                                                                (string UserName, string Password) basicAuth = default,
                                                                IDictionary<string, string> queryParams = null,
                                                                IDictionary<string, string> headerParams = null);

        public Task<HttpResponseMessage> ExecuteGetRequestAsync(string url,
                                                                string resource = null,
                                                                string basicAuthBase64 = default,
                                                                IDictionary<string, string> queryParams = null,
                                                                IDictionary<string, string> headerParams = null);

        public Task<HttpResponseMessage> ExecutePostBasicAuthRequestAsync(string url,
                                                                          string resource = null,
                                                                          object body = null,
                                                                          (string UserName, string Password) basicAuth = default,
                                                                          IDictionary<string, string> queryParams = null,
                                                                          IDictionary<string, string> headerParams = null);

        public Task<HttpResponseMessage> ExecutePostBasicAuthRequestAsync(string url,
                                                                          string resource = null,
                                                                          object body = null,
                                                                          string basicAuthBase64 = default,
                                                                          IDictionary<string, string> queryParams = null,
                                                                          IDictionary<string, string> headerParams = null);

        public HttpRequestMessage CreateFactoryRequest(string resource,
                                                       HttpMethod method,
                                                       object body = null,
                                                       IDictionary<string, string> queryParams = null,
                                                       IDictionary<string, string> header = null);

        public Task<HttpResponseMessage> ExecuteRequestAsync(HttpClient client, HttpRequestMessage request);

        public Task<HttpResponseMessage> PostThat(string route, object body);

        public Task<HttpResponseMessage> GetThat(string route);
    }
}
