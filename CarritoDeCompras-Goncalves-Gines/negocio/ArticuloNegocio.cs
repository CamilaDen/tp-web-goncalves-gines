using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using dominio;

namespace negocio
{
    public class ArticuloNegocio
    {
        // clase para el acceso a datos para la clase Articulo
        public List<Articulo> listar()
        {
            List<Articulo> lista = new List<Articulo>();
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            SqlDataReader lector;

            try
            {
                conexion.ConnectionString = "server=.\\SQLEXPRESS; database=CATALOGO_DB; integrated security=true";
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion Marca, A.IdCategoria ,C.Descripcion Categoria, A.ImagenUrl, A.Precio FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria";
                comando.Connection = conexion;

                conexion.Open();
                lector = comando.ExecuteReader();

                while (lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)lector["Id"];
                    aux.Codigo = (string)lector["Codigo"];
                    aux.Nombre = (string)lector["Nombre"];
                    aux.Descripcion = (string)lector["Descripcion"];
                    if (!(lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)lector["ImagenUrl"];
                    aux.Precio = (decimal)lector["Precio"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)lector["IdMarca"];
                    aux.Marca.Descripcion = (string)lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)lector["Categoria"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

        }

        public List<Articulo> listarConSP()
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearProcedimiento("storedListar");                
                datos.ejecutarLectura();
                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void Agregar(Articulo artic)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("INSERT INTO ARTICULOS (Codigo, Nombre, Descripcion, IdMarca, IdCategoria, ImagenUrl, Precio) VALUES (@Codigo, @Nombre, @Descripcion, @IdMarca, @IdCategoria, @ImagenUrl, @Precio)");
                datos.setearParametro("@Codigo", artic.Codigo);
                datos.setearParametro("@Nombre", artic.Nombre);
                datos.setearParametro("@Descripcion", artic.Descripcion);
                datos.setearParametro("@IdMarca", artic.Marca.Id);
                datos.setearParametro("@IdCategoria", artic.Categoria.Id);
                datos.setearParametro("@ImagenUrl", artic.ImagenUrl);
                datos.setearParametro("@Precio", artic.Precio);
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

        public void Modificar(Articulo artic)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("UPDATE ARTICULOS set Codigo = @Codigo, Nombre = @Nombre, Descripcion = @Descripcion, IdMarca = @IdMarca, IdCategoria = @IdCategoria, ImagenUrl = @ImagenUrl, Precio = @Precio WHERE ID = @ID");
                datos.setearParametro("@Codigo", artic.Codigo);
                datos.setearParametro("@Nombre", artic.Nombre);
                datos.setearParametro("@Descripcion", artic.Descripcion);
                datos.setearParametro("@IdMarca", artic.Marca.Id);
                datos.setearParametro("@IdCategoria", artic.Categoria.Id);
                datos.setearParametro("@ImagenUrl", artic.ImagenUrl);
                datos.setearParametro("@Precio", artic.Precio);
                datos.setearParametro("@ID", artic.Id);
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

        public void Eliminar(int id)
        {
            AccesoDatos datos = new AccesoDatos();
            try
            {
                datos.setearConsulta("DELETE FROM ARTICULOS WHERE ID = @Id");
                datos.setearParametro("@Id", id);
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

        public List<Articulo> filtrar(string campo, string criterio, string filtro)
        {
            List<Articulo> lista = new List<Articulo>();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion Marca, A.IdCategoria ,C.Descripcion Categoria, A.ImagenUrl, A.Precio FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria AND ";

                switch (campo)
                {
                    case "Código":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "A.Codigo like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "A.Codigo like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "A.Codigo like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Descripción":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "A.Descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "A.Descripcion like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "A.Descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Marca":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "M.Descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "M.Descripcion like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "M.Descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;

                    case "Categoría":
                        switch (criterio)
                        {
                            case "Comienza con":
                                consulta += "C.Descripcion like '" + filtro + "%'";
                                break;
                            case "Termina con":
                                consulta += "C.Descripcion like '%" + filtro + "'";
                                break;
                            default:
                                consulta += "C.Descripcion like '%" + filtro + "%'";
                                break;
                        }
                        break;
                    case "Precio":
                        switch (criterio)
                        {
                            case "Mayor a":
                                consulta += "A.Precio > " + filtro;
                                break;
                            case "Menor a":
                                consulta += "A.Precio < " + filtro;
                                break;
                            default:
                                consulta += "A.Precio = " + filtro;
                                break;
                        }
                        break;
                    default:
                        break;
                }

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();

                while (datos.Lector.Read())
                {
                    Articulo aux = new Articulo();
                    aux.Id = (int)datos.Lector["Id"];
                    aux.Codigo = (string)datos.Lector["Codigo"];
                    aux.Nombre = (string)datos.Lector["Nombre"];
                    aux.Descripcion = (string)datos.Lector["Descripcion"];
                    if (!(datos.Lector["ImagenUrl"] is DBNull))
                        aux.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                    aux.Precio = (decimal)datos.Lector["Precio"];
                    aux.Marca = new Marca();
                    aux.Marca.Id = (int)datos.Lector["IdMarca"];
                    aux.Marca.Descripcion = (string)datos.Lector["Marca"];
                    aux.Categoria = new Categoria();
                    aux.Categoria.Id = (int)datos.Lector["IdCategoria"];
                    aux.Categoria.Descripcion = (string)datos.Lector["Categoria"];
                    lista.Add(aux);
                }
                return lista;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
        public Articulo buscarPorId(int Id)
        {
            Articulo articulo = new Articulo();
            AccesoDatos datos = new AccesoDatos();
            try
            {
                string consulta = "SELECT A.Id, A.Codigo, A.Nombre, A.Descripcion, A.IdMarca, M.Descripcion Marca, A.IdCategoria ,C.Descripcion Categoria, A.ImagenUrl, A.Precio FROM ARTICULOS A INNER JOIN MARCAS M ON M.Id = A.IdMarca INNER JOIN CATEGORIAS C ON C.Id = A.IdCategoria AND A.Id=" + Id;

                datos.setearConsulta(consulta);
                datos.ejecutarLectura();
                datos.Lector.Read();
                articulo.Id = (int)datos.Lector["Id"];
                articulo.Codigo = (string)datos.Lector["Codigo"];
                articulo.Nombre = (string)datos.Lector["Nombre"];
                articulo.Descripcion = (string)datos.Lector["Descripcion"];
                if (!(datos.Lector["ImagenUrl"] is DBNull))
                    articulo.ImagenUrl = (string)datos.Lector["ImagenUrl"];
                articulo.Precio = (decimal)datos.Lector["Precio"];
                articulo.Marca = new Marca();
                articulo.Marca.Id = (int)datos.Lector["IdMarca"];
                articulo.Marca.Descripcion = (string)datos.Lector["Marca"];
                articulo.Categoria = new Categoria();
                articulo.Categoria.Id = (int)datos.Lector["IdCategoria"];
                articulo.Categoria.Descripcion = (string)datos.Lector["Categoria"];

                return articulo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
