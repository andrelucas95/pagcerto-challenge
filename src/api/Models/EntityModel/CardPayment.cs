namespace api.Models.EntityModel
{
    public class CardPayment
    {
        public string CardNumber { get; set; }
        public decimal Amount { get; set; }
        public int Installments { get; set; }
    }
}