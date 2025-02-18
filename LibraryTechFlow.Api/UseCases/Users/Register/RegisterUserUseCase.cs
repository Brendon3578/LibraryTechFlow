using LibraryTechFlow.Api.Domain.Entities;
using LibraryTechFlow.Api.Infrastructure;
using LibraryTechFlow.Communication.Requests;
using LibraryTechFlow.Communication.Responses;
using LibraryTechFlow.Exception;

namespace LibraryTechFlow.Api.UseCases.Users.Register
{
    public class RegisterUserUseCase
    {
        private RegisterUserValidator _validator;

        public RegisterUserUseCase(RegisterUserValidator validator)
        {
            _validator = validator;
        }

        public ResponseRegisteredUserJson Execute(UserRequestJson request)
        {
            Validate(request);

            var entity = new User
            {
                Name = request.Name,
                Email = request.Email,
                Password = request.Password
            };

            var dbContext = new LibraryTechFlowDbContext();

            dbContext.Users.Add(entity);
            dbContext.SaveChanges();


            return new ResponseRegisteredUserJson()
            {
                Name = entity.Name,
            };
        }

        private void Validate(UserRequestJson request)
        {
            var result = _validator.Validate(request);

            var isNotValid = !result.IsValid;

            if (isNotValid)
            {
                var errorMessages = result.Errors.Select(errors => errors.ErrorMessage);

                throw new ErrorOnValidationException(errorMessages);
            }
        }
    }
}
