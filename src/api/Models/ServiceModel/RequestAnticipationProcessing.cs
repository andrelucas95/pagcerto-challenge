using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.EntityModel;
using api.Models.EntityModel.Core;
using api.Models.EntityModel.Enums;

namespace api.Models.ServiceModel
{
    public class RequestAnticipationProcessing
    {
        private readonly ICardTransactionRepository _cardTransactionRepository;

        public RequestAnticipationProcessing(ICardTransactionRepository cardTransactionRepository)
        {
            _cardTransactionRepository = cardTransactionRepository;
        }

        public bool AlreadyRequestedAnticipation { get; private set; }
        public Anticipation Anticipation { get; private set; }
        public List<CardTransaction> CardTransactions { get; private set; }
        public bool InProgess { get; private set; }

        public async Task<bool> Process(List<int> CardTransactionsNsus)
        {
            Anticipation = await _cardTransactionRepository.FindAnticipation();

            InProgess = HasAnticipationInProgress();

            if (InProgess) return false;

            CardTransactions = await _cardTransactionRepository.ListByNsus(CardTransactionsNsus);

            AlreadyRequestedAnticipation = HasAlreadyRequestedAnticipation();

            if (AlreadyRequestedAnticipation) return false;

            Anticipation = new Anticipation(CardTransactions);

            return await Save();
        }

        private async Task<bool> Save()
        {
            _cardTransactionRepository.Add(Anticipation);
            return await _cardTransactionRepository.UnitOfWork.Commit();
        }

        private bool HasAnticipationInProgress()
        {
            return Anticipation != null && Anticipation.AnalysisFinalizedAt == null;
        }

        private bool HasAlreadyRequestedAnticipation()
        {
            return CardTransactions.Where(ct => ct.AlreadyRequestedAnticipation()).Any();
        }
    }
}