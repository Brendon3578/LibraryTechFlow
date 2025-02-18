using System.Net;

namespace LibraryTechFlow.Exception
{
    public abstract class LibraryTechFlowException : SystemException
    {
        public abstract IEnumerable<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
