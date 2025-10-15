using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BibliotecaMetropolis.Models
{
    public class Autor
    {
        [Key]
        public int IdAutor { get; set; }

        [Required]
        public string Nombres { get; set; }

        [Required]
        public string Apellidos { get; set; }

        public virtual ICollection<AutoresRecursos> AutoresRecursos { get; set; }
    }
}