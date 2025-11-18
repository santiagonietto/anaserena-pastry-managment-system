using C4_ENTIDAD;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace C3_DAL
{
    public class UsuarioDAL
    {
        private Conexion conexion = new Conexion();

        public bool Agregar(Usuarios usuario)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"INSERT INTO Usuario 
                                       (USERNAME, NOMBRECOMPLETO, PASSWORD, ROL, ESTADO, INTENTOFALLIDOS, FECHAINGRESO, IDDOMICILIO)
                                       VALUES 
                                       (@username, @nombreCompleto, @password, @rol, @estado, @intentosFallidos, @fechaIngreso, @idDomicilio)";

                    cmd.Parameters.AddWithValue("@username", usuario.Username);
                    cmd.Parameters.AddWithValue("@nombreCompleto", usuario.NombreCompleto);
                    cmd.Parameters.AddWithValue("@password", usuario.Password);
                    cmd.Parameters.AddWithValue("@rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("@estado", usuario.Estado);
                    cmd.Parameters.AddWithValue("@intentosFallidos", usuario.IntentosFallidos);
                    cmd.Parameters.AddWithValue("@fechaIngreso", usuario.FechaIngreso);
                    cmd.Parameters.AddWithValue("@idDomicilio", usuario.IdDomicilio > 0 ? (object)usuario.IdDomicilio : DBNull.Value);

                    cmd.Connection = conn;
                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al agregar usuario: " + ex.Message);
            }
        }

        public List<Usuarios> Leer()
        {
            List<Usuarios> lista = new List<Usuarios>();

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"SELECT ID, USERNAME, NOMBRECOMPLETO, PASSWORD, ROL, ESTADO, 
                                              INTENTOFALLIDOS, FECHAINGRESO, IDDOMICILIO 
                                       FROM Usuario 
                                       ORDER BY FECHAINGRESO DESC";
                    cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader lector = cmd.ExecuteReader();

                    while (lector.Read())
                    {
                        Usuarios usuario = new Usuarios();
                        usuario.IdUsuario = lector.GetInt32(0);
                        usuario.Username = lector.GetString(1);
                        usuario.NombreCompleto = lector.GetString(2);
                        usuario.Password = lector.GetString(3);
                        usuario.Rol = lector.GetString(4);
                        usuario.Estado = lector.GetBoolean(5);
                        usuario.IntentosFallidos = lector.GetInt32(6);
                        usuario.FechaIngreso = lector.GetDateTime(7);
                        usuario.IdDomicilio = lector.IsDBNull(8) ? 0 : lector.GetInt32(8);

                        lista.Add(usuario);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al leer usuarios: " + ex.Message);
            }

            return lista;
        }
        public Usuarios BuscarPorUsername(string username)
        {
            Usuarios usuario = null;

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"SELECT ID, USERNAME, NOMBRECOMPLETO, PASSWORD, ROL, ESTADO, 
                                              INTENTOFALLIDOS, FECHAINGRESO, IDDOMICILIO 
                                       FROM Usuario 
                                       WHERE USERNAME = @username";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader lector = cmd.ExecuteReader();

                    if (lector.Read())
                    {
                        usuario = new Usuarios();
                        usuario.IdUsuario = lector.GetInt32(0);
                        usuario.Username = lector.GetString(1);
                        usuario.NombreCompleto = lector.GetString(2);
                        usuario.Password = lector.GetString(3);
                        usuario.Rol = lector.GetString(4);
                        usuario.Estado = lector.GetBoolean(5);
                        usuario.IntentosFallidos = lector.GetInt32(6);
                        usuario.FechaIngreso = lector.GetDateTime(7);
                        usuario.IdDomicilio = lector.IsDBNull(8) ? 0 : lector.GetInt32(8);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario: " + ex.Message);
            }

            return usuario;
        }

        public Usuarios BuscarPorId(int idUsuario)
        {
            Usuarios usuario = null;

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"SELECT ID, USERNAME, NOMBRECOMPLETO, PASSWORD, ROL, ESTADO, 
                                              INTENTOFALLIDOS, FECHAINGRESO, IDDOMICILIO 
                                       FROM Usuario 
                                       WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader lector = cmd.ExecuteReader();

                    if (lector.Read())
                    {
                        usuario = new Usuarios();
                        usuario.IdUsuario = lector.GetInt32(0);
                        usuario.Username = lector.GetString(1);
                        usuario.NombreCompleto = lector.GetString(2);
                        usuario.Password = lector.GetString(3);
                        usuario.Rol = lector.GetString(4);
                        usuario.Estado = lector.GetBoolean(5);
                        usuario.IntentosFallidos = lector.GetInt32(6);
                        usuario.FechaIngreso = lector.GetDateTime(7);
                        usuario.IdDomicilio = lector.IsDBNull(8) ? 0 : lector.GetInt32(8);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuario por ID: " + ex.Message);
            }

            return usuario;
        }


        public List<Usuarios> Buscar(string textoBusqueda)
        {
            List<Usuarios> lista = new List<Usuarios>();

            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"SELECT ID, USERNAME, NOMBRECOMPLETO, PASSWORD, ROL, ESTADO, 
                                              INTENTOFALLIDOS, FECHAINGRESO, IDDOMICILIO 
                                       FROM Usuario 
                                       WHERE USERNAME LIKE @texto OR NOMBRECOMPLETO LIKE @texto
                                       ORDER BY FECHAINGRESO DESC";
                    cmd.Parameters.AddWithValue("@texto", "%" + textoBusqueda + "%");
                    cmd.Connection = conn;

                    conn.Open();
                    SqlDataReader lector = cmd.ExecuteReader();

                    while (lector.Read())
                    {
                        Usuarios usuario = new Usuarios();
                        usuario.IdUsuario = lector.GetInt32(0);
                        usuario.Username = lector.GetString(1);
                        usuario.NombreCompleto = lector.GetString(2);
                        usuario.Password = lector.GetString(3);
                        usuario.Rol = lector.GetString(4);
                        usuario.Estado = lector.GetBoolean(5);
                        usuario.IntentosFallidos = lector.GetInt32(6);
                        usuario.FechaIngreso = lector.GetDateTime(7);
                        usuario.IdDomicilio = lector.IsDBNull(8) ? 0 : lector.GetInt32(8);

                        lista.Add(usuario);
                    }

                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuarios: " + ex.Message);
            }

            return lista;
        }


        public bool Modificar(Usuarios usuario)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = @"UPDATE Usuario SET 
                                       USERNAME = @username,
                                       NOMBRECOMPLETO = @nombreCompleto,
                                       ROL = @rol,
                                       IDDOMICILIO = @idDomicilio
                                       WHERE ID = @id";

                    cmd.Parameters.AddWithValue("@id", usuario.IdUsuario);
                    cmd.Parameters.AddWithValue("@username", usuario.Username);
                    cmd.Parameters.AddWithValue("@nombreCompleto", usuario.NombreCompleto);
                    cmd.Parameters.AddWithValue("@rol", usuario.Rol);
                    cmd.Parameters.AddWithValue("@idDomicilio", usuario.IdDomicilio > 0 ? (object)usuario.IdDomicilio : DBNull.Value);

                    cmd.Connection = conn;
                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar usuario: " + ex.Message);
            }
        }

        public bool CambiarPassword(int idUsuario, string nuevaPassword)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Usuario SET PASSWORD = @password WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    cmd.Parameters.AddWithValue("@password", nuevaPassword);
                    cmd.Connection = conn;

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar contraseña: " + ex.Message);
            }
        }


        public bool Eliminar(int idUsuario)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "DELETE FROM Usuario WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    cmd.Connection = conn;

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario: " + ex.Message);
            }
        }

        public bool Desactivar(int idUsuario)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Usuario SET ESTADO = 0 WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    cmd.Connection = conn;

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Reactivar(int idUsuario)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Usuario SET ESTADO = 1 WHERE ID = @id";
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    cmd.Connection = conn;

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool IncrementarIntentosFallidos(string username)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Usuario SET INTENTOFALLIDOS = INTENTOFALLIDOS + 1 WHERE USERNAME = @username";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Connection = conn;

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool ReiniciarIntentosFallidos(string username)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Usuario SET INTENTOFALLIDOS = 0 WHERE USERNAME = @username";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Connection = conn;

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

 
        public bool BloquearUsuario(string username)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "UPDATE Usuario SET ESTADO = 0 WHERE USERNAME = @username";
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Connection = conn;

                    conn.Open();
                    int filasAfectadas = cmd.ExecuteNonQuery();
                    conn.Close();

                    return filasAfectadas > 0;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int ContarVentasPorUsuario(int idUsuario)
        {
            try
            {
                using (SqlConnection conn = conexion.ObtenerConxeion())
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT COUNT(*) FROM Venta WHERE IDUSUARIO = @id";
                    cmd.Parameters.AddWithValue("@id", idUsuario);
                    cmd.Connection = conn;

                    conn.Open();
                    int cantidad = (int)cmd.ExecuteScalar();
                    conn.Close();

                    return cantidad;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }
    }
}