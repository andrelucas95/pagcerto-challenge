using System.Threading.Tasks;
using api.Models.EntityModel;
using api.Models.EntityModel.Core;

namespace api.Models.ServiceModel
{
    public class PaymentProcessing : ServiceBase
    {
        private const decimal Fee = 0.9M;
        private const string RejectedCard = "5999";
        private readonly ICardTransactionRepository _cardTransactionRepository;

        public PaymentProcessing(ICardTransactionRepository cardTransactionRepository)
        {
            _cardTransactionRepository = cardTransactionRepository;
        }

        public CardTransaction CardTransaction { get; private set; }

        public async Task Process(string cardNumber, int installments, decimal amount)
        {
            CardTransaction = new CardTransaction(cardNumber, amount);

            AcquirerProcessing(cardNumber);

            if (CardTransaction.IsApproved())
            {
                CardTransaction.ChargeFee(Fee);
                CardTransaction.AddInstallments(installments);
            }

            if (!await Save())
                AddError("the request could not be processed");
        }

        private void AcquirerProcessing(string cardNumber)
        {
            var card = cardNumber.Substring(0, 4);

            if (card.Equals(RejectedCard))
            {
                CardTransaction.Reprove();
                AddError("Transaction reproved");
                return;
            }

            CardTransaction.Approve();
        }

        private async Task<bool> Save()
        {
            _cardTransactionRepository.Add(CardTransaction);
            return await _cardTransactionRepository.UnitOfWork.Commit();
        }

    }
}