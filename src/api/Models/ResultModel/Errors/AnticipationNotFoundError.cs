namespace api.Models.ResultModel.Errors
{
    public class AnticipationNotFoundError : UnprocessableEntityError
    {
        public AnticipationNotFoundError()
        {
            Error = "ANTICIPATION_NOT_FOUND";
        }
    }
}