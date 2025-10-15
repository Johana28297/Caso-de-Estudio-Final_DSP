using System.Collections.Generic;
using System.Linq;

namespace BibliotecaMetropolis.Models
{
    public class BibliotecaService
    {
        private BibliotecaContext db = new BibliotecaContext();

        // CRUD Recurso
        public void AgregarRecurso(Recurso r) => db.Recursos.Add(r);
        public List<Recurso> ObtenerRecursos() => db.Recursos.ToList();
        public Recurso BuscarRecurso(int id) => db.Recursos.Find(id);
        public void EditarRecurso(Recurso r) => db.Entry(r).State = System.Data.Entity.EntityState.Modified;
        public void EliminarRecurso(int id)
        {
            var r = db.Recursos.Find(id);
            if (r != null)
            {
                db.Recursos.Remove(r);
                db.SaveChanges();
            }
        }

        // CRUD Autor
        public void AgregarAutor(Autor a) => db.Autores.Add(a);
        public List<Autor> ObtenerAutores() => db.Autores.ToList();

        // CRUD Etiqueta
        public void AgregarEtiqueta(Etiqueta e) => db.Etiquetas.Add(e);
        public List<Etiqueta> ObtenerEtiquetas() => db.Etiquetas.ToList();

        // CRUD Editorial
        public void AgregarEditorial(Editorial ed) => db.Editoriales.Add(ed);
        public List<Editorial> ObtenerEditoriales() => db.Editoriales.ToList();

        // CRUD País
        public void AgregarPais(Pais p) => db.Paises.Add(p);
        public List<Pais> ObtenerPaises() => db.Paises.ToList();

        // Guardar cambios
        public void GuardarCambios() => db.SaveChanges();
    }
}