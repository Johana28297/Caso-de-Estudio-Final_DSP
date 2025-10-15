using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaMetropolis.Models
{
    public class Recurso
    {
        [Key]
        public int IdRec { get; set; }

        [Required]
        public string Titulo { get; set; }

        public int AnnoPublic { get; set; }

        public int Edicion { get; set; }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public string PalabrasBusqueda { get; set; }

        [Required]
        [StringLength(20)]
        public string TipoRecurso { get; set; } // Validar en controlador: Libro, Enciclopedia, Revista, Tesis, DVD

        public string InstitucionEducativa { get; set; } // Solo para tesis

        public string Ciudad { get; set; }

        public DateTime FechaRegistro { get; set; }

        public int? IdEdit { get; set; }
        [ForeignKey("IdEdit")]
        public virtual Editorial Editorial { get; set; }

        public int IdPais { get; set; }
        [ForeignKey("IdPais")]
        public virtual Pais Pais { get; set; }

        public virtual ICollection<AutoresRecursos> AutoresRecursos { get; set; }

        public virtual ICollection<RecursoEtiqueta> RecursoEtiquetas { get; set; }
    }
}