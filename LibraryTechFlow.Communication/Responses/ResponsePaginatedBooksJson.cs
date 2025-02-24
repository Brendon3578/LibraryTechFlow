namespace LibraryTechFlow.Communication.Responses
{
    public class ResponsePaginatedBooksJson
    {
        public ResponsePaginationJson Pagination { get; set; } = default!;
        public List<ResponseBookJson> Books { get; set; } = [];
    }
}
