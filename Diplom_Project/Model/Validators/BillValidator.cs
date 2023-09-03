using FluentValidation;

namespace Diplom_Project
{
    public class BillValidator : AbstractValidator<Bill>
    {
        public BillValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Please, enter name");
            RuleFor(x => x.Total).NotEqual(0).WithMessage("Please, enter total");
            RuleFor(x => x.Members).NotNull().NotEmpty().WithMessage("You must add at least one member");
        }
    }
}
