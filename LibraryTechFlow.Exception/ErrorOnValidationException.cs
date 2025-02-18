using System.Net;

namespace LibraryTechFlow.Exception
{
    public class ErrorOnValidationException : LibraryTechFlowException
    {
        private readonly IEnumerable<string> _errors;

        public ErrorOnValidationException(IEnumerable<string> errorMessages)
        {
            _errors = errorMessages;
        }

        public override IEnumerable<string> GetErrorMessages() => _errors;

        public override HttpStatusCode GetStatusCode() => HttpStatusCode.BadRequest;
    }
}
