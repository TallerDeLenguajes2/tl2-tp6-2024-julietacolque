namespace tl2_tp6_2024_julietacolque.Models;

public class Presupuesto
{
    public int IdPresupuesto { get; set; }
    public string NombreDestinatario { get; set; }

    public DateTime FechaCreacion {get;set;}
    public List<PresupuestoDetalle>Detalle { get; set; } = new();
    public double MontoPresupuesto()
    {
        var monto = Detalle.Sum(p => p.ProductoD.Precio * p.Cantidad);
        return monto;
    }
    public double MontoPresupuestoConIVA()
    {
        var montoIVA = MontoPresupuesto() * 1.21;
        return montoIVA;
    }

    public int CantidadProductos()
    {
        var cant = Detalle.Sum(p=> p.Cantidad);
        return cant;
    }
}
