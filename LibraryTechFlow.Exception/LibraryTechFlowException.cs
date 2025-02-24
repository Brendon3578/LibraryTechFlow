using System.Net;

namespace LibraryTechFlow.Exception
{
    public abstract class LibraryTechFlowException : SystemException
    {
        protected LibraryTechFlowException(string message) : base(message) { }
        public abstract IEnumerable<string> GetErrorMessages();
        public abstract HttpStatusCode GetStatusCode();
    }
}
