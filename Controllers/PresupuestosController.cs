using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_julietacolque.Models;
using tl2_tp6_2024_julietacolque.Repository;
namespace tl2_tp6_2024_julietacolque.Controllers;


public class PresupuestosController : Controller
{
    private readonly PresupuestosRepository repoPresupuesto;
    private readonly ILogger<IPresupuestosRepository> _logger;
    public PresupuestosController(ILogger<IPresupuestosRepository> logger)
    {
        _logger = logger;
        repoPresupuesto = new();

    }
    [HttpGet]
    public IActionResult Index()
    {
        // var lista = repoPresupuesto.ListarPresupuestos();
        return View(repoPresupuesto.ListarPresupuestos());
    }
    [HttpGet]
    public IActionResult Detalle(int id)
    {
        //asp-route-id => en el controlador debe esperar un "id"     
        var presupuestoD = repoPresupuesto.PresupuestoDetallePorID(id);
        return View(presupuestoD);
    }
    [HttpGet]
    public IActionResult Modificar(int id)
    {
        var presupuesto = repoPresupuesto.PresupuestoDetallePorID(id);
        return View(presupuesto);
    }
    [HttpPost]
    public IActionResult ModificarProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        if (cantidad <= 0 || cantidad >= 15) cantidad = 1;
        var presupuesto = repoPresupuesto.PresupuestoDetallePorID(idPresupuesto);
        repoPresupuesto.ModificarPresupuesto(presupuesto, idProducto, cantidad);
        return RedirectToAction("Modificar", new { id = idPresupuesto });
    }
    [HttpPost]
    public IActionResult EliminarProducto(int idPresupuesto, int idProducto)
    {
        repoPresupuesto.EliminarProducto(idPresupuesto,idProducto);
        return RedirectToAction("Modificar", new { id = idPresupuesto});



    }
}
