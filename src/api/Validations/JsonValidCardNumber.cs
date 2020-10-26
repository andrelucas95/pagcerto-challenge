using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace api.Validations
{
    public class JsonValidCardNumber : ValidationAttribute
    {
        public JsonValidCardNumber()
        {
            ErrorMessage = "{0} Invalid.";
        }
        public override bool IsValid(object value)
        {
            var cardNumber = value as string;
            Regex pattern = new Regex("^[a-zA-Z0-9]*$");
            
            return !string.IsNullOrEmpty(cardNumber) && pattern.IsMatch(cardNumber) && cardNumber.Length == 16;
        }
    }
}