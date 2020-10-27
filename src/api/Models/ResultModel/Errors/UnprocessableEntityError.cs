using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace api.Models.ResultModel.Errors
{
    public class UnprocessableEntityError : IActionResult
    {
        public string Error { get; set; }

        public async Task ExecuteResultAsync(ActionContext context)
        {
            var json = new JsonResult(this) { StatusCode = 422 };
            await json.ExecuteResultAsync(context);
        }
    }
}