using System.Net;

namespace LibraryTechFlow.Exception
{
    public class InvalidLoginException : LibraryTechFlowException
    {
        public InvalidLoginException() : base("E-mail or password invalid.")
        {
        }

        public override IEnumerable<string> GetErrorMessages() => [Message];

        public override HttpStatusCode GetStatusCode() =>
            HttpStatusCode.Unauthorized;
    }
}
