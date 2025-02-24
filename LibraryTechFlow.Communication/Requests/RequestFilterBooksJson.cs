namespace LibraryTechFlow.Api.UseCases.Books.Filter
{
    public class RequestFilterBooksJson
    {
        public int PageNumber { get; set; }
        public string? Title { get; set; }
    }
}