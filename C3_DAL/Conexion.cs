using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using C4_ENTIDAD;

namespace C3_DAL
{
    public class Conexion
    {
        SqlConnection conexion = new SqlConnection();
        SqlCommand comando = new SqlCommand();

        public List<Producto> Leer()
        {
            List<Producto> listaProductos = new List<Producto>();

            SqlDataReader lector;

            conexion.ConnectionString = "Data Source=DESKTOP-O3HRH7N\\SQLDEV; initial catalog=anaserena_pms_db; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "SELECT * FROM Producto";
            comando.Connection = conexion;
            conexion.Open();

            lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Producto aux = new Producto();
                aux.Id = lector.GetInt32(0);
                aux.Codigo = lector.GetString(1);
                aux.Nombre = lector.GetString(2);
                aux.Descripcion = lector.GetString(3);
                aux.Precio = lector.GetDecimal(4);
                aux.Categoria = lector.GetString(5);
                aux.Disponible = lector.GetBoolean(6);
                aux.EnStock = lector.GetBoolean(7);
                aux.FechaCreacion = lector.GetDateTime(8);

                listaProductos.Add(aux);
            }

            conexion.Close();
            return listaProductos;
        }
        public void Agregar(Producto nuevo)
        {
            conexion.ConnectionString = "Data Source=DESKTOP-O3HRH7N\\SQLDEV; initial catalog=anaserena_pms_db; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;

            comando.CommandText = "INSER INTO Producto values (@codigo, @nombre, @descripcion, @precio, @categoria, @disponible, @enStock, @fechaCreacion)";

            comando.Parameters.AddWithValue("@codigo", nuevo.Codigo.ToString());
            comando.Parameters.AddWithValue("@nombre", nuevo.Nombre.ToString());
            comando.Parameters.AddWithValue("@descripcion", nuevo.Descripcion.ToString());
            comando.Parameters.AddWithValue("@precio", nuevo.Precio);
            comando.Parameters.AddWithValue("@categoria", nuevo.Categoria.ToString());
            comando.Parameters.AddWithValue("@disponible", nuevo.Disponible);
            comando.Parameters.AddWithValue("@enStock", nuevo.EnStock);
            comando.Parameters.AddWithValue("@fechaCreacion", nuevo.FechaCreacion);

            comando.Connection = conexion;

            conexion.Open();
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
            conexion.Close();
        }
        public List<Producto> Buscar(string busca)
        {
            List<Producto> listProducto = new List<Producto>();

            conexion.ConnectionString = "Data Source=DESKTOP-O3HRH7N\\SQLDEV; initial catalog=anaserena_pms_db; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;

            comando.CommandText = "SELECT * FROM Producto WHERE Nombre LIKE'" + busca + "'";
            comando.Connection = conexion;
            conexion.Open();

            SqlDataReader lector = comando.ExecuteReader();
            while (lector.Read())
            {
                Producto aux = new Producto();
                aux.Id = lector.GetInt32(0);
                aux.Codigo = lector.GetString(1);
                aux.Nombre = lector.GetString(2);
                aux.Descripcion = lector.GetString(3);
                aux.Precio = lector.GetDecimal(4);
                aux.Categoria = lector.GetString(5);
                aux.Disponible = lector.GetBoolean(6);
                aux.EnStock = lector.GetBoolean(7);
                aux.FechaCreacion = lector.GetDateTime(8);

                listProducto.Add(aux);
            }

            conexion.Close();
            return listProducto;
        }
        public void Modificar(Producto modificado)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();
            conexion.ConnectionString = "Data Source=DESKTOP-O3HRH7N\\SQLDEV; initial catalog=anaserena_pms_db; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;

            comando.CommandText = "UPDATE Producto set Codigo=@codigo,Nombre=@nombre,Descripcion=@descripcion,Precio=@precio,Categoria=@categoria,Disponible=@disponible,EnStock=@enStock,FechaCreacion=@fechaCreacion WHERE Id=" + modificado.Id;
            comando.Parameters.AddWithValue("@codigo", modificado.Codigo.ToString());
            comando.Parameters.AddWithValue("@nombre", modificado.Nombre.ToString());
            comando.Parameters.AddWithValue("@descripcion", modificado.Descripcion.ToString());
            comando.Parameters.AddWithValue("@precio", modificado.Precio);
            comando.Parameters.AddWithValue("@categoria", modificado.Categoria.ToString());
            comando.Parameters.AddWithValue("@disponible", modificado.Disponible);
            comando.Parameters.AddWithValue("@enStock", modificado.EnStock);
            comando.Parameters.AddWithValue("@fechaCreacion", modificado.FechaCreacion);

            comando.Connection = conexion;

            conexion.Open();
            comando.ExecuteNonQuery();

            comando.Parameters.Clear();
            conexion.Close();
        }
        public void Eliminar(Producto borrar)
        {
            SqlConnection conexion = new SqlConnection();
            SqlCommand comando = new SqlCommand();

            conexion.ConnectionString = "Data Source=DESKTOP-O3HRH7N\\SQLDEV; initial catalog=anaserena_pms_db; integrated security=sspi";
            comando.CommandType = System.Data.CommandType.Text;
            comando.CommandText = "DELETE FROM Producto WHERE Id=" + borrar.Id;
            comando.Connection = conexion;

            conexion.Open();
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        // Verificación de integridad 
        public bool VerificarProductosEnVentas(int idProducto)
        {
            using (SqlConnection conexion = new SqlConnection(@"Data Source=DESKTOP-O3HRH7N\\SQLDEV; initial catalog=anaserena_pms_db; integrated security=sspi"))
            {
                conexion.Open();
                string query = "SELECT COUNT(*) FROM Ventas WHERE ProductoId = @Id";
                using (SqlCommand cmd = new SqlCommand(query, conexion))
                {
                    cmd.Parameters.AddWithValue("@Id", idProducto);
                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // true si hay ventas asociadas
                }
            }
        }
        public Producto BuscarPorNombre(string nombre)
        {
            var productos = Leer(); // trae todos los productos
            return productos.FirstOrDefault(p => p.Nombre.Equals(nombre, StringComparison.OrdinalIgnoreCase));
        }
        public List<Producto> ObtenerTodos()
        {
            // Reutiliza el método Leer()
            return Leer();
        }
    }
}
