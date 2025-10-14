using CapaDatos;
using CapaEntidad;
using CapaUtilidades;

namespace CapaNegocio
{
    public class UsuarioNegocio
    {
        private readonly UsuarioDAO dao = new UsuarioDAO();

        public bool Registrar(string nombre, string correo, string contrasena)
        {
            string hash = Seguridad.CalcularSHA256(contrasena);
            Usuario u = new Usuario
            {
                Nombre = nombre,
                Correo = correo,
                ContrasenaHash = hash
            };
            return dao.Insertar(u);
        }

        public Usuario? IniciarSesion(string correo, string contrasena)
        {
            return dao.Login(correo, contrasena);
        }

        public List<Usuario> ObtenerUsuarios()
        {
            return dao.Listar();
        }

        public bool EliminarUsuario(int id)
        {
            return dao.Eliminar(id);
        }
        public bool ActualizarUsuario(Usuario u)
        {
            return dao.Actualizar(u);
        }
    }
}
