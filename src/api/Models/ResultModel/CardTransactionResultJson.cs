using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Models.EntityModel;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel
{
    public class CardTransactionResultJson : IActionResult
    {
        public CardTransactionResultJson() { }

        public CardTransactionResultJson(CardTransaction cardTransaction)
        {
            Nsu = cardTransaction.Nsu;
            ApprovedAt = cardTransaction.ApprovedAt;
            ReprovedAt = cardTransaction.ReprovedAt;
            GrossAmount = cardTransaction.GrossAmount;
            NetAmount = cardTransaction.NetAmount;
            Fee = cardTransaction.Fee;
            CardFinal = cardTransaction.CardFinal;
            Anticipated = cardTransaction.Anticipated;
            AcquirerConfirmation = cardTransaction.AcquirerConfirmation;
            Installments = cardTransaction.Installments.Select(installment => new InstallmentResultJson(installment)).ToList();
        }

        public int Nsu { get; set; }
        public DateTime? ApprovedAt { get; set; }
        public DateTime? ReprovedAt { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public decimal Fee { get; set; }
        public string CardFinal { get; set; }
        public bool Anticipated { get; set; }
        public bool AcquirerConfirmation { get; private set; }
        public List<InstallmentResultJson> Installments { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}