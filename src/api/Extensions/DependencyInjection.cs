using api.Infrastructure;
using api.Infrastructure.Repositories;
using api.Models.EntityModel.Core;
using api.Models.IntegrationModel;
using api.Models.ServiceModel;
using Microsoft.Extensions.DependencyInjection;

namespace api.Extensions
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            //Context
            services.AddScoped<PaymentDbContext>();
            
            //Repositories
            services.AddScoped<ICardTransactionRepository, CardTransactionRepository>();

            //Apis
            services.AddScoped<IAcquirerApi, CieloApi>();

            //Services
            services.AddScoped<PaymentProcessing>();
            services.AddScoped<RequestAnticipationProcessing>();
            services.AddScoped<AnticipationAnalysisProcessing>();
        }
    }
}