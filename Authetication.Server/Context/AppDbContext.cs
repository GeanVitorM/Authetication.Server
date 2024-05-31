using Microsoft.EntityFrameworkCore;
using Authetication.Server.Models;

namespace Authetication.Server.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Login> Logins { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Coordenador> Coordenadores { get; set; }
        public DbSet<Fisioterapeuta> Fisioterapeutas { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Admin>()
                .HasOne(a => a.Login)
                .WithOne(l => l.Admin)
                .HasForeignKey<Admin>(a => a.IdAdmin);

            modelBuilder.Entity<Coordenador>()
                .HasOne(c => c.Login)
                .WithOne(l => l.Coordenador)
                .HasForeignKey<Coordenador>(c => c.IdCoordenador);

            modelBuilder.Entity<Fisioterapeuta>()
                .HasOne(f => f.Login)
                .WithOne(l => l.Fisioterapeuta)
                .HasForeignKey<Fisioterapeuta>(f => f.IdFisio);

            modelBuilder.Entity<Paciente>()
                .HasOne(p => p.Login)
                .WithOne(l => l.Paciente)
                .HasForeignKey<Paciente>(p => p.IdPaciente);
        }
    }
}
