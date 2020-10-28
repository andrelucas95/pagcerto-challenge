using System.Threading.Tasks;
using api.Models.EntityModel;
using api.Models.ServiceModel;
using tests.Factories;
using tests.Fakes;
using Xunit;

namespace tests.Functional
{
    public sealed class CardTransactionTest
    {
        private readonly FakeApiServer _server;

        public CardTransactionTest()
        {
            _server = new FakeApiServer();
        }

        [Fact]
        public async Task ShouldBeReproved()
        {
            var paymentProcessing = new PaymentProcessing(_server.cardTransactionRepository, _server.acquirerApi);

            var result = await paymentProcessing.Process(new CardPayment().Build().WithInvalidValidCardNumber());

            Assert.Equal(result, false);
            Assert.Equal(paymentProcessing.Reproved, true);
            Assert.Equal(paymentProcessing.CardTransaction.Installments.Count, 0);
        }
    }
}