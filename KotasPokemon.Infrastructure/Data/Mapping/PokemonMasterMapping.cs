using KotasPokemon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KotasPokemon.Infrastructure.Data.Mapping
{
    class PokemonMasterMapping : IEntityTypeConfiguration<PokemonMaster>
    {
        public void Configure(EntityTypeBuilder<PokemonMaster> builder)
        {
            builder.ToTable("pokemon_masters");
            builder.Property(p => p.Id);
            builder.Property(p => p.Name).HasMaxLength(100);
            builder.Property(p => p.Age);
            builder.Property(p => p.Cpf).HasMaxLength(11);

        }
    }
}
