using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarWorkshop.Application.CarWorkshop.EditValue
{
    public class GetCarWorkshopByEncodedNameToEditValidator : AbstractValidator<GetCarWorkshopByEncodedNameToEdit>
    {
        public GetCarWorkshopByEncodedNameToEditValidator()
        {
            RuleFor(c => c.Description)
                .NotEmpty().WithMessage("Please enter description");
            RuleFor(c => c.PhoneNumber)
                .MinimumLength(8)
                .MaximumLength(12);
        }
    }
}
