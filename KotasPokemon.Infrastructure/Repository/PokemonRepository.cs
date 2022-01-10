using KotasPokemon.Domain.Core.DTOs;
using KotasPokemon.Domain.Core.Interfaces;
using KotasPokemon.Domain.Models;
using KotasPokemon.Infrastructure.AppSettings;
using KotasPokemon.Infrastructure.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace KotasPokemon.Infrastructure.Repository
{
    public class PokemonRepository : IPokemonRepository
    {

        private readonly PokemonContext _context;
        private readonly string _pokeApiSettings;
        private readonly string _pokeSpecieApiSettings;
        private readonly int MAX_NUMBER_POKEMONS = 200;
        private readonly int MAX_NUMBER_RATE_CAPTURE_POKEMON = 255;

        public PokemonRepository(IOptions<PokeApiSettings> pokeApiSettings, PokemonContext context)
        {
            _context = context;
            _pokeApiSettings = pokeApiSettings.Value.PokemonUrl;
            _pokeSpecieApiSettings = pokeApiSettings.Value.PokemonSpecieUrl;
        }

        public IQueryable GetAllPokemonMasters()
        {
            return _context.Set<PokemonMaster>().AsNoTracking().Select(s => new { s.Id, s.Name, s.Age }).AsQueryable();
        }

        public IQueryable GetAllPokemonsCaptured()
        {
            return _context.Set<PokemonCaptured>().Include(i => i.PokemonMaster).AsNoTracking().Select(s => new { s.PokemonId, s.PokemonName, PokemonMasterName = s.PokemonMaster.Name }).AsQueryable();
        }

        public async Task<BaseDataPokemonResponseDTO> GetEspecificPokemon(int pokemonId)
        {

            var httpClient = new HttpClient();

            var uriPokemon = $"{_pokeApiSettings}{ pokemonId}";

            var responsePokemon = await httpClient.GetAsync(uriPokemon);

            var resultPokemon = JsonConvert.DeserializeObject<BaseDataPokemonResponseDTO>(await responsePokemon.Content.ReadAsStringAsync());

            resultPokemon.Sprites.Front_default = await ConvertUrlToImageBase64(resultPokemon.Sprites.Front_default);

            resultPokemon.Sprites.Back_default = await ConvertUrlToImageBase64(resultPokemon.Sprites.Back_default);

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

                    resultPokemon.Sprites.Back_default = await ConvertUrlToImageBase64(resultPokemon.Sprites.Back_default);

                    resultPokemon.Sprites.Front_default = await ConvertUrlToImageBase64(resultPokemon.Sprites.Front_default);

                    listPokemon.Add(resultPokemon);
                }

            } while (listPokemon.Count <= 9);

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

        public async Task<ResultTryCapturePokemon> TryCapturePokemon(CapturePokemonDTO dto)
        {
            try
            {
                Random random = new Random();

                var resultCapturePokemon = new ResultTryCapturePokemon();

                var pokemon = await GetEspecificPokemon(dto.PokemonId); 

                var chance = random.Next(1, MAX_NUMBER_RATE_CAPTURE_POKEMON);

                resultCapturePokemon.Pokemon = pokemon;

                resultCapturePokemon.CaptureRate = await GetCaptureRate(pokemon.Id);

                resultCapturePokemon.percentageSuccess = Convert.ToDouble(resultCapturePokemon.CaptureRate) / Convert.ToDouble(MAX_NUMBER_RATE_CAPTURE_POKEMON) * 100;

                if (chance <= resultCapturePokemon.CaptureRate)
                {
                    resultCapturePokemon.PokemonCaptured = true;

                    return resultCapturePokemon;
                }

                resultCapturePokemon.PokemonCaptured = false;

                return resultCapturePokemon;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task AddPokemonCaptured(PokemonCaptured entity)
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

        private async Task<int> GetCaptureRate(int pokemonId)
        {
            var httpClient = new HttpClient();

            var uriPokemon = $"{_pokeSpecieApiSettings}{ pokemonId}";

            var responsePokemon = await httpClient.GetAsync(uriPokemon);

            var resultPokemon = JsonConvert.DeserializeObject<SpeciePokemonDTO>(await responsePokemon.Content.ReadAsStringAsync());

            return resultPokemon.Capture_Rate;

        }

        private async static Task<string> ConvertUrlToImageBase64(string url)
        {
            using var client = new HttpClient();

            var bytes = await client.GetByteArrayAsync(url);

            return "data:image/jpeg;base64," + Convert.ToBase64String(bytes);

        }

    }
}
