using System.Data.Entity;

namespace BibliotecaMetropolis.Models
{
    public class BibliotecaContext : DbContext
    {
        public BibliotecaContext() : base("name=BibliotecaConnection") { }

        public DbSet<Pais> Paises { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }
        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<AutoresRecursos> AutoresRecursos { get; set; }
        public DbSet<RecursoEtiqueta> RecursoEtiquetas { get; set; }
    }
}