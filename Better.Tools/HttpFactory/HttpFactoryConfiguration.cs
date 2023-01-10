using Better.Tools.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Better.Tools.HttpFactory
{
    public class HttpFactoryConfiguration : IConfigureOptions<HttpFactoryOptions>
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public HttpFactoryConfiguration(IServiceScopeFactory serviceScopeFactory)
        {
            _serviceScopeFactory = serviceScopeFactory;
        }

        public void Configure(HttpFactoryOptions options)
        {
            var scope = _serviceScopeFactory.CreateScope();
            var provider = scope.ServiceProvider;

            options.BaseUrl = Constants.BaseUrl;

            IDictionary<string, string> headerParams = new Dictionary<string, string>
            {
                { Constants.HeaderKey, Constants.HeaderToken }
            };

            options.Header = headerParams;
        }
    }
}
