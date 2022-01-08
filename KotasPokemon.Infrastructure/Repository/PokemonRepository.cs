using KotasPokemon.Domain.Core.DTOs;
using KotasPokemon.Domain.Core.Interfaces;
using KotasPokemon.Domain.Models;
using KotasPokemon.Infrastructure.AppSettings;
using KotasPokemon.Infrastructure.Data.Context;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace KotasPokemon.Infrastructure.Repository
{
    public class PokemonRepository : IPokemonRepository
    {

        private readonly PokemonContext _context;
        private readonly string _pokeApiSettings;
        private readonly int MAX_NUMBER_POKEMONS = 200;

        public PokemonRepository(IOptions<PokeApiSettings> pokeApiSettings, PokemonContext context)
        {
            _context = context;
            _pokeApiSettings = pokeApiSettings.Value.PokemonUrl;
        }

        public async Task<BaseDataPokemonResponseDTO> GetEspecificPokemon(int pokemonId)
        {

            var httpClient = new HttpClient();

            var uriPokemon = $"{_pokeApiSettings}{ pokemonId}";

            var responsePokemon = await httpClient.GetAsync(uriPokemon);

            var resultPokemon = JsonConvert.DeserializeObject<BaseDataPokemonResponseDTO>(await responsePokemon.Content.ReadAsStringAsync());

            return resultPokemon;
        }

        public async Task<List<BaseDataPokemonResponseDTO>> GetTenRandomPokemons()
        {
            var listPokemon = new List<BaseDataPokemonResponseDTO>();

            var httpClient = new HttpClient();


            do
            {
                Random random = new Random();

                var pokemonIdRandom = random.Next(0, MAX_NUMBER_POKEMONS);

                var uriPokemon = $"{_pokeApiSettings}{ pokemonIdRandom}";

                var responsePokemon = await httpClient.GetAsync(uriPokemon);

                if (responsePokemon.IsSuccessStatusCode)
                {
                    var resultPokemon = JsonConvert.DeserializeObject<BaseDataPokemonResponseDTO>(await responsePokemon.Content.ReadAsStringAsync());

                    listPokemon.Add(resultPokemon);
                }

            } while (listPokemon.Count <= 10);

            return listPokemon;
        }
        public async Task AddPokemonMaster(PokemonMaster entity)
        {
            try
            {
                await _context.AddAsync(entity);
                await _context.CommitAsync();
            }
            catch (Exception ex)
            {
                await _context.DisposeAsync();
                throw ex;
            }
        }

    }
}
