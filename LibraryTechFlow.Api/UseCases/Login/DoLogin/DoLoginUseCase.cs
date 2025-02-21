using LibraryTechFlow.Api.Infrastructure.DataAccess;
using LibraryTechFlow.Api.Infrastructure.Security.Cryptography;
using LibraryTechFlow.Api.Infrastructure.Security.Tokens.Access;
using LibraryTechFlow.Communication.Requests;
using LibraryTechFlow.Communication.Responses;
using LibraryTechFlow.Exception;

namespace LibraryTechFlow.Api.UseCases.Login.DoLogin
{
    public class DoLoginUseCase
    {
        private LibraryTechFlowDbContext _context;
        private BCryptAlgorithm _crpytography;
        private JwtTokenGenerator _tokenGenerator;

        public DoLoginUseCase(LibraryTechFlowDbContext context, BCryptAlgorithm crpytography, JwtTokenGenerator tokenGenerator)
        {
            _context = context;
            _crpytography = crpytography;
            _tokenGenerator = tokenGenerator;
        }

        public ResponseRegisteredUserJson Execute(RequestLoginJson request)
        {
            var entity = _context.Users.FirstOrDefault(x => x.Email == request.Email)
                ?? throw new InvalidLoginException();

            var isPasswordValid = _crpytography.Verify(request.Password, entity);

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
