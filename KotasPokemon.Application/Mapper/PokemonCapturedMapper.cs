using AutoMapper;
using KotasPokemon.Domain.Core.DTOs;
using KotasPokemon.Domain.Models;

namespace KotasPokemon.Application.Mapper
{
    public class PokemonCapturedMapper : Profile
    {
        public PokemonCapturedMapper()
        {
            CreateMap<CapturePokemonDTO, PokemonCaptured>();
        }
    }
}
