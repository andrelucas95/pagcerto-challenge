using System.Collections.Generic;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel.Errors
{
    public class ProcessingErrorResultJson : IActionResult
    {
        public ProcessingErrorResultJson(ValidationResult validationResult)
        {
            Errors = new List<string>();
            
            foreach (var erro in validationResult.Errors)
            {
                Errors.Add(erro.ErrorMessage);
            }
        }

        public List<string> Errors { get; private set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var json = new JsonResult(this) { StatusCode = 422 };
            await json.ExecuteResultAsync(context);
        }
    }
}