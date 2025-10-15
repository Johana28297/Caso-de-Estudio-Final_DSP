using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMetropolis.Models
{
    public class RecursoEtiqueta
    {
        [Key]
        public int Id { get; set; }

        public int IdRec { get; set; }
        [ForeignKey("IdRec")]
        public virtual Recurso Recurso { get; set; }

        public int IdEtiqueta { get; set; }
        [ForeignKey("IdEtiqueta")]
        public virtual Etiqueta Etiqueta { get; set; }
    }
}