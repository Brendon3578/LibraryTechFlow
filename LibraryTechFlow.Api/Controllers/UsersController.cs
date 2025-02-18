using LibraryTechFlow.Api.UseCases.Users.Register;
using LibraryTechFlow.Communication.Requests;
using LibraryTechFlow.Communication.Responses;
using LibraryTechFlow.Exception;
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
        [ProducesResponseType(typeof(ReponseErrorMessagesJson), StatusCodes.Status400BadRequest)]
        public IActionResult Create(UserRequestJson request)
        {
            try
            {
                var response = _registerUseCase.Execute(request);

                return Created(nameof(Get), response);

            }
            catch (LibraryTechFlowException ex)
            {
                return StatusCode(
                    (int)ex.GetStatusCode(),
                    new ReponseErrorMessagesJson()
                    {
                        Errors = ex.GetErrorMessages()
                    });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ReponseErrorMessagesJson()
                {
                    Errors = ["Unknown error!"]
                });
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Created();
        }
    }
}
