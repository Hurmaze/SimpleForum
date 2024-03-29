﻿using Services.Models;
using FluentValidation;

namespace Services.Validation.FluentValidation
{
    public class RoleModelValidator : AbstractValidator<RoleModel>
    {
        public RoleModelValidator()
        {
            RuleFor(x => x.RoleName)
                .NotEmpty()
                .MaximumLength(50);
               
        }
    }
}
