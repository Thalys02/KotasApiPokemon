using FluentValidation;
using KotasPokemon.Domain.Core.DTOs;
using KotasPokemon.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace KotasPokemon.Application.Validators
{
    public class PokemonMasterValidator : AbstractValidator<AddPokemonMasterDTO>
    {
        public PokemonMasterValidator(PokemonContext context)
        {
            RuleFor(w => w.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(w => w.Age)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(w => w.Cpf)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(w => w.Cpf)
                .MustAsync(async (cpf, cancellation) =>
                {
                    var cpfValid = cpf.Replace(".", "").Replace("-", "");

                    if (cpfValid.Length == 11) return true;

                    return false;
                }).WithMessage("O campo {PropertyName} está inválido, precisa ter no minimo 11 caracteres.");

            RuleFor(w => w.Cpf)
                .MustAsync(async (cpf, cancellation) =>
                {
                    var pokemonMaster = await context.PokemonMasters.AsNoTracking().FirstOrDefaultAsync(w => w.Cpf == cpf);

                    if (pokemonMaster != null) return false;

                    return true;
                }).WithMessage("O Mestre pokemon informado já encontra-se cadastrado.");
        }
    }
}
