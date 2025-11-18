using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using C4_ENTIDAD;

namespace C3_DAL
{
    public class ClienteDAL
    {
        public Conexion conexion = new Conexion();

        public bool Desactivar(int idCliente)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "update Cliente set Activo=0 where Id=@idcliente";
                    comando.Parameters.AddWithValue("@idcliente", idCliente);
                    comando.Connection = conn;

                    conn.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    comando.Parameters.Clear();
                    conn.Close();
                    return filasAfectadas > 0;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Reactivar(int idCliente)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = System.Data.CommandType.Text;
                    comando.CommandText = "update Cliente set Activo=1 where Id=@idcliente";
                    comando.Parameters.AddWithValue("@idcliente", idCliente);
                    comando.Connection = conn;

                    conn.Open();
                    int filasAfectadas = comando.ExecuteNonQuery();
                    comando.Parameters.Clear();
                    conn.Close();
                    return filasAfectadas > 0;

                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public List<Domicilio> ObtenerDomicilio(int idDomicilio)
        {
            List<Domicilio> listDomicilio = new List<Domicilio>();

            using (SqlConnection conn = conexion.ObtenerConxeion())
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select * from Domicilio where IdDomicilio=@iddomicilio";
                comando.Parameters.AddWithValue("@iddomicilio", idDomicilio);
                comando.Connection = conn;


                conn.Open();
                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Domicilio aux = new Domicilio();

                    aux.IdDomicilio = lector.GetInt32(0);
                    aux.Numero = lector.GetInt32(1);
                    aux.Barrio = lector.GetString(2);
                    aux.Piso = lector.GetString(3);
                    aux.Ciudad = lector.GetString(4);
                    aux.Provincia = lector.GetString(5);

                    listDomicilio.Add(aux);
                }
                conn.Close();
            }
            return listDomicilio;
        }

        public int ConocerVenta(int idCliente)
        {
            using (SqlConnection conn = conexion.ObtenerConxeion())
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select count(*) from Venta where IdCliente=@idcliente";
                comando.Parameters.AddWithValue("@idcliente", idCliente);
                comando.Connection = conn;
                conn.Open();
                int cantidadVentas = (int)comando.ExecuteScalar();

                conn.Close();
                return cantidadVentas;
            }
        }

        public List<Cliente> Leer()
        {
            List<Cliente> listaCliente = new List<Cliente>();

            using (SqlConnection conn = conexion.ObtenerConxeion())
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select * from Cliente";
                comando.Connection = conn;
                conn.Open();

                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.IdCliente = lector.GetInt32(0);
                    aux.Nombre = lector.GetString(1);
                    aux.Apellido = lector.GetString(2);
                    aux.Telefono = lector.GetString(3);
                    aux.Email = lector.GetString(4);
                    aux.Activo = lector.GetBoolean(5);
                    aux.FechaIngreso = lector.GetDateTime(6);

                    listaCliente.Add(aux);
                }
                conn.Close();
            }

            return listaCliente;
        }

        public void Agregar(Cliente nuevo)
        {
            using (SqlConnection conn = conexion.ObtenerConxeion())
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "INSERT INTO Cliente (Nombre, Apellido, Telefono, Email, Activo, FechaIngreso) " +
                                      "VALUES (@nombre, @apellido, @telefono, @email, @activo, @fechaingreso)";

                comando.Parameters.AddWithValue("@nombre", nuevo.Nombre);
                comando.Parameters.AddWithValue("@apellido", nuevo.Apellido);
                comando.Parameters.AddWithValue("@telefono", nuevo.Telefono);
                comando.Parameters.AddWithValue("@email", nuevo.Email);
                comando.Parameters.AddWithValue("@activo", nuevo.Activo);
                comando.Parameters.AddWithValue("@fechaingreso", nuevo.FechaIngreso);

                comando.Connection = conn;
                conn.Open();
                comando.ExecuteNonQuery();

                comando.Parameters.Clear();
                conn.Close();
            }
        }

        public List<Cliente> Buscar(string busca)
        {
            List<Cliente> listaClientes = new List<Cliente>();

            using (SqlConnection conn = conexion.ObtenerConxeion())
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.Text;
                comando.CommandText = "select * from Cliente where Nombre like @busca or Apellido like @busca";
                comando.Parameters.AddWithValue("@busca", "%" + busca + "%");

                comando.Connection = conn;
                conn.Open();

                SqlDataReader lector = comando.ExecuteReader();
                while (lector.Read())
                {
                    Cliente aux = new Cliente();
                    aux.IdCliente = lector.GetInt32(0);
                    aux.Nombre = lector.GetString(1);
                    aux.Apellido = lector.GetString(2);
                    aux.Telefono = lector.GetString(3);
                    aux.Email = lector.GetString(4);
                    aux.Activo = lector.GetBoolean(5);
                    aux.FechaIngreso = lector.GetDateTime(6);

                    listaClientes.Add(aux);
                }
                conn.Close();
            }
            return listaClientes;
        }

        public void Modificar(Cliente modificado)
        {
            using (SqlConnection conn = conexion.ObtenerConxeion())
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.Text;

                comando.CommandText = "UPDATE Cliente SET Nombre=@nombre, Apellido=@apellido, Telefono=@telefono, Email=@email, Activo=@activo, FechaIngreso=@fechaingreso WHERE Id=@ID";

                comando.Parameters.AddWithValue("@ID", modificado.IdCliente);
                comando.Parameters.AddWithValue("@nombre", modificado.Nombre);
                comando.Parameters.AddWithValue("@apellido", modificado.Apellido);
                comando.Parameters.AddWithValue("@telefono", modificado.Telefono);
                comando.Parameters.AddWithValue("@email", modificado.Email);
                comando.Parameters.AddWithValue("@activo", modificado.Activo);
                comando.Parameters.AddWithValue("@fechaingreso", modificado.FechaIngreso);

                comando.Connection = conn;
                conn.Open();
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                conn.Close();
            }
        }

        public void Eliminar(int idCliente)
        {
            using (SqlConnection conn = conexion.ObtenerConxeion())
            {
                SqlCommand comando = new SqlCommand();
                comando.CommandType = System.Data.CommandType.Text;

                comando.CommandText = "DELETE FROM Cliente WHERE Id=@idCliente";
                comando.Parameters.AddWithValue("@idcliente", idCliente);
                comando.Connection = conn;
                conn.Open();
                comando.ExecuteNonQuery();
                comando.Parameters.Clear();
                conn.Close();
            }
        }
    }
}
