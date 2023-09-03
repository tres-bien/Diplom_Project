using FluentValidation;

namespace Diplom_Project
{
    public class MemberValidator : AbstractValidator<Member>
    {
        public MemberValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().NotNull();
        }
    }
}
