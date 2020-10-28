using System.Linq;
using api.Models.EntityModel;
using api.Models.EntityModel.Enums;

namespace api.Infrastructure.Queries
{
    public static class AnticipationQuery
    {
        public static IQueryable<Anticipation> WhereStatus(this IQueryable<Anticipation> anticipations, string status)
        {
            switch (status)
            {
                case "pending":
                    return anticipations.Where(a => a.AnalysisStartedAt == null);
                case "inAnalysis":
                    return anticipations.Where(a => a.AnalysisStartedAt != null && a.AnalysisFinalizedAt == null);
                case "finalized":
                    return anticipations.Where(a => a.AnalysisFinalizedAt != null);
                default:
                    return anticipations;
            }
        }
    }
}