using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo
    {

        public int Id { get; set; }
        public string CodigoArticulo  { get; set; }

        public string Nombre { get; set; }

        public string Descripcion { get; set; }

        public decimal Precio { get; set; }
        public Marca Marcas { get; set; }

        public Categoria Categorias { get; set; }
        //Lista de imagenes por si hay mas de 1
        public List<string> imagenes { get; set; }
        public Articulo()
        {
            imagenes = new List<string>();
        }
        

    }
}
