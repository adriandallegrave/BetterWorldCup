using Better.Application.Interfaces;
using Better.Application.Services;
using Better.Domain.Interfaces;
using Better.Domain.Interfaces.Services;
using Better.Domain.Services;
using Better.Persistence;
using Better.Tools.HttpFactory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Better.Injection
{
    public static class InversionOfControl
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            RegisterApplicationServices(services);
            RegisterDomainServices(services);
            RegisterPersistenceServices(services);
            RegisterToolsServices(services);

            return services;
        }

        private static void RegisterApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IAccountAppService, AccountAppService>();
            services.AddScoped<ITeamAppService, TeamAppService>();
            services.AddScoped<IGameAppService, GameAppService>();
            services.AddScoped<ITransactionAppService, TransactionAppService>();
            services.AddScoped<IBetAppService, BetAppService>();
            services.AddScoped<IWebAppService, WebAppService>();
        }

        private static void RegisterDomainServices(IServiceCollection services)
        {
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<ITeamService, TeamService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<ITransactionService, TransactionService>();
            services.AddScoped<IBetService, BetService>();
        }

        private static void RegisterPersistenceServices(IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
        }

        private static void RegisterToolsServices(IServiceCollection services)
        {
            // Http
            services.AddScoped<IHttpFactoryService, HttpFactoryService>();
            services.AddSingleton<IConfigureOptions<HttpFactoryOptions>, HttpFactoryConfiguration>();
            services.AddHttpClient(nameof(HttpFactoryService)).SetHandlerLifetime(TimeSpan.FromMinutes(5));
        }
    }
}
