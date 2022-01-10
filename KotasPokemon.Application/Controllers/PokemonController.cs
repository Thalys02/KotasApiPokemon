

using AutoMapper;
using KotasPokemon.Domain.Core.DTOs;
using KotasPokemon.Domain.Core.Interfaces;
using KotasPokemon.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace KotasPokemon.Application.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PokemonController : Controller
    {
        private readonly IMapper _mapper;
        public readonly IPokemonRepository _pokemonRepository;
        public PokemonController(
                                 IPokemonRepository repository,
                                 IMapper mapper
                                )
        {
            _pokemonRepository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> GetRandom()
        {
            var result = await _pokemonRepository.GetTenRandomPokemons();

            return Ok(result);
        }

        [HttpGet("{pokemonId}")]
        public async Task<IActionResult> GetEspecificPokemon(int pokemonId)
        {
            var result = await _pokemonRepository.GetEspecificPokemon(pokemonId);

            return Ok(result);
        }

        [HttpGet("PokemonsCaptured")]
        public IActionResult PokemonsCaptured()
        {
            return Ok(_pokemonRepository.GetAllPokemonsCaptured());
        }

        [HttpPost("CapturePokemon")]
        public async Task<IActionResult> CapturePokemon(CapturePokemonDTO dto)
        {
            var result = await _pokemonRepository.TryCapturePokemon(dto);

            if (result.percentageSuccess.ToString().Length > 4)
                result.percentageSuccess = Convert.ToDouble(result.percentageSuccess.ToString().Substring(0, 4));

            if (result.PokemonCaptured)
            {
                PokemonCaptured pokemonCaptured = _mapper.Map<PokemonCaptured>(dto);

                pokemonCaptured.PercentageSuccess = result.percentageSuccess;

                pokemonCaptured.PokemonName = result.Pokemon.Name;

                await _pokemonRepository.AddPokemonCaptured(pokemonCaptured);

                return Ok($"Parabéns você conseguiu capturar o pokemon:{result.Pokemon.Name}, probabilidade de captura: {result.percentageSuccess}%");
            }

            return Ok($"Infelizmente você não conseguiu capturar o pokemon:{result.Pokemon.Name}, probabilidade de captura: {result.percentageSuccess}%, mas pode tentar novamente.");
        }

    }
}
