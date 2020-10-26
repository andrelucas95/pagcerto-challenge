using System;
using System.Threading.Tasks;
using api.Models.EntityModel;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel
{
    public class InstallmentResultJson : IActionResult
    {
        public InstallmentResultJson() { }

        public InstallmentResultJson(Installment installment)
        {
            Key = installment.Key;
            TransactionId = installment.TransactionId;
            GrossAmount = installment.GrossAmount;
            NetAmount = installment.NetAmount;
            Number = installment.Number;
            AnticipatedValue = installment.AnticipatedValue;
            ReceiptDate = installment.ReceiptDate;
            TransferedAt = installment.TransferedAt;
        }

        public int Key { get; set; }
        public Guid TransactionId { get; set; }
        public decimal GrossAmount { get; set; }
        public decimal NetAmount { get; set; }
        public int Number { get; set; }
        public decimal? AnticipatedValue { get; set; }
        public DateTime ReceiptDate { get; set; }
        public DateTime? TransferedAt { get; set; }

        public Task ExecuteResultAsync(ActionContext context)
        {
            return new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}