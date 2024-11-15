using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_julietacolque.Models;
using tl2_tp6_2024_julietacolque.Repository;
using tl2_tp6_2024_julietacolque.ViewModels;
namespace tl2_tp6_2024_julietacolque.Controllers;


public class PresupuestosController : Controller
{
    private readonly PresupuestosRepository repoPresupuesto;
    private readonly ProductosRepository repoProductos;
    private readonly ILogger<IPresupuestosRepository> _logger;
    public PresupuestosController(ILogger<IPresupuestosRepository> logger)
    {
        _logger = logger;
        repoPresupuesto = new();
        repoProductos = new();
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
        var presupuesto = repoPresupuesto.PresupuestoDetallePorID(id);
        if (presupuesto.IdPresupuesto == 0)
        {
            var presupuestoVacio = repoPresupuesto.ListarPresupuestos().FirstOrDefault(p => p.IdPresupuesto == id);
            return View(presupuestoVacio);
        }
        return View(presupuesto);
    }
    [HttpGet]
    public IActionResult Modificar(int id)
    {
        var presupuesto = repoPresupuesto.PresupuestoDetallePorID(id);
        if (presupuesto.IdPresupuesto == 0)
        {
            var presupuestoVacio = repoPresupuesto.ListarPresupuestos().FirstOrDefault(p => p.IdPresupuesto == id);
            return View(presupuestoVacio);
        }
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
        repoPresupuesto.EliminarProducto(idPresupuesto, idProducto);
        return RedirectToAction("Modificar", new { id = idPresupuesto });

    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View(new Presupuesto());
    }

    [HttpPost]
    public IActionResult AddPresupuesto(string nombreDestinatario)
    {

        var presupuesto = new Presupuesto
        {
            NombreDestinatario = nombreDestinatario,
            FechaCreacion = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day)
        };
        repoPresupuesto.CrearPresupuesto(presupuesto);
        return RedirectToAction("index", "Presupuestos");
    }

    [HttpGet]
    public IActionResult CargarProducto(int idPresupuesto)
    {

        var infoVista = new AgregarProductoViewModel
        {
            IdPresupuesto = idPresupuesto,
            ListaProductos = repoProductos.ListarProductos()
        };
        return View(infoVista);
    }
    [HttpPost]
    public IActionResult AddProducto(int idPresupuesto, int idProducto, int cantidad)
    {
        if (cantidad <= 0 || cantidad >= 15) cantidad = 1;

        //buscar presupuesto obtener detalle si hay algun producto solo llamar a la funcion moodificar
        var presupuesto = repoPresupuesto.PresupuestoDetallePorID(idPresupuesto);

        var producto = presupuesto.Detalle.FirstOrDefault(p => p.ProductoD.IdProducto == idProducto);

        //verifica si hay ese producto en el presupuesto, si hay modifica el presupuesto

        if (producto is null) { repoPresupuesto.AgregarProducto(idPresupuesto, idProducto, cantidad); }
        //si no, lo agrega 
        else
        {
            cantidad += producto.Cantidad;
            repoPresupuesto.ModificarPresupuesto(presupuesto, idProducto, cantidad);

        }

        return RedirectToAction("Modificar", new { id = idPresupuesto });
    }

       [HttpPost] //form envia post o get luego no entra al controlador con delete
    public IActionResult Eliminar(int idPresupuesto) //debe coincidir /form
    {

        repoPresupuesto.EliminarPresupuesto(idPresupuesto);
        return RedirectToAction("index", "Presupuestos");
    }
}
