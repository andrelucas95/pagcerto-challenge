using api.Models.EntityModel;

namespace tests.Factories
{
    public static class CreateCardPaymentFactory
    {
        public static CardPayment Build(this CardPayment cardPayment)
        {
            cardPayment.Amount = 100;
            cardPayment.Installments = 2;

            return cardPayment;
        }

        public static CardPayment WithValidCardNumber(this CardPayment cardPayment)
        {
            cardPayment.CardNumber = "5899779275282813";

            return cardPayment;
        }

        public static CardPayment WithInvalidValidCardNumber(this CardPayment cardPayment)
        {
            cardPayment.CardNumber = "5999779275282813";

            return cardPayment;
        }

    }
}