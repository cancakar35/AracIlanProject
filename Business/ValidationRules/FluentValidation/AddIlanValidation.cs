using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class AddIlanValidation : AbstractValidator<AddIlanDto>
    {
        public AddIlanValidation()
        {
            RuleFor(ilan => ilan.TelefonNo)
                .NotEmpty()
                .NotNull().WithMessage("Telefon Numarası Zorunludur")
                .MinimumLength(10).WithMessage("Telefon numarası en az 10 karakter olmalıdır")
                .MaximumLength(20).WithMessage("Telefon numarası en fazla 20 karakter olmalıdır")
                .Matches(@"^(((\+)?(90)|0)[-|\s]?)?((\d{3})[-|\s]?(\d{3})[-|\s]?(\d{2})[-|\s]?(\d{2}))$").WithMessage("Lütfen geçerli bir telefon numarası giriniz");
        }
    }
}
