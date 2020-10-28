using System;
using api.Infrastructure;
using api.Models.EntityModel.Core;
using api.Models.IntegrationModel;
using api.Models.ServiceModel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using tests.Fakes;

namespace tests
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PaymentDbContext>(options =>
            {
                options.UseInMemoryDatabase(Guid.NewGuid().ToString());
            });

            services.AddControllers();

            services.AddSingleton<PaymentDbContext>();
            
            //Repositories
            services.AddScoped<ICardTransactionRepository, FakeCardTransactionRepository>();

            //Apis
            services.AddScoped<IAcquirerApi, FakeAcquirerApi>();

            //Services
            services.AddScoped<PaymentProcessing>();
            services.AddScoped<RequestAnticipationProcessing>();
            services.AddScoped<AnticipationAnalysisProcessing>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
