using System.Net;

namespace LibraryTechFlow.Exception
{
    public class InvalidLoginException : LibraryTechFlowException
    {
        public override IEnumerable<string> GetErrorMessages() =>
            ["Email or password is invalid"];

        public override HttpStatusCode GetStatusCode() =>
            HttpStatusCode.Unauthorized;
    }
}
