using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;

namespace Negocio
{
    public class MarcaNegocio
    {

        public List<Marca> listar() {

            List<Marca> lista = new List<Marca>();
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("select id, descripcion from MARCAS");
                datos.ejecutarConsulta();

                while (datos.Lector.Read())
                {

                    Marca aux = new Marca();

                    aux.Id = (int)datos.Lector["id"];
                    aux.NombreMarca = (string)datos.Lector["descripcion"];

                    lista.Add(aux);

                }




                return lista;
            }
            catch (Exception)
            {

                throw;
            }

            finally { 
            datos.cerrarConexion();
            }

           
        
        }

        public void agregar(Marca marca)
        {
            AccesoDatos datos = new AccesoDatos();


            try
            {
                datos.setearConsulta("Insert into Marcas(Descripcion) values (@Descripcion)");
                datos.setearParametros("@Descripcion", marca.NombreMarca);

                datos.ejecutarAccion();


            }
            catch (Exception)
            {


                throw;
            }
            finally {

                datos.cerrarConexion();
            }


        }


        public void modificar(int id, string descripcion)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearConsulta("UPDATE marcas SET descripcion = @descripcion WHERE id= @id");
                datos.setearParametros("@Id", id);
                datos.setearParametros("@descripcion", descripcion);

                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }
        }

        public void eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                
                datos.setearConsulta("delete from MARCAS where Id = @Id");
                datos.setearParametros("@Id", id);

                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally
            {
                datos.cerrarConexion();
            }

        }


    }
}
