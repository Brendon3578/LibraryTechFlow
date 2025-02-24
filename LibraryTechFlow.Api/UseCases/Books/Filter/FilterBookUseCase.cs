using LibraryTechFlow.Api.Infrastructure.DataAccess;
using LibraryTechFlow.Communication.Responses;

namespace LibraryTechFlow.Api.UseCases.Books.Filter
{
    public class FilterBookUseCase
    {
        private const int PAGE_SIZE = 10;

        private readonly LibraryTechFlowDbContext _context;

        public FilterBookUseCase(LibraryTechFlowDbContext context)
        {
            _context = context;
        }

        public ResponsePaginatedBooksJson Execute(RequestFilterBooksJson request)
        {
            var skip = ((request.PageNumber - 1) * PAGE_SIZE);

            var query = _context.Books.AsQueryable();

            if (!string.IsNullOrEmpty(request.Title))
                query = query.Where(x => x.Title.Contains(request.Title));

            var books = query
                .OrderBy(b => b.Title).ThenBy(b => b.Author)
                .Skip(skip)
                .Take(PAGE_SIZE)
                .ToList();

            var totalCount = 0;

            if (string.IsNullOrEmpty(request.Title))
                totalCount = query.Count();
            else
                totalCount = _context.Books.Count(b => b.Title.Contains(request.Title));

            return new ResponsePaginatedBooksJson
            {
                Pagination = new ResponsePaginationJson
                {
                    PageNumber = request.PageNumber,
                    TotalCount = totalCount
                },
                Books = books.Select(b => new ResponseBookJson
                {
                    Id = b.Id,
                    Title = b.Title,
                    Author = b.Author,
                }).ToList()
            };
        }
    }
}
