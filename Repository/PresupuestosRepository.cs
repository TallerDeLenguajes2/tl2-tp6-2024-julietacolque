namespace tl2_tp6_2024_julietacolque.Repository;

using Microsoft.Data.Sqlite;
using tl2_tp6_2024_julietacolque.Models;
public class PresupuestosRepository : IPresupuestosRepository
{
    private readonly string cadenaConexion = "Data Source = DB/Tienda.db;Cache = Shared";
    public void CrearPresupuesto(Presupuesto presupuesto)
    {

        using var conexion = new SqliteConnection(cadenaConexion);

        var query = @"INSERT INTO Presupuestos (NombreDestinatario, FechaCreacion) VALUES (@nombre,@fecha)";
        conexion.Open();
        using (var comando = new SqliteCommand(query, conexion))
        {

            comando.Parameters.Add(new SqliteParameter("@nombre", presupuesto.NombreDestinatario));
            comando.Parameters.Add(new SqliteParameter("@fecha", presupuesto.FechaCreacion));
            comando.ExecuteNonQuery();
        }
        conexion.Close();

    }
    public List<Presupuesto> ListarPresupuestos()
    {

        var listaP = new List<Presupuesto>();
        using var conexion = new SqliteConnection(cadenaConexion);
        var query = @"SELECT idPresupuesto,NombreDestinatario,FechaCreacion FROM Presupuestos";
        conexion.Open();
        using var comando = new SqliteCommand(query, conexion);
        using (var reader = comando.ExecuteReader())
        {

            while (reader.Read())
            {
                var presupuesto = new Presupuesto()
                {
                    IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]),
                    NombreDestinatario = reader["NombreDestinatario"].ToString(),
                    FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString())
                };
                listaP.Add(presupuesto);
            }
            conexion.Close();
        }

        return listaP;
    }


    /*Obtener detalles de un Presupuesto por su ID. (recibe un Id y devuelve un
     Presupuesto con sus productos y cantidades)*/
    public Presupuesto PresupuestoDetallePorID(int id)
    {
        var presupuesto = new Presupuesto();
        using var conexion = new SqliteConnection(cadenaConexion);
        var query = @"SELECT P.idPresupuesto, P.NombreDestinatario,P.FechaCreacion,PR.idProducto, PR.Descripcion AS Producto, PR.Precio, PD.Cantidad
                      FROM 
                      Presupuestos P
                      JOIN 
                      PresupuestosDetalle PD ON P.idPresupuesto = PD.idPresupuesto
                      JOIN 
                      Productos PR ON PD.idProducto = PR.idProducto
                      WHERE 
                      P.idPresupuesto = @id";

        conexion.Open();
        using var comando = new SqliteCommand(query, conexion);
        comando.Parameters.Add(new SqliteParameter("@id", id));

        using (var reader = comando.ExecuteReader())
        {
            bool datosP = true;
            while (reader.Read())
            {
                if (datosP)
                {//presupuesto datos 
                    presupuesto.IdPresupuesto = Convert.ToInt32(reader["idPresupuesto"]);
                    presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    presupuesto.FechaCreacion = DateTime.Parse(reader["FechaCreacion"].ToString());
                    datosP = false;
                };


                //producto 
                var prod = new Producto()
                {
                    IdProducto = Convert.ToInt32(reader["idProducto"]),
                    Descripcion = reader["Producto"].ToString(),
                    Precio = Convert.ToInt32(reader["Precio"])
                };

                //armado list presupuestoDetalle
                var presupuestoD = new PresupuestoDetalle()
                {
                    ProductoD = prod,
                    Cantidad = Convert.ToInt32(reader["Cantidad"])
                };
                presupuesto.Detalle.Add(presupuestoD);

            }
            conexion.Close();
        }
        return presupuesto;
    }



    // Agregar un producto y una cantidad a un presupuesto (recibe un Id)
    public void AgregarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        cantidad = (cantidad > 0) ? cantidad : 1;
        using var conexion = new SqliteConnection(cadenaConexion);
        var query = @"INSERT INTO PresupuestosDetalle (idPresupuesto, idProducto, Cantidad) VALUES (@idPre,@idProd,@cantidad); ";
        conexion.Open();
        using (var comando = new SqliteCommand(query, conexion))
        {
            comando.Parameters.Add(new SqliteParameter("@idPre", idPresupuesto));
            comando.Parameters.Add(new SqliteParameter("@idProd", idProducto));
            comando.Parameters.Add(new SqliteParameter("@cantidad", cantidad));
            comando.ExecuteNonQuery();
        }
        conexion.Close();

    }
    public void EliminarPresupuesto(int id)
    {
        //ON DELETE CASCADE sin configurar(dos querys)
        using var conexion = new SqliteConnection(cadenaConexion);
        var queryPD = @"DELETE FROM PresupuestosDetalle WHERE idPresupuesto = @idP;";
        var queryP = @"DELETE FROM Presupuestos WHERE idPresupuesto = @idP;";

        conexion.Open();
        using (var comando = new SqliteCommand(queryPD, conexion))
        {
            comando.Parameters.Add(new SqliteParameter("@idP", id));
            comando.ExecuteNonQuery();
        }
        using (var comando = new SqliteCommand(queryP, conexion))
        {
            comando.Parameters.Add(new SqliteParameter("@idP", id));
            comando.ExecuteNonQuery();
        }
        conexion.Close();

    }
    public void ModificarPresupuesto(Presupuesto presupuesto, int idProducto, int cantidad)
    {
        using var conexion = new SqliteConnection(cadenaConexion);
        var query = @"Update PresupuestosDetalle SET cantidad = @cant WHERE idPresupuesto=@idPre AND idProducto=@idProd";
        conexion.Open();
        using (var comando = new SqliteCommand(query, conexion))
        {
            comando.Parameters.Add(new SqliteParameter("@idPre", presupuesto.IdPresupuesto));
            comando.Parameters.Add(new SqliteParameter("@idProd", idProducto));
            comando.Parameters.Add(new SqliteParameter("@cant", cantidad));
            comando.ExecuteNonQuery();
        }
        conexion.Close();

    }
    public void EliminarProducto(int idPresupuesto,int idProducto){
         using var conexion = new SqliteConnection(cadenaConexion);
         var query = @"DELETE FROM presupuestosDetalle WHERE idPresupuesto=@idPre AND idProducto=@idProd";
         conexion.Open();
          using (var comando = new SqliteCommand(query, conexion))
        {
            comando.Parameters.Add(new SqliteParameter("@idPre", idPresupuesto));
            comando.Parameters.Add(new SqliteParameter("@idProd", idProducto));
            comando.ExecuteNonQuery();
        }
        conexion.Close();
    }
  
}