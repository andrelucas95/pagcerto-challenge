using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.EntityModel;
using api.Models.EntityModel.Core;

namespace api.Models.ServiceModel
{
    public class AnticipationAnalysisProcessing
    {
        private readonly ICardTransactionRepository _cardTransactionRepository;

        public AnticipationAnalysisProcessing(ICardTransactionRepository cardTransactionRepository)
        {
            _cardTransactionRepository = cardTransactionRepository;
        }

        public Anticipation Anticipation { get; private set; }
        public List<int> TransactionsRequested { get; private set; }
        public bool NotFound { get; private set; }
        public bool TransactionAlreadyAnalyzed { get; private set; }

        public async Task<bool> Approve(List<int> cardTransactionsNsus)
        {
            TransactionsRequested = cardTransactionsNsus;

            await GetAnticipation();

            if (NotFound) return false;

            CheckTransactionsAlreadyAnalyzed();

            if (TransactionAlreadyAnalyzed) return false;

            ApproveRequestedTransactions();
            Anticipation.CalculateAnticipatedAmount();

            if (AllTransactionsAnalyzed()) Anticipation.FinalizeAttendance();

            return await Save();
        }

        public async Task<bool> Reprove(List<int> cardTransactionsNsus)
        {
            await GetAnticipation();

            if (NotFound) return false;

            CheckTransactionsAlreadyAnalyzed();

            if (TransactionAlreadyAnalyzed) return false;

            ReproveRequestedTransactions();

            if (AllTransactionsAnalyzed()) Anticipation.FinalizeAttendance();

            return await Save();
        }

        private async Task GetAnticipation()
        {
            Anticipation = await _cardTransactionRepository.FindAnticipation();
            if (Anticipation == null) NotFound = true;
        }

        private void CheckTransactionsAlreadyAnalyzed()
        {
            TransactionAlreadyAnalyzed = Anticipation.CardTransactions
                .Where(t => TransactionsRequested.Contains(t.Nsu))
                .Where(t => t.Anticipated.HasValue)
                .Any();
        }

        private bool AllTransactionsAnalyzed()
        {
            return Anticipation.CardTransactions
                .ToList()
                .TrueForAll(t => t.Anticipated != null);
        }

        private void ApproveRequestedTransactions()
        {
            Anticipation.CardTransactions
                .Where(t => TransactionsRequested.Contains(t.Nsu))
                .ToList().ForEach(t => t.ApproveAnticipation());
        }

        private void ReproveRequestedTransactions()
        {
            Anticipation.CardTransactions
                .Where(t => TransactionsRequested.Contains(t.Nsu))
                .ToList().ForEach(t => t.ReproveAnticipation());
        }

        private async Task<bool> Save()
        {
            return await _cardTransactionRepository.UnitOfWork.Commit();
        }
    }
}