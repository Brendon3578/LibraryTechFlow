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
                context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Result = new ObjectResult(new ResponseErrorMessagesJson()
                {
                    Errors = ["Unknown error!"]
                });
            }
        }
    }
}
