using System;
using api.Models.EntityModel;

namespace api.Models.ResultModel
{
    public class AnticipationResultJson
    {
        public AnticipationResultJson(Anticipation anticipation)
        {
            Id = anticipation.Id;
            SolicitedAt = anticipation.CreatedAt;
            AnalysisStartedAt = anticipation.AnalysisStartedAt;
            AnalysisFinalizedAt = anticipation.AnalysisFinalizedAt;
            AnalysisStatus = anticipation.AnalysisStatus.ToString().ToLower();
            RequestedAmount = anticipation.RequestedAmount;
            AnticipatedAmount = anticipation.AnticipatedAmount;
        }

        public Guid Id { get; set; }
        public DateTime SolicitedAt { get; set; }
        public DateTime? AnalysisStartedAt { get; set; }
        public DateTime? AnalysisFinalizedAt { get; set; }
        public string AnalysisStatus { get; set; }
        public decimal RequestedAmount { get; set; }
        public decimal? AnticipatedAmount { get; set; }
    }
}