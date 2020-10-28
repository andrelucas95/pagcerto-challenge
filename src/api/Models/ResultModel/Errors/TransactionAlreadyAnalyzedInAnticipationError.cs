namespace api.Models.ResultModel.Errors
{
    public class TransactionAlreadyAnalyzedInAnticipationError : UnprocessableEntityError
    {
        public TransactionAlreadyAnalyzedInAnticipationError()
        {
            Error = "TRANSACTION_ALREADY_ANALYZED";
        }
    }
}