using System;
using System.Collections.Generic;
using api.Models.EntityModel.Core;

namespace api.Models.EntityModel
{
    public class Transaction : Entity, IAggregateRoot
    {
        public int Nsu { get; private set; }
        public DateTime? ApprovedAt { get; private set; }
        public DateTime? ReprovedAt { get; private set; }
        public bool Anticipated { get; private set; }
        public bool AcquirerConfirmation { get; private set; }
        public decimal Amount { get; private set; }
        public decimal AmountPaid => (Amount - Fee);
        public decimal Fee { get; private set; }
        public ICollection<Installment> Installments { get; private set; }
        public int InstallmentsCount => Installments.Count;
        public string CardFinal { get; set; }

        protected Transaction() { }
    }
}