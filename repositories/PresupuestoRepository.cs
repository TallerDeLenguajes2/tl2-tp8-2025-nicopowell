using Microsoft.Data.Sqlite;

public class PresupuestoRepository
{
    private readonly string cadenaConexion = "Data Source=DB/Tienda.db";
    public void Create(Presupuesto presupuesto)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"
                        INSERT INTO Presupuestos (nombreDestinatario, fechaCreacion) 
                        VALUES (@nombreDestinatario, @fechaCreacion)";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@nombreDestinatario", presupuesto.NombreDestinatario));
        comando.Parameters.Add(new SqliteParameter("@fechaCreacion", presupuesto.FechaCreacion));

        comando.ExecuteNonQuery();

        conexion.Close();
    }

    public List<Presupuesto> GetAll()
    {
        var presupuestos = new List<Presupuesto>();

        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"
                        SELECT *
                        FROM Presupuestos";

        using var comando = new SqliteCommand(sql, conexion);

        using var lector = comando.ExecuteReader();

        while (lector.Read())
        {
            var presupuesto = new Presupuesto
            {
                IdPresupuesto = Convert.ToInt32(lector["idPresupuesto"]),
                NombreDestinatario = lector["nombreDestinatario"].ToString(),
                FechaCreacion = Convert.ToDateTime(lector["fechaCreacion"])
            };
            presupuestos.Add(presupuesto);
        }
        conexion.Close();

        return presupuestos;
    }

    public Presupuesto GetById(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"
                        SELECT 
                            p.idPresupuesto, 
                            p.nombreDestinatario, 
                            p.fechaCreacion, 
                            prod.idProducto, 
                            prod.descripcion, 
                            prod.precio, 
                            d.cantidad                    
                        FROM 
                            Presupuestos AS p
                        INNER JOIN 
                            PresupuestosDetalle AS d ON p.idPresupuesto = d.idPresupuesto
                        INNER JOIN 
                            Productos AS prod ON d.idProducto = prod.idProducto
                        WHERE 
                            p.idPresupuesto = 1";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@idProducto", id));

        using var lector = comando.ExecuteReader();
        Presupuesto presupuesto = null;
        while (lector.Read())
        {
            if (presupuesto == null)
            {
                presupuesto = new Presupuesto
                {
                    IdPresupuesto = Convert.ToInt32(lector["idPresupuesto"]),
                    NombreDestinatario = lector["nombreDestinatario"].ToString(),
                    FechaCreacion = Convert.ToDateTime(lector["fechaCreacion"]),
                };
            }
            ;
            var presupuestoDetalle = new PresupuestoDetalle
            {
                Producto = new Producto
                {
                    IdProducto = Convert.ToInt32(lector["idProducto"]),
                    Descripcion = lector["producto"].ToString(),
                    Precio = Convert.ToInt32(lector["precio"])
                },
                Cantidad = Convert.ToInt32(lector["cantidad"])
            };

        }
        conexion.Close();
        return presupuesto;
    }

    public bool Exist(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"
                        SELECT *
                        FROM Presupuestos
                        WHERE idPresupuesto = @idPresupuesto";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@idPresupuesto", id));

        using var lector = comando.ExecuteReader();

        if (lector.Read())
        {
            conexion.Close();
            return true;
        }
        conexion.Close();
        return false;
    }

    public void CreateDetalle(int idPresupuesto, PresupuestoDetalle detalle)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"
                        INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, cantidad) 
                        VALUES (@idPresupuesto, @idProducto, @cantidad)";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@idPresupuesto", idPresupuesto));
        comando.Parameters.Add(new SqliteParameter("@idProducto", detalle.Producto.IdProducto));
        comando.Parameters.Add(new SqliteParameter("@cantidad", detalle.Cantidad));

        comando.ExecuteNonQuery();
        conexion.Close();
    }

    public void DeleteById(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        conexion.Open();

        string sql = @"DELETE FROM Presupuestos WHERE idPresupuesto = @idPresupuesto";

        using var comando = new SqliteCommand(sql, conexion);

        comando.Parameters.Add(new SqliteParameter("@idPresupuesto", id));
        comando.ExecuteNonQuery();
        conexion.Close();
    }
}