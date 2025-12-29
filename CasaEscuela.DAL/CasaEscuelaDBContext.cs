using CasaEscuela.EN;
using CasaEscuela.EN.CatologosEN;
using CasaEscuela.EN.Seguridad;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Reflection.Emit;
using Permiso = CasaEscuela.EN.Seguridad.Permiso;

namespace CasaEscuela.DAL
{
    public partial class CasaEscuelaDBContext : DbContext
    {

        public CasaEscuelaDBContext(DbContextOptions<CasaEscuelaDBContext> options)
            : base(options)
        {

        }


       


        public DbSet<UsuarioEN> Usuarios { get; set; }
        public DbSet<CorrelativoEN> Correlativos { get; set; }
       
        public DbSet<Permiso> Permisos { get; set; }

        public DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }

        // ==========================
        // CATÁLOGOS
        // ==========================

        public DbSet<CatNivelEscolarEN> CatNivelEscolar { get; set; }
        public DbSet<CatTipoFamiliaEN> CatTipoFamilia { get; set; }
        public DbSet<CatViveConEN> CatViveCon { get; set; }
        public DbSet<CatTipoPartoEN> CatTipoParto { get; set; }
        public DbSet<CatTipoPreceptoriaEN> CatTipoPreceptoria { get; set; }

        // ==========================
        // CORE
        // ==========================

        public DbSet<EstudianteEN> Estudiantes { get; set; }
        public DbSet<EstudianteFamiliarEN> EstudianteFamiliares { get; set; }
        public DbSet<AnamnesisEN> Anamnesis { get; set; }
        public DbSet<EstudiantePreceptoriaEN> EstudiantePreceptorias { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Write Fluent API configurations here           
            //Property Configurations

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

            // Rol como rol no tiene el id usuario significa que el usuario puede usar el mismo id del mismo 
            // registro varias entonces rol con usuario es de uno a muchos
            // Usuario en usuario esta el RolId eso significa que cada registro de un usuario
            // puede referencia a un rol entonce la relacion de usuario y rol de uno a uno
            modelBuilder.Entity<UsuarioEN>().ToTable("usuarios");
            modelBuilder.Entity<UsuarioEN>().HasKey(s => s.Id);
           

      

            modelBuilder.Entity<CorrelativoEN>().ToTable("correlativos");
            modelBuilder.Entity<CorrelativoEN>().HasKey(s => s.Id);
            modelBuilder.Entity<CorrelativoEN>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
          
            // Configuración Estudiante y relacionados
            modelBuilder.Entity<EstudianteEN>().ToTable("estudiantes");
            modelBuilder.Entity<EstudianteEN>().HasKey(e => e.IdEstudiante);

            modelBuilder.Entity<AnamnesisEN>().ToTable("anamnesis");
            modelBuilder.Entity<AnamnesisEN>().HasKey(a => a.IdAnamnesis);
            modelBuilder.Entity<AnamnesisEN>()
                .HasOne(a => a.Estudiante)
                .WithOne(e => e.Anamnesis)
                .HasForeignKey<AnamnesisEN>(a => a.IdEstudiante);

            modelBuilder.Entity<EstudianteFamiliarEN>().ToTable("estudiante_familiares");
            modelBuilder.Entity<EstudianteFamiliarEN>().HasKey(ef => ef.IdFamiliar);
            modelBuilder.Entity<EstudianteFamiliarEN>()
                .HasOne(ef => ef.Estudiante)
                .WithMany(e => e.Familiares)
                .HasForeignKey(ef => ef.IdEstudiante);

            modelBuilder.Entity<EstudiantePreceptoriaEN>().ToTable("estudiante_preceptorias");
            modelBuilder.Entity<EstudiantePreceptoriaEN>().HasKey(ep => ep.Id);
            modelBuilder.Entity<EstudiantePreceptoriaEN>()
                .HasOne(ep => ep.Estudiante)
                .WithMany(e => e.Preceptorias)
                .HasForeignKey(ep => ep.IdEstudiante);
          

           



            



          
           
        }
    }
}