using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Mime;

namespace DelinquencyManagement.API.Controllers.v1.PaymentsContext
{
    /// <summary>
    /// Operations related to debt instrument management with idempotency support
    /// </summary>
    [ApiController]
    [ApiVersion("1.0")]
    [Route("/api/customers/")]
    public class DebtInstrumentController : Controller
    {
        private readonly ActivitySource _activitySource;

        public DebtInstrumentController(ActivitySource activitySource)
        {
            _activitySource = activitySource;
        }

        /// <summary>
        /// Creates a new debt instrument for a customer with idempotency support
        /// </summary>
        [HttpPost("{merchantDocument}/debt-instruments")]
        [MapToApiVersion("1.0")]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        //[ProducesResponseType(typeof(CreatedDebtInstrumentOutput), StatusCodes.Status201Created, MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status400BadRequest, MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError, MediaTypeNames.Application.Json)]
        public async Task<ActionResult> CreateDebtInstrument()
        {
            using Activity activity = _activitySource.StartActivity("Test Datadog", ActivityKind.Server)!;

            return Ok();
        }
    }
}
