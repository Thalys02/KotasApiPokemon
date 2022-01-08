using System;

namespace KotasPokemon.Domain.Models
{
    public class BaseEntity
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? ModifiedAt { get; set; }

        public void UpdateModified()
        {
            this.ModifiedAt = DateTime.Now;
        }
    }
}
