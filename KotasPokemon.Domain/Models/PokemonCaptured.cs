using System;

namespace KotasPokemon.Domain.Models
{
    public class PokemonCaptured : BaseEntity
    {
        public PokemonCaptured(int pokemonId, Guid pokemonMasterId)
        {
            PokemonId = pokemonId;
            PokemonMasterId = pokemonMasterId;
        }
        public int PokemonId { get; set; }
        public string PokemonName { get; set; }
        public Guid PokemonMasterId { get; set; }
        public double PercentageSuccess { get; set; }
        public PokemonMaster PokemonMaster { get; set; }
    }
}
