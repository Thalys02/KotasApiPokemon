using KotasPokemon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KotasPokemon.Infrastructure.Data.Mapping
{
    public class PokemonCapturedMapping : IEntityTypeConfiguration<PokemonCaptured>
    {
        public void Configure(EntityTypeBuilder<PokemonCaptured> builder)
        {
            builder.ToTable("pokemon_captured");
            builder.Property(p => p.Id);
            builder.Property(p => p.PokemonId);
            builder.Property(p => p.PokemonMasterId);

        }
    }
}
