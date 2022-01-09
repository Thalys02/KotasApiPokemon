using KotasPokemon.Domain.Core.DTOs;
using KotasPokemon.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KotasPokemon.Domain.Core.Interfaces
{
    public interface IPokemonRepository
    {
        IQueryable GetAllPokemonMasters();
        IQueryable GetAllPokemonsCaptured();
        Task AddPokemonMaster(PokemonMaster entity);
        Task AddPokemonCaptured(PokemonCaptured entity);
        Task<List<BaseDataPokemonResponseDTO>> GetTenRandomPokemons();
        Task<BaseDataPokemonResponseDTO> GetEspecificPokemon(int pokemonId);
        Task<ResultTryCapturePokemon> TryCapturePokemon(CapturePokemonDTO dto);
    }
}
