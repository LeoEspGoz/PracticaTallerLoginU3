using System.Collections.Generic;
using MySql.Data.MySqlClient;
using CapaEntidad;

namespace CapaDatos
{
    public class UsuarioDAO
    {
        public bool Insertar(Usuario u)
        {
            using (MySqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "INSERT INTO usuarios (nombre, correo, contrasena_hash, rol) VALUES (@n,@c,@h,@r)";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@n", u.Nombre);
                cmd.Parameters.AddWithValue("@c", u.Correo);
                cmd.Parameters.AddWithValue("@h", u.ContrasenaHash);
                cmd.Parameters.AddWithValue("@r", u.Rol);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }

        public Usuario? Login(string correo, string hash)
        {
            using (MySqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM usuarios WHERE correo=@c AND contrasena_hash=@h";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@c", correo);
                cmd.Parameters.AddWithValue("@h", hash);
                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    return new Usuario
                    {
                        Id = dr.GetInt32("id"),
                        Nombre = dr.GetString("nombre"),
                        Correo = dr.GetString("correo"),
                        Rol = dr.GetString("rol"),
                        FechaRegistro = dr.GetDateTime("fecha_registro")
                    };
                }
                return null;
            }
        }

        public List<Usuario> Listar()
        {
            List<Usuario> lista = new List<Usuario>();
            using (MySqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "SELECT * FROM usuarios";
                MySqlCommand cmd = new MySqlCommand(query, con);
                con.Open();
                MySqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add(new Usuario
                    {
                        Id = dr.GetInt32("id"),
                        Nombre = dr.GetString("nombre"),
                        Correo = dr.GetString("correo"),
                        Rol = dr.GetString("rol"),
                        FechaRegistro = dr.GetDateTime("fecha_registro")
                    });
                }
            }
            return lista;
        }

        public bool Eliminar(int id)
        {
            using (MySqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "DELETE FROM usuarios WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@id", id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
        public bool Actualizar(Usuario u)
        {
            using (MySqlConnection con = Conexion.ObtenerConexion())
            {
                string query = "UPDATE usuarios SET nombre=@n, correo=@c, rol=@r WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, con);
                cmd.Parameters.AddWithValue("@n", u.Nombre);
                cmd.Parameters.AddWithValue("@c", u.Correo);
                cmd.Parameters.AddWithValue("@r", u.Rol);
                cmd.Parameters.AddWithValue("@id", u.Id);
                con.Open();
                return cmd.ExecuteNonQuery() > 0;
            }
        }
    }
}
