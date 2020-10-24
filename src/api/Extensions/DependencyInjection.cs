using api.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace api.Extensions
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<PaymentDbContext>();
        }
    }
}