using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades;

namespace Modelo
{
    public class Contexto : DbContext
    {
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Jugador> Jugadores { get; set; }
        public DbSet<Partida> Partidas { get; set; }
        public DbSet<Rango> Rangos { get; set; }
        public DbSet<ResultadoPartida> ResultadoPartidas { get; set; }
        public DbSet<Torneo> TorneosCasuales { get; set; }
        public DbSet<TorneoProfesional> TorneosProfesionales { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"data source=(localdb)\MSSQLLocalDB;initial catalog=DBTorneoCSGo;integrated security = true;").EnableSensitiveDataLogging();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResultadoPartida>()
             .HasOne(r => r.Partida)
             .WithOne(p => p.Resultado)
             .HasForeignKey<ResultadoPartida>(r => r.PartidaId);

            modelBuilder.Entity<ResultadoPartida>().Property(r => r.Mapa).IsRequired(false);

            modelBuilder.Entity<Equipo>()
             .HasOne(e => e.Capitan)
             .WithMany()
             .HasForeignKey(e => e.CapitanId);
        }

    }
}
