using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_julietacolque.Models;
using tl2_tp6_2024_julietacolque.Repository;
namespace tl2_tp6_2024_julietacolque.Controllers;


[ApiController]
[Route("[controller]")]
public class ProductosController : Controller
{
    private readonly ProductosRepository repoProducto;
    private readonly ILogger<IProductosRepository> _logger;
    public ProductosController(ILogger<IProductosRepository> logger)
    {
        _logger = logger;
        repoProducto = new();

    }


    [HttpGet]
    public IActionResult Index()
    {
        var listaP = repoProducto.ListarProductos();
        if (listaP.Count == 0) return View();
        return View(listaP);
    }

    [HttpGet]
    public IActionResult Crear()
    {
        return View();
    }

    [HttpPost]
    public IActionResult AddProducto(Producto producto)
    {
        repoProducto.CrearProducto(producto);
        return RedirectToAction("index", "Productos");

    }

    [HttpGet]
    public IActionResult Modificar(int id)
    {
        //debo buscar el producto
        var prod = repoProducto.ProductoPorID(id);
        //controlar

        return View(prod);
    }


    [HttpPost]
    public IActionResult Actualizar(Producto producto)
    {
        repoProducto.ModificarProducto(producto.IdProducto, producto);

        return RedirectToAction("index");
    }


    [HttpPost] //form envia post o get luego no entra al controlador con delete
    public IActionResult Eliminar(int IdProducto) //debe coincidir /form
    {
        Console.WriteLine(IdProducto);
        repoProducto.EliminarProducto(IdProducto);
        return RedirectToAction("index","Productos");
    }


}


