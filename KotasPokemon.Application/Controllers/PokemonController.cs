

using KotasPokemon.Domain.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KotasPokemon.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : Controller
    {
        public readonly IPokemonRepository pokemonRepository;
        public PokemonController(IPokemonRepository repository)
        {
            pokemonRepository = repository;
        }

        [HttpGet("{pokemonId}")]
        public async Task<IActionResult> GetEspecificPokemon(int pokemonId)
        {
            var result = await pokemonRepository.GetEspecificPokemon(pokemonId);

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetRandom()
        {
            var result = await pokemonRepository.GetTenRandomPokemons();

            return Ok(result);
        }
    }
}
