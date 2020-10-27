namespace api.Models.ResultModel.Errors
{
    public class CardTransactionReprovedError : UnprocessableEntityError
    {
        public CardTransactionReprovedError()
        {
            Error = "CARD_TRANSACTION_REPROVED";
        }
    }
}