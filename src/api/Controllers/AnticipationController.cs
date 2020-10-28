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
    [Route("api/v1/anticipations")]
    public class AnticipationController : Controller
    {
        private readonly PaymentDbContext _context;
        private readonly RequestAnticipationProcessing _requestAnticipationProcessing;
        private readonly AnticipationAnalysisProcessing _anticipationAnalysisProcessing;

        public AnticipationController(
            PaymentDbContext context, 
            RequestAnticipationProcessing requestAnticipationProcessing, 
            AnticipationAnalysisProcessing anticipationAnalysisProcessing)
        {
            _context = context;
            _requestAnticipationProcessing = requestAnticipationProcessing;
            _anticipationAnalysisProcessing = anticipationAnalysisProcessing;
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet, Route("card-transactions-available")]
        public async Task<IActionResult> List()
        {
            var cardTransactions = await _context.CardTransctions
                .IncludeInstallments()
                .WhereAvailableForAnticipation()
                .ToListAsync();

            var cardTransactionsResultJson = cardTransactions.Select(ct => new CardTransactionResultJson(ct)).ToList();

            return Ok(cardTransactionsResultJson);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [HttpGet, Route("")]
        public async Task<IActionResult> ListHistoryAnticipations([FromQuery] string status)
        {
            var anticipations = await _context.Anticipations
                .WhereStatus(status)
                .ToListAsync();

            var anticipationsJson = anticipations.Select(a => new AnticipationResultJson(a)).ToList();

            return Ok(anticipationsJson);
        }

        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [HttpPost, Route("new-request/selected-card-transactions")]
        public async Task<IActionResult> Create([FromBody] AnticipationViewModel model)
        {
            await _requestAnticipationProcessing.Process(model.CardTransactionsNsus);

            if (_requestAnticipationProcessing.InProgess) return new HasAnticipationInProgressError();
            if (_requestAnticipationProcessing.AlreadyRequestedAnticipation) return new AlreadyRequestedAnticipationError();

            return Ok(new AnticipationResultJson(_requestAnticipationProcessing.Anticipation));
        }

        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [HttpPut, Route("request/start-attendance")]
        public async Task<IActionResult> StartAttendance()
        {
            var anticipation = await _context.Anticipations
                .WhereStatus("pending")
                .SingleOrDefaultAsync();

            if (anticipation == null) return new AnticipationNotFoundError();

            anticipation.StartAttendance();

            await _context.SaveChangesAsync();

            return NoContent();
        }

        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [HttpPut, Route("request/approve")]
        public async Task<IActionResult> Approve([FromBody] AnticipationViewModel model)
        {
            await _anticipationAnalysisProcessing.Approve(model.CardTransactionsNsus);

            if (_anticipationAnalysisProcessing.NotFound) return new AnticipationNotFoundError();
            if (_anticipationAnalysisProcessing.TransactionAlreadyAnalyzed) return new TransactionAlreadyAnalyzedInAnticipationError();

            return NoContent();
        }

        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.UnprocessableEntity)]
        [HttpPut, Route("request/reprove")]
        public async Task<IActionResult> Reprove([FromBody] AnticipationViewModel model)
        {
            await _anticipationAnalysisProcessing.Reprove(model.CardTransactionsNsus);

            if (_anticipationAnalysisProcessing.NotFound) return new AnticipationNotFoundError();
            if (_anticipationAnalysisProcessing.TransactionAlreadyAnalyzed) return new TransactionAlreadyAnalyzedInAnticipationError();

            return NoContent();
        }
        
    }
}