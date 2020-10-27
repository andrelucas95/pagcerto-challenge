using api.Models.EntityModel;

namespace api.Models.IntegrationModel
{
    public class CieloApi : IAcquirerApi
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