using System;
using System.Collections.Generic;
using System.Linq;
using api.Models.EntityModel.Core;
using api.Models.EntityModel.Enums;

namespace api.Models.EntityModel
{
    public class Anticipation : Entity
    {
        protected Anticipation() { }

        public Anticipation(List<CardTransaction> cardTransactions)
        {
            CardTransactions = cardTransactions;
            RequestedAmount = CalculateRequestedAmount();
            AnalysisStatus = AnticipationAnalysisTypes.Pending;
        }

        public DateTime? AnalysisStartedAt { get; private set; }
        public DateTime? AnalysisFinalizedAt { get; private set; }
        public AnticipationAnalysisTypes AnalysisStatus { get; private set; }
        public decimal RequestedAmount { get; private set; }
        public decimal? AnticipatedAmount { get; private set; }
        public ICollection<CardTransaction> CardTransactions  { get; private set; }

        private decimal CalculateRequestedAmount() => CardTransactions.Select(ct => ct.NetAmount).ToList().Sum();
        public void StartAttendance() => AnalysisStartedAt = DateTime.Now;
        
        public void CalculateAnticipatedAmount()
        {
            AnticipatedAmount = CardTransactions
                .Where(t => t.Anticipated.HasValue && t.Anticipated.Value)
                .SelectMany(t => t.Installments)
                .Select(i => i.AnticipatedValue)
                .Sum();
        }
        
        public void FinalizeAttendance()
        {
            AnalysisFinalizedAt = DateTime.Now;

            CardTransactions
                .Where(t => t.Anticipated.Value)
                .SelectMany(t => t.Installments)
                .ToList().ForEach(i => i.Transfer());

            if (CardTransactions.ToList().Any(t => t.Anticipated.HasValue && t.Anticipated.Value))
                AnalysisStatus = AnticipationAnalysisTypes.PartiallyApproved;

            if (CardTransactions.ToList().TrueForAll(t => t.Anticipated.HasValue && t.Anticipated.Value))
                AnalysisStatus = AnticipationAnalysisTypes.Approved;

            if (CardTransactions.ToList().TrueForAll(t => t.Anticipated.HasValue && !t.Anticipated.Value))
                AnalysisStatus = AnticipationAnalysisTypes.Reproved;
        } 
    }
}