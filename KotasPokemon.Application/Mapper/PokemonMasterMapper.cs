using AutoMapper;
using KotasPokemon.Domain.Core.DTOs;
using KotasPokemon.Domain.Models;

namespace KotasPokemon.Application.Mapper
{
    public class PokemonMasterMapper : Profile
    {
        public PokemonMasterMapper()
        {
            CreateMap<AddPokemonMasterDTO, PokemonMaster>();
        }
    }
}
