using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaMetropolis.Models
{
    public class Etiqueta
    {
        [Key]
        public int IdEtiqueta { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public virtual ICollection<RecursoEtiqueta> RecursoEtiquetas { get; set; }
    }
}