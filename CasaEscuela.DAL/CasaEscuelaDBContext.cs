using CasaEscuela.EN;
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


       


        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Correlativo> Correlativos { get; set; }
       
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<UsuarioPermiso> UsuarioPermisos { get; set; }


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
            modelBuilder.Entity<Usuario>().ToTable("usuarios");
            modelBuilder.Entity<Usuario>().HasKey(s => s.Id);
           

      

            modelBuilder.Entity<Correlativo>().ToTable("correlativos");
            modelBuilder.Entity<Correlativo>().HasKey(s => s.Id);
            modelBuilder.Entity<Correlativo>()
                .Property(c => c.Id)
                .ValueGeneratedOnAdd();
          

           



            



          
           
        }
    }
}