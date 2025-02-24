using LibraryTechFlow.Exception;
using System.Net;

namespace LibraryTechFlow.Api.UseCases.Checkouts
{
    public class ConflictException : LibraryTechFlowException
    {
        public ConflictException(string message) : base(message)
        {
        }

        public override IEnumerable<string> GetErrorMessages() =>
            [Message];

        public override HttpStatusCode GetStatusCode() =>
            HttpStatusCode.Conflict;
    }
}