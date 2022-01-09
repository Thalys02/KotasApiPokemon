namespace KotasPokemon.Domain.Core.DTOs
{
    public class ResultTryCapturePokemon
    {
        public BaseDataPokemonResponseDTO Pokemon { get; set; }
        public int CaptureRate { get; set; }
        public double percentageSuccess { get; set; }
        public bool PokemonCaptured { get; set; }
    }
}
