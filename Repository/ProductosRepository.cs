using Microsoft.Data.Sqlite;
using tl2_tp6_2024_julietacolque.Models;

namespace tl2_tp6_2024_julietacolque.Repository;

public class ProductosRepository : IProductosRepository
{
    private readonly string cadenaConexion = "Data Source = DB/Tienda.db;Cache = Shared";

    public void CrearProducto(Producto prod)
    {
        string query = @"INSERT INTO Productos (Descripcion, Precio) VALUES (@descripcion, @precio)";

        using SqliteConnection conexion = new(cadenaConexion);
        conexion.Open();

        using (var comando = new SqliteCommand(query, conexion))
        {
            comando.Parameters.Add(new SqliteParameter("@descripcion", prod.Descripcion));
            comando.Parameters.Add(new SqliteParameter("@precio", prod.Precio));

            comando.ExecuteNonQuery();
        }

        conexion.Close();
    }
    public void ModificarProducto(int id, Producto prod)
    {
        using var conexion = new SqliteConnection(cadenaConexion);

        var query = @"UPDATE Productos SET descripcion = @desc,precio = @precio WHERE idProducto = @id";
        conexion.Open();
        using (var comando = new SqliteCommand(query, conexion))
        {
            comando.Parameters.Add(new SqliteParameter("@desc", prod.Descripcion));
            comando.Parameters.Add(new SqliteParameter("@precio", prod.Precio));
            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.ExecuteNonQuery();
        }
        conexion.Close();

    }
    public List<Producto> ListarProductos()
    {
        var listaP = new List<Producto>();

        string query = @"SELECT idProducto,descripcion,precio FROM Productos;";

        using var conexion = new SqliteConnection(cadenaConexion);

        var comando = new SqliteCommand(query, conexion);

        conexion.Open();

        using (var reader = comando.ExecuteReader())
        {
            while (reader.Read())
            {
                var prod = new Producto()
                {
                    IdProducto = Convert.ToInt32(reader["idProducto"]),
                    Descripcion = reader["descripcion"].ToString(),
                    Precio = Convert.ToInt32(reader["precio"])
                };
                listaP.Add(prod);
            }
            conexion.Close();
        }
        return listaP;
    }

    public Producto ProductoPorID(int id)
    {
        var prod = new Producto();
        using var conexion = new SqliteConnection(cadenaConexion);
        var query = @"SELECT idProducto,descripcion,precio FROM Productos WHERE idProducto=@id;";
    
        conexion.Open();
        using var comando = new SqliteCommand(query, conexion);
        comando.Parameters.Add(new SqliteParameter("@id", id));

       
        using (var reader = comando.ExecuteReader())
        {
            while (reader.Read())
            {
                prod.IdProducto = Convert.ToInt32(reader["idProducto"]);
                prod.Descripcion = reader["descripcion"].ToString();
                prod.Precio = Convert.ToInt32(reader["precio"]);
            }
            conexion.Close();
        }

        return prod;
    }
    public void EliminarProducto(int id)
    {
        using var conexion = new SqliteConnection(cadenaConexion);

        var query = @"DELETE FROM productos WHERE idProducto = @id;";

        conexion.Open();
        using (var comando = new SqliteCommand(query, conexion))
        {

            comando.Parameters.Add(new SqliteParameter("@id", id));
            comando.ExecuteNonQuery();

        }
        conexion.Close();
    }


}
//crear conexion
//2 consulta
// crear comando de consulta
// add parameters si los hubiera
//abrir la conexion
