using System.Collections.Generic;
using System.Threading.Tasks;
using api.Models.EntityModel;
using api.Models.ServiceModel;
using tests.Factories;
using tests.Fakes;
using Xunit;

namespace tests.Functional
{
    public sealed class AnticipationTest
    {
        private readonly FakeApiServer _server;

        public AnticipationTest()
        {
            _server = new FakeApiServer();
        }

        [Fact]
        public async Task ShouldBeHasAnticipationInProgress()
        {
            var paymentProcessing = new PaymentProcessing(_server.cardTransactionRepository, _server.acquirerApi);
            var requestAnticipationProcessing = new RequestAnticipationProcessing(_server.cardTransactionRepository);
            
            await paymentProcessing.Process(new CardPayment().Build().WithValidCardNumber());

            var transaction = paymentProcessing.CardTransaction;

            await requestAnticipationProcessing.Process(new List<int>() { transaction.Nsu });
            var result = await requestAnticipationProcessing.Process(new List<int>() { transaction.Nsu });

            Assert.Equal(result, false);
            Assert.Equal(requestAnticipationProcessing.InProgess, true);
        }
    }
}