using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class UserRegisterValidation : AbstractValidator<UserRegisterDto>
    {
        public UserRegisterValidation()
        {
            RuleFor(u => u.Email).NotEmpty().EmailAddress();
            RuleFor(u => u.FirstName).NotEmpty();
            RuleFor(u=>u.LastName).NotEmpty();
            RuleFor(u => u.Password).NotEmpty().MinimumLength(8);
        }
    }
}
