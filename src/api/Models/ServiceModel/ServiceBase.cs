using System.Collections.Generic;
using FluentValidation.Results;

namespace api.Models.ServiceModel
{
    public abstract class ServiceBase
    {
        public ValidationResult ValidationResult { get; private set; }

        protected ServiceBase()
        {
            ValidationResult = new ValidationResult();
        }

        protected void AddError(string message)
        {
            ValidationResult.Errors.Add(new ValidationFailure(string.Empty, message));
        }

        public bool HasErrors()
        {
            return ValidationResult.Errors.Count > 0;
        }
    }
}