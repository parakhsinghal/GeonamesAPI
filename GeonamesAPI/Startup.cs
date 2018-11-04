using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using GeonamesAPI.Domain.Interfaces;
using GeonamesAPI.SQLRepository;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.AspNetCore.Mvc;

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

            services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(majorVersion: 2, minorVersion: 0);
            });
            
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v2", new Info()
                {
                    Title = "Geonames API",
                    Version = "v2",                    
                });
                options.CustomSchemaIds(x=>x.FullName);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(options=>options.SwaggerEndpoint("/swagger/v2/swagger.json", "Geonames API V2"));
            app.UseMvc();
        }
    }
}
