using System.Linq;
using api.Models.EntityModel;
using Microsoft.EntityFrameworkCore;

namespace api.Infrastructure.Queries
{
    public static class CardTransactionQuery
    {
        public static IQueryable<CardTransaction> IncludeInstallments(this IQueryable<CardTransaction> cardTransactions)
        {
            return cardTransactions.Include(ct => ct.Installments);
        }

        public static IQueryable<CardTransaction> WhereNsu(this IQueryable<CardTransaction> cardTransactions, int nsu)
        {
            return cardTransactions.Where(ct => ct.Nsu == nsu);
        }

        public static IQueryable<CardTransaction> WhereAvailableForAnticipation(this IQueryable<CardTransaction> cardTransactions)
        {
            return cardTransactions.Where(ct => ct.Anticipated == null);
        }

    }
}