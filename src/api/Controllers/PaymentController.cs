using System.Linq;
using System.Net;
using System.Threading.Tasks;
using api.Infrastructure;
using api.Infrastructure.Queries;
using api.Models.ResultModel;
using api.Models.ResultModel.Errors;
using api.Models.ServiceModel;
using api.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace api.Controllers
{
    [ApiController]
    [Route("api/v1")]
    public class PaymentController : Controller
    {
        private readonly PaymentProcessing _paymentProcessing;
        private readonly PaymentDbContext _context;

        public PaymentController(PaymentProcessing paymentProcessing, PaymentDbContext context)
        {
            _paymentProcessing = paymentProcessing;
            _context = context;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet, Route("partner/transactions/{nsu:int}")]
        public async Task<IActionResult> Find([FromRoute] int nsu)
        {
            var cardTransaction = await _context.CardTransctions
                .IncludeInstallments()
                .WhereNsu(nsu)
                .SingleOrDefaultAsync();

            return Ok(new CardTransactionResultJson(cardTransaction));
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [HttpPost, Route("pay/cards")]
        public async Task<IActionResult> Create(
            [FromBody] CreatePaymentViewModel model)
        {
            await _paymentProcessing.Process(model.CardNumber, model.Installments.Value, model.Amount.Value);

            if (_paymentProcessing.HasErrors()) return new ProcessingErrorResultJson(_paymentProcessing.ValidationResult);

            return Ok(new CardTransactionResultJson(_paymentProcessing.CardTransaction));
        }
    }
}