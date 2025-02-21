using FluentValidation;
using LibraryTechFlow.Communication.Requests;

namespace LibraryTechFlow.Api.UseCases.Users.Register
{
    public class RegisterUserValidator : AbstractValidator<RequestUserJson>
    {
        public RegisterUserValidator()
        {
            RuleFor(r => r.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(r => r.Email).NotEmpty().WithMessage("Email is required");
            RuleFor(r => r.Password).NotEmpty().WithMessage("Password is required");
            When(r => !string.IsNullOrEmpty(r.Password), () =>
            {
                RuleFor(r => r.Password).MinimumLength(6).WithMessage("Password must be at least 6 characters");
            });
        }
    }
}
