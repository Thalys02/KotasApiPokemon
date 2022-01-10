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
    public class PokemonMasterController : Controller
    {
        public IMapper _mapper;
        public readonly IPokemonRepository _pokemonRepository;
        public PokemonMasterController(
                                        IMapper mapper,
                                        IPokemonRepository repository
                                      )
        {
            _mapper = mapper;
            _pokemonRepository = repository;
        }
        [HttpGet]
        public IActionResult AllPokemonMasters()
        {
            return Ok(_pokemonRepository.GetAllPokemonMasters());
        }

        [HttpPost]
        public async Task<IActionResult> AddPokemonMaster(AddPokemonMasterDTO dto)
        {
            PokemonMaster pokemonMaster = _mapper.Map<PokemonMaster>(dto);

            try
            {
                await _pokemonRepository.AddPokemonMaster(pokemonMaster);

                return Ok($"Mestre Pokemon: {dto.Name} cadastrado com sucesso.");
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
    }
}
