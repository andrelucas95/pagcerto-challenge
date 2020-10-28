using api.Models.EntityModel;
using api.Models.IntegrationModel;

namespace tests.Fakes
{
    public class FakeAcquirerApi : IAcquirerApi
    {
        private const string RejectedCard = "5999";
        
        public CardTransaction Process(CardPayment cardPayment)
        {
            var CardTransaction = new CardTransaction(cardPayment.CardNumber, cardPayment.Amount);

            var card = cardPayment.CardNumber.Substring(0, 4);

            if (card.Equals(RejectedCard))
            {
                CardTransaction.Reprove();
                return CardTransaction;
            }

            CardTransaction.Approve();
            return CardTransaction;
        }
    }
}