using System;
using System.Collections.Generic;
using api.Models.EntityModel.Core;

namespace api.Models.EntityModel
{
    public class CardTransaction : Entity, IAggregateRoot
    {
        protected CardTransaction() { }

        public CardTransaction(string cardNumber, decimal amount)
        {
            Nsu = Id.GetHashCode();
            CardFinal = cardNumber.Substring(12);
            GrossAmount = amount;
            Installments = new List<Installment>();
        }

        public int Nsu { get; private set; }
        public DateTime? ApprovedAt { get; private set; }
        public DateTime? ReprovedAt { get; private set; }
        public bool Anticipated { get; private set; }
        public bool AcquirerConfirmation { get; private set; }
        public decimal GrossAmount { get; private set; }
        public decimal NetAmount { get; private set; }
        public decimal Fee { get; private set; }
        public ICollection<Installment> Installments { get; private set; }
        public int InstallmentsCount => Installments.Count;
        public string CardFinal { get; set; }

        public void Approve()
        {
            ApprovedAt = DateTime.Now;
            AcquirerConfirmation = true;
        } 
        
        public void Reprove()
        {
            ReprovedAt = DateTime.Now;
            AcquirerConfirmation = false;
        }

        public bool IsApproved() => ApprovedAt != null;

        public void ChargeFee(decimal fee)
        {
            Fee = fee;
            NetAmount = GrossAmount - Fee;
        }

        public void AddInstallments(int installmentsCount)
        {
            if (!ApprovedAt.HasValue)
                throw new Exception("Transaction required be approved");

            if (installmentsCount == 0) return;

            var installmentValue = (NetAmount / installmentsCount);

            for (int installmentNumber = 1; installmentNumber <= installmentsCount; installmentNumber++)
            {
                Installments.Add(new Installment(
                    installmentNumber, 
                    installmentValue, 
                    installmentValue, 
                    ApprovedAt.Value.AddDays(installmentNumber * 30)));
            }
        }
    }
}