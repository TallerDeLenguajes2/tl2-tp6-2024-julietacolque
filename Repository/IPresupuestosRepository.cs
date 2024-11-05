
using tl2_tp6_2024_julietacolque.Models;

namespace tl2_tp6_2024_julietacolque.Repository;

public interface IPresupuestosRepository
{
    public void CrearPresupuesto(Presupuesto presupuesto);
    public List<Presupuesto> ListarPresupuestos();
    public Presupuesto PresupuestoDetallePorID(int id);
    public void AgregarProducto(int idPresupuesto,int idProducto , int cantidad);
    public void EliminarPresupuesto(int id);
}