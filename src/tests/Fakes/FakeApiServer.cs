using api.Infrastructure;
using api.Models.EntityModel.Core;
using api.Models.IntegrationModel;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;

namespace tests.Fakes
{
    public sealed class FakeApiServer : TestServer
    {
        public FakeApiServer() : base(new Program().CreateWebHostBuilder()) { }

        public PaymentDbContext Database => Host.Services.GetService<PaymentDbContext>();
        public FakeCardTransactionRepository cardTransactionRepository => Host.Services.GetService<ICardTransactionRepository>() as FakeCardTransactionRepository;
        public FakeAcquirerApi acquirerApi => Host.Services.GetService<IAcquirerApi>() as FakeAcquirerApi;
    }
}