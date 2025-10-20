
using Microsoft.EntityFrameworkCore;


namespace BibliotecaMetropolis.Models
{
    // Hereda de Microsoft.EntityFrameworkCore.DbContext
    public class BibliotecaContext : DbContext
    {
        // 2. Cambia el constructor para que acepte las opciones de configuración
        public BibliotecaContext(DbContextOptions<BibliotecaContext> options) : base(options)
        {
        }

        // 3. Tus DbSet están perfectos, no necesitan cambios.
        public DbSet<Pais> Paises { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Etiqueta> Etiquetas { get; set; }
        public DbSet<Recurso> Recursos { get; set; }
        public DbSet<AutoresRecursos> AutoresRecursos { get; set; }
        public DbSet<RecursoEtiqueta> RecursoEtiquetas { get; set; }
    }
}