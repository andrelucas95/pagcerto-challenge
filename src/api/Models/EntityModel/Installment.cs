using System;
using api.Models.EntityModel.Core;

namespace api.Models.EntityModel
{
    public class Installment : Entity
    {
        public int Key { get; private set; }
        public Guid TransactionId { get; private set; }
        public Transaction Transaction { get; private set; }
        public decimal Amount { get; private set; }
        public decimal AmountPaid { get; private set; }
        public int Number { get; private set; }
        public decimal? AnticipatedValue { get; private set; }
        public DateTime ReceiptDate { get; private set; }
        public DateTime? TransferedAt { get; private set; }

        protected Installment() { }
    }
}