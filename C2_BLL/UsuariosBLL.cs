using C2_BLL;
using C3_DAL;
using C4_ENTIDAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace C2_BLL
{
    public class UsuarioBLL
    {
        private UsuarioDAL usuarioDAL = new UsuarioDAL();

        public Usuarios IniciarSesion(string username, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                {
                    throw new Exception("Deben completarse todos los campos");
                }

                Usuarios usuario = usuarioDAL.BuscarPorUsername(username.Trim());

                if (usuario == null)
                {
                    throw new Exception("El usuario o contraseña son incorrectos");
                }

                if (usuario.EstaBloqueado())
                {
                    throw new Exception("Usuario bloqueado. Contacte al administrador.");
                }

                if (!VerificarPassword(password, usuario.Password))
                {
                    usuarioDAL.IncrementarIntentosFallidos(username);

                    if (usuario.IntentosFallidos + 1 >= 3)
                    {
                        usuarioDAL.BloquearUsuario(username);
                        throw new Exception("Usuario bloqueado por múltiples intentos fallidos. Contacte al administrador.");
                    }

                    throw new Exception("El usuario o contraseña son incorrectos");
                }

                usuarioDAL.ReiniciarIntentosFallidos(username);
                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool RegistrarUsuario(Usuarios usuario)
        {
            try
            {
                ValidarCamposObligatorios(usuario);

                ValidarUsername(usuario.Username);


                ValidarPassword(usuario.Password);

                if (ExisteUsername(usuario.Username))
                {
                    throw new Exception("El nombre de usuario ya existe.");
                }

                usuario.Estado = true;
                usuario.IntentosFallidos = 0;
                usuario.FechaIngreso = DateTime.Now;

                usuarioDAL.Agregar(usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al registrar el usuario: " + ex.Message);
            }
        }

        public List<Usuarios> ObtenerTodosLosUsuarios()
        {
            try
            {
                return usuarioDAL.Leer();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener la lista de usuarios: " + ex.Message);
            }
        }

        public List<Usuarios> BuscarUsuarios(string textoBusqueda)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(textoBusqueda))
                {
                    throw new Exception("Debe ingresar un criterio de búsqueda");
                }

                List<Usuarios> resultados = usuarioDAL.Buscar(textoBusqueda);

                if (resultados.Count == 0)
                {
                    throw new Exception("No se encontraron coincidencias");
                }

                return resultados;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar usuarios: " + ex.Message);
            }
        }

        public bool ModificarUsuario(Usuarios usuario)
        {
            try
            {
                if (usuario.IdUsuario <= 0)
                {
                    throw new Exception("ID de usuario inválido");
                }

                ValidarCamposObligatorios(usuario);

                ValidarUsername(usuario.Username);

                if (ExisteUsernameExcluyendo(usuario.Username, usuario.IdUsuario))
                {
                    throw new Exception("El nombre de usuario ya existe.");
                }

                usuarioDAL.Modificar(usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al modificar el usuario: " + ex.Message);
            }
        }

        public bool CambiarPassword(int idUsuario, string nuevaPassword)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    throw new Exception("ID de usuario inválido");
                }

                ValidarPassword(nuevaPassword);

                usuarioDAL.CambiarPassword(idUsuario, nuevaPassword);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar la contraseña: " + ex.Message);
            }
        }


        public bool EliminarUsuario(int idUsuario, int idUsuarioActual)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    throw new Exception("ID de usuario inválido");
                }

                // No permitir eliminar el propio usuario
                if (idUsuario == idUsuarioActual)
                {
                    throw new Exception("No puede eliminar el usuario con sesión activa.");
                }

                // Verificar si tiene ventas asociadas
                int cantidadVentas = usuarioDAL.ContarVentasPorUsuario(idUsuario);
                if (cantidadVentas > 0)
                {
                    throw new Exception("No se puede eliminar este usuario porque está asociado a registros de venta.");
                }

                // Eliminar usuario
                usuarioDAL.Eliminar(idUsuario);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar usuario: " + ex.Message);
            }
        }

        // ===============================================
        // DESACTIVAR/REACTIVAR USUARIO
        // ===============================================
        public bool DesactivarUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    throw new Exception("ID de usuario inválido");
                }

                bool resultado = usuarioDAL.Desactivar(idUsuario);
                if (!resultado)
                {
                    throw new Exception("No se pudo desactivar el usuario");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al desactivar usuario: " + ex.Message);
            }
        }

        public bool ReactivarUsuario(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    throw new Exception("ID de usuario inválido");
                }

                bool resultado = usuarioDAL.Reactivar(idUsuario);
                if (!resultado)
                {
                    throw new Exception("No se pudo reactivar el usuario");
                }

                // Reiniciar intentos fallidos al reactivar
                Usuarios usuario = usuarioDAL.BuscarPorId(idUsuario);
                if (usuario != null)
                {
                    usuarioDAL.ReiniciarIntentosFallidos(usuario.Username);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al reactivar usuario: " + ex.Message);
            }
        }


        private bool VerificarPassword(string passwordIngresada, string passwordAlmacenada)
        {
            return passwordIngresada == passwordAlmacenada;
        }

        private void ValidarUsername(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("El nombre de usuario es obligatorio");
            }

            if (username.Length < 3)
            {
                throw new Exception("El nombre de usuario debe tener al menos 3 caracteres");
            }

            if (!Regex.IsMatch(username, @"^[a-zA-Z0-9_]+$"))
            {
                throw new Exception("El nombre de usuario solo puede contener letras, números y guión bajo");
            }
        }

        private void ValidarPassword(string password)
        {
            List<string> errores = new List<string>();

            if (string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("La contraseña es obligatoria");
            }


            if (password.Length < 10)
            {
                errores.Add("Largo mínimo 10 caracteres");
            }


            if (!Regex.IsMatch(password, @"[A-Z]"))
            {
                errores.Add("Contener al menos una mayúscula");
            }

            if (!Regex.IsMatch(password, @"[a-z]"))
            {
                errores.Add("Contener al menos una minúscula");
            }

            if (errores.Count > 0)
            {
                string mensaje = "La contraseña debe contener:\n" + string.Join("\n", errores);
                throw new Exception(mensaje);
            }
        }

        private void ValidarCamposObligatorios(Usuarios usuario)
        {
            List<string> camposVacios = new List<string>();

            if (string.IsNullOrWhiteSpace(usuario.Username))
                camposVacios.Add("Usuario");

            if (string.IsNullOrWhiteSpace(usuario.NombreCompleto))
                camposVacios.Add("Nombre Completo");

            if (string.IsNullOrWhiteSpace(usuario.Rol))
                camposVacios.Add("Rol");

            if (usuario.IdUsuario == 0 && string.IsNullOrWhiteSpace(usuario.Password))
                camposVacios.Add("Contraseña");

            if (camposVacios.Count > 0)
            {
                string mensaje = "Los siguientes campos son obligatorios: " + string.Join(", ", camposVacios);
                throw new Exception(mensaje);
            }
        }

        private bool ExisteUsername(string username)
        {
            Usuarios usuario = usuarioDAL.BuscarPorUsername(username);
            return usuario != null;
        }

        private bool ExisteUsernameExcluyendo(string username, int idUsuarioActual)
        {
            List<Usuarios> todosUsuarios = usuarioDAL.Leer();
            return todosUsuarios.Any(u =>
                u.IdUsuario != idUsuarioActual &&
                u.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
            );
        }
    }
}