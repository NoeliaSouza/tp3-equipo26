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
        ListaArticulo = new List<Articulo> { };
        
        }

        public decimal PrecioTotal { get; set; }

         public List<Articulo> ListaArticulo { get; set;}

         

        public void RestarATotal(int id) {
            Articulo articulo = new Articulo();
          articulo =  ListaArticulo.Find(x=> x.Id == id);

            this.PrecioTotal = this.PrecioTotal - articulo.Precio;
          }

        public void SumarATotal(int id) {

            Articulo articulo = new Articulo();
            articulo = ListaArticulo.Find(x => x.Id == id);

            this.PrecioTotal = this.PrecioTotal + articulo.Precio;
        }


    }



    }

    



