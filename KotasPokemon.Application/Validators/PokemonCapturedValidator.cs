using FluentValidation;
using KotasPokemon.Domain.Core.DTOs;
using KotasPokemon.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace KotasPokemon.Application.Validators
{
    public class PokemonCapturedValidator : AbstractValidator<CapturePokemonDTO>
    {
        public PokemonCapturedValidator(PokemonContext context)
        {
            RuleFor(w => w.PokemonId)
               .NotNull()
               .NotEmpty()
               .WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(w => w.PokemonMasterId)
                .NotNull()
                .NotEmpty()
                .WithMessage("O campo {PropertyName} precisa ser fornecido.");

            RuleFor(w => w.PokemonMasterId)
                .MustAsync(async (pokemoMasterId, cancellation) =>
                {
                    var pokemonMaster = await context.PokemonMasters.AsNoTracking().FirstOrDefaultAsync(w => w.Id == pokemoMasterId);

                    if (pokemonMaster is null) return false;

                    return true;
                }).WithMessage("O Mestre pokemon não está cadastrado, por gentileza realizar cadastro antes.");

            RuleFor(w => w.PokemonId)
                .MustAsync(async (pokemonId, cancellation) =>
                {
                    var IsPokemonCaptured = await context.PokemonsCaptured.AsNoTracking().FirstOrDefaultAsync(w => w.PokemonId == pokemonId);

                    if (IsPokemonCaptured != null) return false;

                    return true;
                }).WithMessage($"Esse Pokemon já foi capturado por gentileza tentar capturar outro.");
        }
    }
}
