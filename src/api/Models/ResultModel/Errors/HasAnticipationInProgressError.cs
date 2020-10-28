namespace api.Models.ResultModel.Errors
{
    public class HasAnticipationInProgressError : UnprocessableEntityError
    {
        public HasAnticipationInProgressError()
        {
            Error = "HAS_ANTICIPATION_IN_PROGRESS";
        }
    }
}