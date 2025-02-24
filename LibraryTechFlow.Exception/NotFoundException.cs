
using LibraryTechFlow.Exception;
using System.Net;

namespace LibraryTechFlow.Api.UseCases.Checkouts
{
    public class NotFoundException : LibraryTechFlowException
    {
        public NotFoundException(string message) : base(message)
        {
        }

        public override IEnumerable<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() =>
            HttpStatusCode.NotFound;
    }
}