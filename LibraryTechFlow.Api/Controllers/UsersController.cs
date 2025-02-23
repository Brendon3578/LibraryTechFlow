using LibraryTechFlow.Api.UseCases.Users.Register;
using LibraryTechFlow.Communication.Requests;
using LibraryTechFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTechFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {

        private RegisterUserUseCase _registerUseCase;

        public UsersController(RegisterUserUseCase registerUseCase)
        {
            _registerUseCase = registerUseCase;
        }

        [HttpPost]
        [ProducesResponseType(typeof(ResponseRegisteredUserJson), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ResponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
        public IActionResult Register(RequestUserJson request)
        {
            var response = _registerUseCase.Execute(request);

            return Created(nameof(Get), response);

        }

        [HttpGet]
        public IActionResult Get()
        {
            return Created();
        }
    }
}
