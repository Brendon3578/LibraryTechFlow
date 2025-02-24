using LibraryTechFlow.Api.Infrastructure.DataAccess;
using LibraryTechFlow.Api.Infrastructure.Security.Tokens.Access;
using LibraryTechFlow.Communication.Requests;
using LibraryTechFlow.Communication.Responses;
using LibraryTechFlow.Exception;
using LibraryTechFlow.Security.Interfaces;

namespace LibraryTechFlow.Api.UseCases.Login.DoLogin
{
    public class DoLoginUseCase
    {
        private readonly LibraryTechFlowDbContext _context;
        private readonly JwtTokenGenerator _tokenGenerator;
        private readonly ICryptographyAlgorithm _cryptography;

        public DoLoginUseCase(LibraryTechFlowDbContext context, JwtTokenGenerator tokenGenerator, ICryptographyAlgorithm cryptography)
        {
            _context = context;
            _tokenGenerator = tokenGenerator;
            _cryptography = cryptography;
        }

        public ResponseRegisteredUserJson Execute(RequestLoginJson request)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Email == request.Email)
                ?? throw new InvalidLoginException();

            var isPasswordValid = _cryptography.Verify(request.Password, entity);

            if (!isPasswordValid)
                throw new InvalidLoginException();

            var token = _tokenGenerator.Generate(entity);

            return new ResponseRegisteredUserJson
            {
                Name = entity.Name,
                AccessToken = token
            };
        }
    }
}
