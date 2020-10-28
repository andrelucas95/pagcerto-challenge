using System;
using api.Models.EntityModel.Core;

namespace api.Models.EntityModel
{
    public class Installment : Entity
    {
        protected Installment() { }

        public Installment(int number, decimal grossAmount, decimal netAmount, DateTime receiptDate)
        {
            Key = Math.Abs(Id.GetHashCode());
            Number = number;
            GrossAmount = grossAmount;
            NetAmount = netAmount;
            ReceiptDate = receiptDate;
        }

        public int Key { get; private set; }
        public Guid TransactionId { get; private set; }
        public CardTransaction Transaction { get; private set; }
        public decimal GrossAmount { get; private set; }
        public decimal NetAmount { get; private set; }
        public int Number { get; private set; }
        public decimal? AnticipatedValue { get; private set; }
        public DateTime ReceiptDate { get; private set; }
        public DateTime? TransferedAt { get; private set; }

        public void ApplyAnticipationFee()
        {
            AnticipatedValue = NetAmount - (3.8M * (NetAmount / 100));
        }

        public void Transfer() => TransferedAt = DateTime.Now;
    }
}