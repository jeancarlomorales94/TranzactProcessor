using Application.Common;
using Application.Wikimedia;
using Microsoft.Extensions.DependencyInjection;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var services = ConfigureServices();
            var serviceProvider = services.BuildServiceProvider();

            var wikimediaProcessor = serviceProvider.GetService<WikimediaProcessor>();
            wikimediaProcessor.AddActivity<WikimediaExtractor>();
            wikimediaProcessor.AddActivity<WikimediaTransformer>();
            wikimediaProcessor.AddActivity<WikimediaLoader>();
            wikimediaProcessor.Process();
        }
        private static IServiceCollection ConfigureServices()
        {
            IServiceCollection services = new ServiceCollection();
            services.AddScoped<IActivity, WikimediaExtractor>();
            services.AddScoped<IActivity, WikimediaTransformer>();
            services.AddScoped<IActivity, WikimediaLoader>();
            services.AddTransient<WikimediaProcessor>();
            return services;
        }
    }
}
