using MySql.Data.MySqlClient;

namespace CapaDatos
{
    public static class Conexion
    {
        private static string cadena = "Server=localhost;Database=logincrud;Uid=root;Pwd=root;";

        public static MySqlConnection ObtenerConexion()
        {
            return new MySqlConnection(cadena);
        }
    }
}