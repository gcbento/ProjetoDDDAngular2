﻿using Admin2.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Validators
{
    public class VendaValidator : AbstractValidator<Venda>
    {
        public VendaValidator()
        {
            //RuleFor(x => x.Descricao).NotNull().WithMessage("Descricao é obrigatório");
            //RuleFor(x => x.Valor).NotNull().WithMessage("Valor é obrigatório");
        }
    }
}
