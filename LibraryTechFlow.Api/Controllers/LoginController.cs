using LibraryTechFlow.Api.UseCases.Login.DoLogin;
using LibraryTechFlow.Communication.Requests;
using LibraryTechFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTechFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly DoLoginUseCase _doLoginUseCase;

        public LoginController(DoLoginUseCase doLoginUseCase)
        {
            _doLoginUseCase = doLoginUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status401Unauthorized)]
        public IActionResult Post(RequestLoginJson request)
        {
            var response = _doLoginUseCase.Execute(request);

            return Ok(response);
        }
    }
}
