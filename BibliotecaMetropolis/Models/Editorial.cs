using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaMetropolis.Models
{
    public class Editorial
    {
        [Key]
        public int IdEdit { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public virtual ICollection<Recurso> Recursos { get; set; }
    }
}