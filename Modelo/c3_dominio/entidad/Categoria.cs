using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelo.c5_transversal.excepcion;
namespace Modelo.c3_dominio.entidad
{
    public class Categoria
    {
        public Categoria()
        {
            this.codigocategoria = 0;
            this.listaSubCategoria = new List<SubCategoria>();
        }
        public int codigocategoria { get; set; }
        public string nombrecategoria { get; set; }
        public List<SubCategoria> listaSubCategoria { get; set; }

        public void agregarSubCategoria(SubCategoria subCategoria)
        {
            verificarExistenciaSubCategoria(subCategoria);
            listaSubCategoria.Add(subCategoria);
        }
        public void verificarExistenciaSubCategoria(SubCategoria subCategoria)
        {
            foreach (SubCategoria subCategoriaVerificar in listaSubCategoria)
            {
                if (subCategoriaVerificar.codigosubcategoria == subCategoria.codigosubcategoria)
                    throw ExcepcionReglaNegocio.crearErrorExistenciaSubCategoria();
            }
        }
        public void quitarSubCategoria(int codigosubcategoria)
        {
            foreach (SubCategoria subCategoriaVerificar in listaSubCategoria)
            {
                if (subCategoriaVerificar.codigosubcategoria == codigosubcategoria)
                {
                    listaSubCategoria.Remove(subCategoriaVerificar);
                    break;
                }
            }
        }
    }
}
