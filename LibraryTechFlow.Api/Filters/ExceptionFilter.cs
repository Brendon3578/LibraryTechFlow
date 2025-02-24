using LibraryTechFlow.Communication.Responses;
using LibraryTechFlow.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryTechFlow.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {

        private readonly ILogger<ExceptionFilter> _logger;

        public ExceptionFilter(ILogger<ExceptionFilter> logger)
        {
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {


            if (context.Exception is LibraryTechFlowException ex)
            {
                context.HttpContext.Response.StatusCode = (int)ex.GetStatusCode();

                context.Result = new ObjectResult(new ResponseErrorMessagesJson()
                {
                    Errors = ex.GetErrorMessages()
                })
                {
                    StatusCode = (int)ex.GetStatusCode()
                }; ;
            }
            else
            {
                _logger.LogError(context.Exception, "An error occurred: {Message}", context.Exception.Message);


                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult(new ResponseErrorMessagesJson()
                {
                    Errors = ["Unknown error!"]
                });
            }
        }
    }
}
