using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaMetropolis.Models
{
    public class Pais
    {
        [Key]
        public int IdPais { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        public virtual ICollection<Recurso> Recursos { get; set; }
    }
}