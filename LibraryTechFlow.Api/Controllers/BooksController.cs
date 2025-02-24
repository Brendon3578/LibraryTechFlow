using LibraryTechFlow.Api.UseCases.Books.Filter;
using LibraryTechFlow.Communication.Responses;
using Microsoft.AspNetCore.Mvc;

namespace LibraryTechFlow.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {

        private FilterBookUseCase _filterBookUseCase;

        public BooksController(FilterBookUseCase filterBookUseCase)
        {
            _filterBookUseCase = filterBookUseCase;
        }


        [HttpGet("Filter")]
        [ProducesResponseType(typeof(ResponsePaginatedBooksJson), StatusCodes.Status200OK)]
        public IActionResult Filter(int pageNumber, string? title)
        {
            var request = new RequestFilterBooksJson
            {
                PageNumber = pageNumber,
                Title = title
            };

            var response = _filterBookUseCase.Execute(request);

            return Ok(response);
        }

    }
}
