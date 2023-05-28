using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Carrito
    {   

        public Carrito() { 
            ListaArticulo = new List<Articulo>();
            ArticulosCantidad = new Dictionary<int, int>();
            PrecioTotal = 0;
        }

        public decimal PrecioTotal { get; set; }

         public List<Articulo> ListaArticulo { get; set;}
         public Dictionary<int, int> ArticulosCantidad { get; set; }


        //AGREGAMOS ART A LA LISTA DE ART. DEL CARRITO. Si ya existe le sumamos en 1 su cantidad, si no establecemos en 1 la cantidad
        public void AgregarArticulo(Articulo articulo)
        {
            if (ArticulosCantidad.ContainsKey(articulo.Id))
            {
                ArticulosCantidad[articulo.Id]++;
            }
            else
            {
                ArticulosCantidad.Add(articulo.Id, 1);
                ListaArticulo.Add(articulo);
            }

            PrecioTotal += articulo.Precio;
        }
        //ELIMINAMOS ART DE LA LISTA DE ART. DEL CARRITO. Si ya existe y es mayor a 1 elimina 1 cantidad. Si existe y solo hay 1 eliminamos el articulo completo.
        public void RestarArticulo(Articulo articulo)
        {
            if (ArticulosCantidad.ContainsKey(articulo.Id))
            {
                if (ArticulosCantidad[articulo.Id] > 1)
                {
                    ArticulosCantidad[articulo.Id]--;
                }
                else
                {
                    ArticulosCantidad.Remove(articulo.Id);
                    ListaArticulo.Remove(articulo);
                }

                PrecioTotal -= articulo.Precio;
            }
        }
    }


}

 

    



