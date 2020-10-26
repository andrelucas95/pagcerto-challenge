using System.ComponentModel.DataAnnotations;

namespace api.Validations
{
    public class JsonRequired : RequiredAttribute
    {
        public JsonRequired()
        {
            ErrorMessage = "{0}: Required.";
        }
    }
}