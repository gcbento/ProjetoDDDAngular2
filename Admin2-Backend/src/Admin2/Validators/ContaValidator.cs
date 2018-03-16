using Admin2.Domain.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Admin2.Validators
{
    public class ContaValidator : AbstractValidator<Conta>
    {
        public ContaValidator()
        {
            RuleFor(x => x.Email).NotNull().WithMessage("Email é obrigatório");
            RuleFor(x => x.Senha).NotNull().WithMessage("Senha é obrigatório");
            RuleFor(x => x.IdOnline).NotNull().WithMessage("ID Online é obrigatório");
            RuleFor(x => x.TipoConta.Id).NotNull().WithMessage("Tipo Conta é obrigatório");
            RuleFor(x => x.DataNascimento).NotNull().WithMessage("Data de nascimento é obrigatório");
            RuleFor(x => x.Status).NotNull().WithMessage("Status é obrigatório");
            RuleFor(x => x.Status).NotNull().WithMessage("Ativa é obrigatório");
            RuleFor(x => x.Status).NotNull().WithMessage("Data de desativação é obrigatório");
        }
    }
}
