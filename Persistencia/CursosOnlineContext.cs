
using Microsoft.EntityFrameworkCore;
using Dominio;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Persistencia
{
    public class CursosOnlineContext:IdentityDbContext<Usuario>
    {
        //Permitira la migracion de entidades / puentes de Inyeccion de dependencias
        public CursosOnlineContext(DbContextOptions options):base(options){
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){
            //Para crear la migración a las tablas
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<CursoInstructor>().HasKey(ci=>new{ci.InstructorId,ci.CursoId});
        }

        //Mapeo de las clases de la BD
        public DbSet<Comentario> Comentario {get;set;}
        public DbSet<Curso> Curso {get;set;}
        public DbSet<CursoInstructor> CursoInstructor {get;set;}
        public DbSet<Instructor> Instructor {get;set;}
        public DbSet<Precio> Precio {get;set;}
    }
}