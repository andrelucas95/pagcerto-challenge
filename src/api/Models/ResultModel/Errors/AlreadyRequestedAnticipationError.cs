namespace api.Models.ResultModel.Errors
{
    public class AlreadyRequestedAnticipationError : UnprocessableEntityError
    {
        public AlreadyRequestedAnticipationError()
        {
            Error = "ALREADY_REQUESTED_ANTICIPATION";
        }
    }
}