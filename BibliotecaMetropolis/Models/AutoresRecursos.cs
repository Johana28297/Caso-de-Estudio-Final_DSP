using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMetropolis.Models
{
    public class AutoresRecursos
    {
        [Key]
        public int Id { get; set; }

        public int IdRec { get; set; }
        [ForeignKey("IdRec")]
        public virtual Recurso Recurso { get; set; }

        public int IdAutor { get; set; }
        [ForeignKey("IdAutor")]
        public virtual Autor Autor { get; set; }

        [Required]
        public bool EsPrincipal { get; set; }
    }
}