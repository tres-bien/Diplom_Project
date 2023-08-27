using FluentValidation;

namespace Diplom_Project
{
    public class BillValidator : AbstractValidator<Bill>
    {
        public BillValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Please, enter name");
            RuleFor(x => x.Total).NotNull().NotEmpty();
            RuleFor(x => x.Members).NotNull().NotEmpty().WithMessage("You must add at least one member");
        }
    }
}
