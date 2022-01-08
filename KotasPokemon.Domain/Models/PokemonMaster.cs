namespace KotasPokemon.Domain.Models
{
    public class PokemonMaster : BaseEntity
    {
        public PokemonMaster(string name, int age, string cpf)
        {
            Name = name;
            Age = age;
            Cpf = cpf;
        }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Cpf { get; set; }
    }
}
