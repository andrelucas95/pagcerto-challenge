using System.Threading.Tasks;
using api.Models.EntityModel;
using api.Models.EntityModel.Core;
using api.Models.IntegrationModel;

namespace api.Models.ServiceModel
{
    public class PaymentProcessing
    {
        private const decimal Fee = 0.9M;
        private readonly ICardTransactionRepository _cardTransactionRepository;
        private readonly IAcquirerApi _acquirerApi;

        public PaymentProcessing(
            ICardTransactionRepository cardTransactionRepository,
            IAcquirerApi acquirerApi)
        {
            _cardTransactionRepository = cardTransactionRepository;
            _acquirerApi = acquirerApi;
        }

        public CardTransaction CardTransaction { get; private set; }
        public bool Reproved { get; private set; }

        public async Task<bool> Process(CardPayment cardPayment)
        {
            CardTransaction = _acquirerApi.Process(cardPayment);

            Reproved = !CardTransaction.IsApproved();

            if (Reproved) return false;

            CardTransaction.ChargeFee(Fee);
            CardTransaction.AddInstallments(cardPayment.Installments);

            return await Save();
        }

        private async Task<bool> Save()
        {
            _cardTransactionRepository.Add(CardTransaction);
            return await _cardTransactionRepository.UnitOfWork.Commit();
        }

    }
}