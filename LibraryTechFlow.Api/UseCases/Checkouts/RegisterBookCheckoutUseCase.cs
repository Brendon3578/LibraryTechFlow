using LibraryTechFlow.Api.Infrastructure.DataAccess;
using LibraryTechFlow.Api.Services.LoggedUser;
using LibraryTechFlow.Domain.Entities;

namespace LibraryTechFlow.Api.UseCases.Checkouts
{
    public class RegisterBookCheckoutUseCase
    {
        private readonly LoggedUserService _loggedUser;
        private readonly LibraryTechFlowDbContext _context;

        private const int MAX_LOAN_DAYS = 7;


        public RegisterBookCheckoutUseCase(LoggedUserService loggedUser, LibraryTechFlowDbContext context)
        {
            _loggedUser = loggedUser;
            _context = context;
        }

        public void Execute(Guid bookId, string authorizationRequest)
        {
            Validate(bookId);

            var user = _loggedUser.GetLoggedUser(authorizationRequest);

            var entity = new Checkout
            {
                UserId = user.Id,
                BookId = bookId,
                ExpectedReturnDate = DateTime.UtcNow.AddDays(MAX_LOAN_DAYS),
            };

            _context.Checkouts.Add(entity);
            _context.SaveChanges();
        }

        private void Validate(Guid bookId)
        {
            var book = _context.Books.Find(bookId)
                ?? throw new NotFoundException("Book not found");

            var amountBookNotReturned = _context.Checkouts
                .Count(c => c.BookId == bookId && c.ReturnedDate == null);

            if (amountBookNotReturned == book.Amount)
                throw new ConflictException("All books are already loaned");

        }
    }
}