using LibraryTechFlow.Api.UseCases.Checkouts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTechFlow.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CheckoutsController : ControllerBase
    {

        private readonly RegisterBookCheckoutUseCase _registerBookCheckoutUseCase;

        public CheckoutsController(RegisterBookCheckoutUseCase registerBookCheckoutUseCase)
        {
            _registerBookCheckoutUseCase = registerBookCheckoutUseCase;
        }

        [HttpPost]
        [Route("{bookId}")]
        public IActionResult BookCheckout(Guid bookId)
        {
            var authorizationRequest = HttpContext.Request.Headers.Authorization.ToString();

            _registerBookCheckoutUseCase.Execute(bookId, authorizationRequest);

            return NoContent();
        }

    }
}
