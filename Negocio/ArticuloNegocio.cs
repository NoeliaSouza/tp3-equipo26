using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dominio;
using System.Xml.Linq;
using System.Net;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using System.Security.Cryptography;


namespace Negocio
{
    public class ArticuloNegocio
    {
        //LISTAR ARTICULOS
        public List<Articulo> listar()
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                datos.setearConsulta("SELECT a.Id, Codigo, Nombre, a.Descripcion as DescripcionArticulo, Precio,m.Id as IdMarca, m.Descripcion as NombreMarca,c.Id as IdCategoria, c.Descripcion as NombreCategoria, i.ImagenUrl as imagen from ARTICULOS a inner join MARCAS m on a.IdMarca=m.Id left join CATEGORIAS c on a.IdCategoria=c.Id left join IMAGENES i on a.Id=i.IdArticulo ");
                datos.ejecutarConsulta();
                List<Articulo> lista = new List<Articulo>();
                
                

                while (datos.Lector.Read())
                {
                    //Validaciones BD
                    int id = (int)datos.Lector["Id"];
                    string codigoArt = datos.Lector["Codigo"] == DBNull.Value ? "Sin codigo" : (string)datos.Lector["Codigo"];
                    string descripcion = datos.Lector["DescripcionArticulo"] == DBNull.Value ? "Sin descripcion" : (string)datos.Lector["DescripcionArticulo"];
                    decimal precio = datos.Lector["Precio"] == DBNull.Value ? 0 : (decimal)datos.Lector["Precio"];
                    string nombre = datos.Lector["Nombre"] == DBNull.Value ? "Sin nombre" : (string)datos.Lector["Nombre"];
                    string urlImagen = datos.Lector["imagen"] == DBNull.Value ? "https://t3.ftcdn.net/jpg/02/48/42/64/240_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg" : (string)datos.Lector["imagen"];
                    int idCategorias = datos.Lector["IdCategoria"] == DBNull.Value ? -1 : (int)datos.Lector["IdCategoria"];
                    string categorias = datos.Lector["NombreCategoria"] == DBNull.Value ? "Sin categoria" : (string)datos.Lector["NombreCategoria"];
                    int idMarcas = datos.Lector["IdMarca"] == DBNull.Value ? -1 : (int)datos.Lector["IdMarca"];
                    string marcas = datos.Lector["NombreMarca"] == DBNull.Value ? "Sin marca" : (string)datos.Lector["NombreMarca"];

                     precio = Math.Round(precio, 2);
                    //Verificamos si el articulo existe
                    Articulo articulo = lista.FirstOrDefault(a => a.Id == id);
                    if (articulo == null)
                    {
                        // Si no existe, creamos un nuevo artículo y lo agregamos a la lista
                        
                        articulo = new Articulo

                        {
                            Id = id,
                            CodigoArticulo = codigoArt,
                            Nombre = nombre,
                            Descripcion = descripcion,
                            Precio = precio,

                            Categorias = new Categoria
                            {
                                Id = idCategorias,
                                NombreCategoria = categorias
                            },
                            Marcas = new Marca
                            {
                                Id = idMarcas,
                                NombreMarca = marcas
                            },
                            imagenes = new List<string>() // Inicializamos la lista de imagenes del artículo
                        };
                        lista.Add(articulo);
                    }
                    //Si existe, agregamos la URL de la imagen a la lista de imagenes del artículo
                    articulo.imagenes.Add(urlImagen);


                }
                //cerramos conexion
                //datos.cerrarConexion();
                //retornamos lista
                return
                    lista;
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

        //LISTAR POR FILTROS
        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT a.Id, Codigo, Nombre, a.Descripcion as DescripcionArticulo, \r\n Precio, m.Descripcion as NombreMarca, c.Descripcion as NombreCategoria, \r\n i.ImagenUrl as imagen from ARTICULOS a \r\n inner join MARCAS m on a.IdMarca=m.Id\r\n left join CATEGORIAS c on a.IdCategoria=c.Id\r\n left join IMAGENES i on a.Id=i.IdArticulo where ";
                if (campo == "Precio")
                {
                    consulta=sumarFiltrosAConsulta("Precio", criterio, filtro, consulta);
                }
                else if (campo == "Codigo Articulo")
                {
                    consulta=sumarFiltrosAConsulta("Codigo", criterio, filtro, consulta);
                }
                else if (campo == "Nombre")
                {
                    consulta = sumarFiltrosAConsulta("Nombre", criterio, filtro, consulta);
                }
                else if (campo == "Descripcion")
                {
                    consulta = sumarFiltrosAConsulta("a.Descripcion", criterio, filtro, consulta);
                }
                else if (campo == "Marcas")
                {
                    consulta = sumarFiltrosAConsulta("m.Descripcion", criterio, filtro, consulta);
                }
                else if (campo == "Categorias")
                { 
                    consulta = sumarFiltrosAConsulta("c.Descripcion", criterio, filtro, consulta);
                }
                //TODO IF OTROS CAMPOS....

                

                datos.setearConsulta(consulta);
                datos.ejecutarConsulta();

                while (datos.Lector.Read())
                {
                    //Validaciones BD
                    int id = (int)datos.Lector["Id"];
                    string codigoArt = datos.Lector["Codigo"] == DBNull.Value ? "Sin codigo" : (string)datos.Lector["Codigo"];
                    string descripcion = datos.Lector["DescripcionArticulo"] == DBNull.Value ? "Sin descripcion" : (string)datos.Lector["DescripcionArticulo"];
                    decimal precio = datos.Lector["Precio"] == DBNull.Value ? 0 : (decimal)datos.Lector["Precio"];
                    string nombre = datos.Lector["Nombre"] == DBNull.Value ? "Sin nombre" : (string)datos.Lector["Nombre"];
                    string urlImagen = datos.Lector["imagen"] == DBNull.Value ? "https://t3.ftcdn.net/jpg/02/48/42/64/240_F_248426448_NVKLywWqArG2ADUxDq6QprtIzsF82dMF.jpg" : (string)datos.Lector["imagen"];
                    string categorias = datos.Lector["NombreCategoria"] == DBNull.Value ? "Sin categoria" : (string)datos.Lector["NombreCategoria"];
                    string marcas = datos.Lector["NombreMarca"] == DBNull.Value ? "Sin marca" : (string)datos.Lector["NombreMarca"];


                    //Verificamos si el articulo existe
                    Articulo articulo = lista.FirstOrDefault(a => a.Id == id);
                    if (articulo == null)
                    {
                        // Si no existe, creamos un nuevo artículo y lo agregamos a la lista

                        articulo = new Articulo

                        {
                            Id = id,
                            CodigoArticulo = codigoArt,
                            Nombre = nombre,
                            Descripcion = descripcion,
                            Precio = precio,
                            Categorias = new Categoria { NombreCategoria = categorias },
                            Marcas = new Marca { NombreMarca = marcas },
                            imagenes = new List<string>() // Inicializamos la lista de imagenes del artículo
                        };
                        lista.Add(articulo);
                    }
                    //Si existe, agregamos la URL de la imagen a la lista de imagenes del artículo
                    articulo.imagenes.Add(urlImagen);


                }


                return lista;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public string sumarFiltrosAConsulta(string propiedad, string criterio, string filtro, string consulta)
        {

            string nombrePropiedad = ""; // Variable para almacenar el nombre de la propiedad
            switch (propiedad)
            {
                case "Precio":
                    nombrePropiedad = "Precio";
                    break;
                case "Codigo":
                    nombrePropiedad = "Codigo";
                    break;
                case "Nombre":
                    nombrePropiedad = "Nombre";
                    break;
                case "a.Descripcion":
                    nombrePropiedad = "a.Descripcion";
                    break;
                case "m.Descripcion":
                    nombrePropiedad = "m.Descripcion";
                    break;
                case "c.Descripcion":
                    nombrePropiedad = "c.Descripcion";
                    break;


                default:
                    throw new Exception("Propiedad no válida");
            }

            if (string.Equals(propiedad, "Precio", StringComparison.OrdinalIgnoreCase))
            {
                
                               
                switch (criterio)
                {

                      case "Mayor a":
                      consulta += propiedad + ">" + filtro;
                          break;
                      case "Menor a":
                      consulta += propiedad + "<" + filtro;
                          break;
                      default:
                      consulta += propiedad + "=" + filtro;
                          break;
                }
            }
            else if(string.Equals(propiedad, "m.Descripcion", StringComparison.OrdinalIgnoreCase)){
                consulta += "m.Descripcion ='"  +  filtro + "'";
                
            }

            else if(string.Equals(propiedad, "c.Descripcion", StringComparison.OrdinalIgnoreCase))
            {
                consulta += "c.Descripcion ='" + filtro + "'";
            }
            else
            {
                switch (criterio)
                {


                    case "Comienza con":
                        consulta += $"{nombrePropiedad} LIKE '{filtro}%'";
                        break;
                    case "Termina con":
                        consulta += $"{nombrePropiedad} LIKE '%{filtro}'";
                        break;
                    default:
                        consulta += $"{nombrePropiedad} LIKE '%{filtro}%'";
                        break;


                }

            }



            return consulta;
        }


        //AGREGAR
        public void Agregar(Articulo articulo) { 
        
            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("INSERT into Articulos(Codigo, Nombre, Descripcion, IdMarca, IdCategoria,Precio) values(@Codigo, @Nombre, @Descripcion,@IdMarca,@IdCategoria,@Precio)");

                datos.setearParametros("@Codigo", articulo.CodigoArticulo);
                datos.setearParametros("@Nombre", articulo.Nombre);
                datos.setearParametros("@Descripcion", articulo.Descripcion);
                datos.setearParametros("@IdMarca", articulo.Marcas.Id);
                datos.setearParametros("@IdCategoria", articulo.Categorias.Id);
                datos.setearParametros("@Precio", articulo.Precio);

                datos.ejecutarAccion();
                           
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally {
                datos.cerrarConexion();
              
            }
                              
        }


        

        public void Modificar(Articulo articulo)
        {

            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("update ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, Precio = @Precio, IdMarca = @IdMarca, IdCategoria = @IdCategoria  where Id = @Id");

                datos.setearParametros("@Codigo", articulo.CodigoArticulo);
                datos.setearParametros("@Nombre", articulo.Nombre);
                datos.setearParametros("@Descripcion", articulo.Descripcion);
                datos.setearParametros("@IdMarca", articulo.Marcas.Id);
                datos.setearParametros("@IdCategoria", articulo.Categorias.Id);
                datos.setearParametros("@Precio", articulo.Precio);
                datos.setearParametros("@Id", articulo.Id);

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
            try
            {
                AccesoDatos datos = new AccesoDatos();
                datos.setearConsulta("delete from ARTICULOS where Id = @Id");
                datos.setearParametros("@Id", id);

                datos.ejecutarAccion();

                datos.setearConsulta("delete from IMAGENES where IdArticulo = @IdArticulo");
                datos.setearParametros("@IdArticulo", id);


                datos.ejecutarAccion();


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        

        public int BuscarUltimoId() {

            AccesoDatos datos = new AccesoDatos();
             

          try
            {
                List<Articulo> lista2 = new List<Articulo>();

                lista2 = this.listar();

                int ultimoId = 0;
                
                foreach (var item in lista2)
                {
                    ultimoId = item.Id;
                }
                
                


                return ultimoId;
                

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
        
        
        public void AgregarImagenes(Articulo articulo) {

            int idArticulo= BuscarUltimoId();

           

            AccesoDatos datos   =  new AccesoDatos();

            try
            { 
                
                foreach (var item in articulo.imagenes)
                {
                    if (item != "placeHolder.jpeg")
                    {
                        datos.setearConsulta("insert into imagenes(idArticulo, ImagenUrl) values (" + idArticulo + ", '" + item.ToString() + "' )");
                        datos.ejecutarAccion();
                    }
                }

               
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally { 
                datos.cerrarConexion();
            }
        
        
        }

        public void AgregarOtraImagen(Articulo articulo)
        {
            AccesoDatos datos = new AccesoDatos();

            try
            {
                foreach (var item in articulo.imagenes)
                {
                    datos.setearConsulta("INSERT into IMAGENES (IdArticulo,ImagenUrl) values(@IdArticulo, @url)");

                    datos.setearParametros("@IdArticulo", articulo.Id);
                    datos.setearParametros("@url", item.ToString());
                    


                    datos.ejecutarAccion();
                }
                    

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



        public void modificarImagen(Articulo articulo,List<string> viejasImagenes) {

            AccesoDatos datos = new AccesoDatos();
            int posicion = 0;

            try
            {
                foreach (var item in articulo.imagenes)
                {

                    if (viejasImagenes.Count > posicion)
                    {
                       
                        datos.setearConsulta("Update imagenes set ImagenUrl = '" + item.ToString() + "' where ImagenUrl = @imagenVieja and idArticulo = @id");

                        datos.setearParametros("@imagenVieja", viejasImagenes[posicion].ToString());
                        datos.setearParametros("@id", articulo.Id);
                        datos.ejecutarAccion();
                        posicion++;

                    }
                    

                    
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally {
                datos.cerrarConexion();
            }

        }

        public void eliminarImagenes(Articulo articulo,int posicion) {

            AccesoDatos datos = new AccesoDatos();

            try
            {

                datos.setearConsulta("Delete from Imagenes where idArticulo = @id and ImagenUrl = @urlImagen");
                datos.setearParametros("@id", articulo.Id);
                datos.setearParametros("@urlImagen", articulo.imagenes[posicion].ToString());

                datos.ejecutarAccion();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            finally {

                datos.cerrarConexion();

            
            }

        
        
        }



        public bool compararImagenes(Articulo articulo)
        {
            List<string> listImag = new List<string>();
            AccesoDatos datos = new AccesoDatos();

            
                try
                {
                    datos.setearConsulta("select imagenUrl from IMAGENES where idArticulo=@id");
                    datos.setearParametros("@id", articulo.Id);
                    datos.ejecutarConsulta();

                    while (datos.Lector.Read()) { 

                        listImag.Add((string)datos.Lector["imagenUrl"]);

                    }
                }
                catch (Exception ex)
                {

                    throw ex;
                }

            int posicion = 0;
            foreach (var item in listImag)
            {
                if (item.ToString() == articulo.imagenes[posicion])
                {
                    return true;
                }
                if (articulo.imagenes.Count > posicion)
                {
                    posicion++;

                }
            }
            




            return false;
        }

              

    }
}
