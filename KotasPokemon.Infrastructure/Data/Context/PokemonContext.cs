using KotasPokemon.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KotasPokemon.Infrastructure.Data.Context
{
    public class PokemonContext : DbContext
    {
        public PokemonContext(DbContextOptions<PokemonContext> options) : base(options)
        { }
        public DbSet<PokemonMaster> PokemonMasters { get; set; }
        public DbSet<PokemonCaptured> PokemonsCaptured { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PokemonContext).Assembly);

            modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(DateTime)))
                .Distinct()
                .ToList();


            modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()).ToList().ForEach(relationship =>
            {
                relationship.DeleteBehavior = DeleteBehavior.NoAction;
            });

            base.OnModelCreating(modelBuilder);
        }
        public Task<int> CommitAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            return base.SaveChangesAsync(cancellationToken);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            ChangeTracker.Entries()
                .ToList().ForEach(entry =>
                {
                    if (entry.State == EntityState.Modified)
                    {
                        ((BaseEntity)entry.Entity).UpdateModified();
                    }
                });

            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
