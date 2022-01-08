using KotasPokemon.Domain.Core.DTOs;
using KotasPokemon.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KotasPokemon.Domain.Core.Interfaces
{
    public interface IPokemonRepository
    {
        Task AddPokemonMaster(PokemonMaster entity);
        Task<List<BaseDataPokemonResponseDTO>> GetTenRandomPokemons();
        Task<BaseDataPokemonResponseDTO> GetEspecificPokemon(int pokemonId);
    }
}
