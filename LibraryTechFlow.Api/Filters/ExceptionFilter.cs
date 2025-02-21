using LibraryTechFlow.Communication.Responses;
using LibraryTechFlow.Exception;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace LibraryTechFlow.Api.Filters
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is LibraryTechFlowException exception)
            {
                context.HttpContext.Response.StatusCode = (int)exception.GetStatusCode();


                context.Result = new ObjectResult(new RepsonseErrorMessagesJson()
                {
                    Errors = exception.GetErrorMessages()
                })
                {
                    StatusCode = (int)exception.GetStatusCode()
                };
            }
            else
            {
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult(new RepsonseErrorMessagesJson()
                {
                    Errors = ["Unknown error!"]
                });
            }
        }
    }
}
