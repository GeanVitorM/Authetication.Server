using Microsoft.EntityFrameworkCore;
using Authetication.Server.Api.Models;
using Authetication.Server.Api.DTOs;

namespace Authetication.Server.Api.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Admin> Admins { get; set; }
    public DbSet<Coordenador> Coordenadores { get; set; }
    public DbSet<Fisioterapeuta> Fisioterapeutas { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Usuario>()
            .HasIndex(u => u.Username)
            .IsUnique();

        modelBuilder.Entity<Admin>()
            .HasOne(a => a.Usuario)
            .WithOne(l => l.Admin)
            .HasForeignKey<Admin>(a => a.IdAdmin);

        modelBuilder.Entity<Coordenador>()
            .HasOne(c => c.Usuario)
            .WithOne(l => l.Coordenador)
            .HasForeignKey<Coordenador>(c => c.IdCoordenador);

        modelBuilder.Entity<Fisioterapeuta>()
            .HasOne(f => f.Usuario)
            .WithOne(l => l.Fisioterapeuta)
            .HasForeignKey<Fisioterapeuta>(f => f.IdFisio);

        modelBuilder.Entity<Paciente>()
            .HasOne(p => p.Usuario)
            .WithOne(l => l.Paciente)
            .HasForeignKey<Paciente>(p => p.IdUser);
    }
}
