using FluentValidation;

namespace Diplom_Project.Model.Validators
{
    public class MemberValidator : AbstractValidator<Member>
    {
        public MemberValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
        }
    }
}
