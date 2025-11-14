using Microsoft.Data.Sqlite;
using MVC.Interfaces;

namespace MVC.Repositorios;
public class ProductoRepository : IProductoRepository
{
    private readonly string cadenaConexion = "Data Source=DB/Tienda.db";
    public void Create(Producto producto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"
                        INSERT INTO Productos (descripcion, precio) 
                        VALUES (@descripcion, @precio)";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@descripcion", producto.Descripcion));
        comando.Parameters.Add(new SqliteParameter("@precio", producto.Precio));

        comando.ExecuteNonQuery();

        conexion.Close();
    }

    public void Update(int id, Producto nuevoProducto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"
                        UPDATE Productos 
                        SET descripcion = @descripcion, precio = @precio 
                        WHERE idProducto = @idProducto";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@descripcion", nuevoProducto.Descripcion));
        comando.Parameters.Add(new SqliteParameter("@precio", nuevoProducto.Precio));
        comando.Parameters.Add(new SqliteParameter("@idProducto", id));

        comando.ExecuteNonQuery();

        conexion.Close();
    }

    public List<Producto> GetAll()
    {
        var productos = new List<Producto>();

        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"
                        SELECT *
                        FROM Productos";

        using var comando = new SqliteCommand(sql, conexion);

        using var lector = comando.ExecuteReader();

        while (lector.Read())
        {
            var producto = new Producto
            {
                IdProducto = Convert.ToInt32(lector["idProducto"]),
                Descripcion = lector["descripcion"].ToString(),
                Precio = Convert.ToInt32(lector["precio"])
            };
            productos.Add(producto);
        }
        conexion.Close();

        return productos;
    }

    public Producto GetById(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"
                        SELECT *
                        FROM Productos
                        WHERE idProducto = @idProducto";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@idProducto", id));

        using var lector = comando.ExecuteReader();

        if (lector.Read())
        {
            var producto = new Producto
            {
                IdProducto = Convert.ToInt32(lector["idProducto"]),
                Descripcion = lector["descripcion"].ToString(),
                Precio = Convert.ToInt32(lector["precio"])
            };
            conexion.Close();
            return producto;
        }
        conexion.Close();
        return null;
    }

    public void DeleteById(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"DELETE FROM Productos WHERE idProducto = @idProducto";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@idProducto", id));
        comando.ExecuteNonQuery();
        conexion.Close();
    }
}