using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GeonamesAPI.Domain.Interfaces;
using GeonamesAPI.SQLRepository;

namespace GeonamesAPI
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
            services.AddMvc();
            services.AddSingleton<IContinentRepository, ContinentSQLRepository>();
            services.AddSingleton<ICountryRepository, CountrySQLRepository>();
            services.AddSingleton<IFeatureCategoryRepository, FeatureCategorySQLRepository>();
            services.AddSingleton<IFeatureCodeRepository, FeatureCodeSQLRepository>();
            services.AddSingleton<ITimeZoneRepository, TimeZoneSQLRepository>();
            services.AddSingleton<ILanguageCodeRepository, LanguageCodeSQLRepository>();
            services.AddSingleton<IRawPostalRepository, RawPostalSQLRepository>();
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
