using System;

namespace KotasPokemon.Domain.Core.DTOs
{
    public class CapturePokemonDTO
    {
        public Guid PokemonMasterId { get; set; }
        public int PokemonId { get; set; }
    }
}
