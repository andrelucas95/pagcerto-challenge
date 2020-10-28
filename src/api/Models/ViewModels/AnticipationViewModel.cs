using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using api.Validations;

namespace api.Models.ViewModels
{
    public class AnticipationViewModel
    {
        [Display(Name = "cardTransactions"), JsonRequired]
        [MinLength(1, ErrorMessage = "card transaction must be informed")]
        public List<int> CardTransactionsNsus { get; set; }
    }
}