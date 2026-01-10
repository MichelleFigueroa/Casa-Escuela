using CasaEscuela.EN;
using CasaEscuela.EN.CatologosEN;
using CasaEscuela.EN.Seguridad;
using Microsoft.EntityFrameworkCore;

namespace CasaEscuela.DAL
{
    public partial class CasaEscuelaDBContext : DbContext
    {
        // Constructor estándar para EF Core
        public CasaEscuelaDBContext(DbContextOptions<CasaEscuelaDBContext> options)
            : base(options)
        {
        }

        // Seguridad
        public DbSet<UsuarioEN> Usuarios { get; set; }
        public DbSet<CorrelativoEN> Correlativos { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }

        // Catálogos
        public DbSet<CatNivelEscolarEN> CatNivelEscolar { get; set; }
        public DbSet<CatTipoFamiliaEN> CatTipoFamilia { get; set; }
        public DbSet<CatViveConEN> CatViveCon { get; set; }
        public DbSet<CatTipoPartoEN> CatTipoParto { get; set; }
        public DbSet<CatTipoPreceptoriaEN> CatTipoPreceptoria { get; set; }

        // Core
        public DbSet<EstudianteEN> Estudiantes { get; set; }
        public DbSet<EstudianteFamiliarEN> EstudianteFamiliares { get; set; }
        public DbSet<AnamnesisEN> Anamnesis { get; set; }
        public DbSet<EstudiantePreceptoriaEN> EstudiantePreceptorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuración UsuarioPermiso
            modelBuilder.Entity<UsuarioPermiso>()
                .HasKey(up => new { up.IdUsuario, up.IdPermiso });

            modelBuilder.Entity<UsuarioPermiso>()
                .HasOne(up => up.Usuario)
                .WithMany(u => u.UsuarioPermisos)
                .HasForeignKey(up => up.IdUsuario);

            modelBuilder.Entity<UsuarioPermiso>()
                .HasOne(up => up.Permiso)
                .WithMany(p => p.UsuarioPermisos)
                .HasForeignKey(up => up.IdPermiso);

            // Tablas Base
            modelBuilder.Entity<UsuarioEN>().ToTable("usuarios").HasKey(s => s.Id);
            modelBuilder.Entity<CorrelativoEN>().ToTable("correlativos").HasKey(s => s.Id);
            modelBuilder.Entity<CorrelativoEN>().Property(c => c.Id).ValueGeneratedOnAdd();

            // Relación Estudiante y Anamnesis (1 a 1)
            modelBuilder.Entity<EstudianteEN>().ToTable("estudiantes").HasKey(e => e.IdEstudiante);
            modelBuilder.Entity<AnamnesisEN>().ToTable("anamnesis").HasKey(a => a.IdAnamnesis);
            modelBuilder.Entity<AnamnesisEN>()
                .HasOne(a => a.Estudiante)
                .WithOne(e => e.Anamnesis)
                .HasForeignKey<AnamnesisEN>(a => a.IdEstudiante);

            // Relación Familiares (1 a Muchos)
            modelBuilder.Entity<EstudianteFamiliarEN>().ToTable("estudiantefamiliares").HasKey(ef => ef.IdFamiliar);
            modelBuilder.Entity<EstudianteFamiliarEN>()
                .HasOne(ef => ef.Estudiante)
                .WithMany(e => e.Familiares)
                .HasForeignKey(ef => ef.IdEstudiante);

            // Relación Preceptorias (1 a Muchos)
            modelBuilder.Entity<EstudiantePreceptoriaEN>().ToTable("estudiantepreceptorias").HasKey(ep => ep.Id);
            modelBuilder.Entity<EstudiantePreceptoriaEN>()
                .HasOne(ep => ep.Estudiante)
                .WithMany(e => e.Preceptorias)
                .HasForeignKey(ep => ep.IdEstudiante);
        }
    }
}