using System;
using api.Models.EntityModel.Core;

namespace api.Models.EntityModel
{
    public class Installment : Entity
    {
        public int Key { get; private set; }
        public Guid TransactionId { get; private set; }
        public CardTransaction Transaction { get; private set; }
        public decimal GrossAmount { get; private set; }
        public decimal NetAmount { get; private set; }
        public int Number { get; private set; }
        public decimal? AnticipatedValue { get; private set; }
        public DateTime ReceiptDate { get; private set; }
        public DateTime? TransferedAt { get; private set; }

        protected Installment() { }

        public Installment(int number, decimal grossAmount, decimal netAmount, DateTime receiptDate)
        {
            Key = Id.GetHashCode();
            Number = number;
            GrossAmount = grossAmount;
            NetAmount = netAmount;
            ReceiptDate = receiptDate;
        }
    }
}