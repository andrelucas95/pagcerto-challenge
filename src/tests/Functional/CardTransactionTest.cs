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

        [Fact]
        public async Task ShouldBeApproved()
        {
            var paymentProcessing = new PaymentProcessing(_server.cardTransactionRepository, _server.acquirerApi);
            var cardPayment = new CardPayment().Build().WithValidCardNumber();
            
            var result = await paymentProcessing.Process(cardPayment);

            Assert.Equal(result, true);
            Assert.Equal(paymentProcessing.CardTransaction.Installments.Count, cardPayment.Installments);
            Assert.Equal(paymentProcessing.CardTransaction.Fee, 0.9M);
        }
    }
}