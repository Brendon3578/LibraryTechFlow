using FluentValidation.Results;
using LibraryTechFlow.Api.Infrastructure.DataAccess;
using LibraryTechFlow.Api.Infrastructure.Security.Tokens.Access;
using LibraryTechFlow.Communication.Requests;
using LibraryTechFlow.Communication.Responses;
using LibraryTechFlow.Domain.Entities;
using LibraryTechFlow.Exception;
using LibraryTechFlow.Security.Interfaces;

namespace LibraryTechFlow.Api.UseCases.Users.Register
{
    public class RegisterUserUseCase
    {
        private RegisterUserValidator _validator;
        private LibraryTechFlowDbContext _dbContext;
        private JwtTokenGenerator _tokenGenerator;
        private readonly ICryptographyAlgorithm _cryptography;

        public RegisterUserUseCase(RegisterUserValidator validator, LibraryTechFlowDbContext dbContext, JwtTokenGenerator tokenGenerator, ICryptographyAlgorithm cryptography)
        {
            _validator = validator;
            _dbContext = dbContext;
            _tokenGenerator = tokenGenerator;
            _cryptography = cryptography;
        }

        public ResponseRegisteredUserJson Execute(RequestUserJson request)
        {
            Validate(request);

            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = _cryptography.HashPassword(request.Password)
            };


            _dbContext.Users.Add(entity);
            _dbContext.SaveChanges();


            return new ResponseRegisteredUserJson()
            {
                Name = entity.Name,
                AccessToken = _tokenGenerator.Generate(entity)
            };
        }

        private void Validate(RequestUserJson request)
        {
            var result = _validator.Validate(request);

            var existsUserWithSameEmail = _dbContext.Users.Any(u => u.Email.Equals(request.Email));

            if (existsUserWithSameEmail)
                result.Errors.Add(new ValidationFailure("Email", "E-mail already registered!"));

            var isNotValid = !result.IsValid;

            if (isNotValid)
            {
                var errorMessages = result.Errors.Select(errors => errors.ErrorMessage);

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
