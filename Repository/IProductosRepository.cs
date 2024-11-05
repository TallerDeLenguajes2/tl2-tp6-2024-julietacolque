
using tl2_tp6_2024_julietacolque.Models;

namespace tl2_tp6_2024_julietacolque.Repository;

public interface IProductosRepository{
        public void CrearProducto(Producto prod);

        public void ModificarProducto(int id, Producto prod);
        public List<Producto>ListarProductos();
        public Producto ProductoPorID(int id);
        public void EliminarProducto(int id);

}