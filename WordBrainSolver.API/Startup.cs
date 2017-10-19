using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WordBrainSolver.Core;
using WordBrainSolver.Core.Algorithm;
using WordBrainSolver.Core.Dictionary;
using WordBrainSolver.Core.Interfaces;

namespace WordBrainSolver.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
            services.AddMvc();

            int bruteForceSearchLimit = Convert.ToInt32(Configuration.GetSection("BruteForceSearchLimit").Value);
            string storageConnectionString = Configuration.GetSection("StorageConnectionString").Value;

            services.AddTransient<ISolutionGeneratorCoordinator, SolutionGeneratorCoordinator>();
            services.AddTransient<IWordFinderForLocation, WordFinderForLocation>();
            services.AddTransient<ISubDictionaryGenerator>(s => new SubDictionaryGenerator(bruteForceSearchLimit));
            services.AddTransient<IDictionaryRepository>(s => new DictionaryRepository(storageConnectionString, s.GetService<IWordDictionariesCacheService>()));
            services.AddTransient<IBasicPrimaryWordSearcher>(s => new BasicPrimaryWordSearcher(s.GetService<IIntelligentSecondaryWordSearcher>(), bruteForceSearchLimit));
            services.AddTransient<IIntelligentSecondaryWordSearcher>(s => new IntelligentSecondaryWordSearcher(bruteForceSearchLimit));
            services.AddTransient<IWordDictionariesCacheService, WordDictionariesCacheService>();
            services.AddTransient<IRemoveWordFromBoard, RemoveWordFromBoard>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
