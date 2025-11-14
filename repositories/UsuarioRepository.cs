using Microsoft.Data.Sqlite;
using MVC.Interfaces;

namespace MVC.Repositorios;

public class UsuarioRepository : IUserRepository
{
    private readonly string cadenaConexion = "Data Source=DB/Tienda.db";
    public Usuario GetUser(string username, string password)
    {
        Usuario user = null;

        const string sql = @"
            SELECT Id, Nombre, User, Pass, Rol
            FROM Usuarios
            WHERE User = @Usuario AND Pass = @Contrasenia
        ";

        using var conexion = new SqliteConnection(cadenaConexion);

        conexion.Open();

        using var comando = new SqliteCommand(sql, conexion);
        comando.Parameters.AddWithValue("@Usuario", username);
        comando.Parameters.AddWithValue("@Contrasenia", password);

        using var reader = comando.ExecuteReader();

        if (reader.Read())
        {
            // Si el lector encuentra una fila, el usuario existe y las credenciales son correctas
            user = new Usuario
            {
                IdUsuario = reader.GetInt32(0),
                Nombre = reader.GetString(1),
                Username = reader.GetString(2),
                Password = reader.GetString(3),
                Rol = (Roles) Enum.Parse(typeof(Roles), reader.GetString(4))
            };
        }

        return user;
    }
}