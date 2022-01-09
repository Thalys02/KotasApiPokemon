using System.Collections.Generic;

namespace KotasPokemon.Domain.Core.DTOs
{
    public class BaseDataPokemonResponseDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Height { get; set; }
        public string Weight { get; set; }
        public string Base_Experience { get; set; }
        public int CaptureRate { get; set; }
        public ICollection<TypePokemon> Types { get; set; } = new List<TypePokemon>();
        public ICollection<Abilities> Abilities { get; set; } = new List<Abilities>();
        public Sprint Sprites { get; set; }

    }

    public class TypePokemon
    {
        public TypeDescription Type { get; set; }
        public class TypeDescription
        {
            public string Name { get; set; }
        }

    }

    public class Abilities
    {
        public AbilityDescription Ability { get; set; }
        public string IsHidden { get; set; }
        public bool Slot { get; set; }
        public class AbilityDescription
        {
            public string Name { get; set; }
            public string Url { get; set; }
        }
    }

    public class Sprint
    {
        public string Front_default { get; set; }
        public string Back_default { get; set; }
    }

}
