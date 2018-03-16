using Admin2.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(x => x.Nome).NotNull().WithMessage("Nome é obrigatório");
            RuleFor(x => x.Email).NotNull().WithMessage("Email é obrigatório");
        }
    }
}
