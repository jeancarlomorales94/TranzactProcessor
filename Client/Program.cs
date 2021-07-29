using System;
using System.Collections.Generic;
using System.Linq;
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

    public class WikimediaLoader : IActivity
    {
        public string Message => "";

        public bool Execute()
        {
            Console.WriteLine("Loader");
            return true;
        }
    }

    public class WikimediaTransformer : IActivity
    {
        public string Message => "";

        public bool Execute()
        {
            Console.WriteLine("Transformer");
            return false;
        }
    }

    public class WikimediaExtractor : IActivity
    {
        public string Message => "";

        public bool Execute()
        {
            Console.WriteLine("Extractor");
            return true;
        }
    }

    public class WikimediaProcessor
    {
        private readonly List<Type> _addedActivities = new List<Type>();
        private readonly IEnumerable<IActivity> _activities;

        public WikimediaProcessor(IEnumerable<IActivity> activities)
        {
            _activities = activities;
        }
        public void AddActivity<T>() where T : IActivity => _addedActivities.Add(typeof(T));
        public void Process()
        {
            bool continueExecution = true;

            foreach (var activity in _addedActivities)
            {
                if (!continueExecution)
                    return;

                var implementation = _activities.FirstOrDefault(x => x.GetType() == activity);
                if (implementation == null)
                    return;

                continueExecution = implementation.Execute();
            }
        }
    }

    public interface IActivity
    {
        bool Execute();
        string Message { get; }
    }
}
