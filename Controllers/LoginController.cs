
using Microsoft.AspNetCore.Mvc;
using tl2_tp6_2024_julietacolque.ViewModels;
using tl2_tp6_2024_julietacolque.Models;
namespace tl2_tp6_2024_julietacolque.Controllers;
public class LoginController : Controller
{
    private readonly ILogger<LoginController> _logger;
    private readonly IUserRepository _InMemoryUserRepository;
    public LoginController(ILogger<LoginController> logger, IUserRepository inMemoryUserRepository)
    {
        _logger = logger;
        _InMemoryUserRepository = inMemoryUserRepository;
    }

    //accion que devuelve la vista del login 
    public IActionResult Index()
    {
        var model = new LoginViewModel()
        {
            IsAuthenticated = HttpContext.Session.GetString("IsAuthenticated") == "true"
        };
        return View(model);
    }
    [HttpPost]
    public IActionResult Login(LoginViewModel model)
    {
        //verificar que los datos de entrada no esten vacios
        if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
        {
            model.ErrorMessage = "Por favor ingrese su nombre de usuario y contraseña.";

        }
        //si el usuarioexiste y las credenciales son correctas
        Usuario usuario = _InMemoryUserRepository.GetUser(model.Username, model.Password);
        if (usuario != null)
        {
            HttpContext.Session.SetString("IsAuthenticated", "true");
            HttpContext.Session.SetString("User", usuario.UserName);
            HttpContext.Session.SetString("AccessLevel", usuario.AccessLevel.ToString());
            return RedirectToAction("Index", "Home");
        }
        //si las  credenciales no son correctas, mostrar mensaje de error
        model.ErrorMessage = "Credenciales Invalidas";
        model.IsAuthenticated = false;
        return View("Index", model);

    }
    [HttpGet]
    public IActionResult Logout()
    {
        // Limpiar la sesión
        HttpContext.Session.Clear();

        // Redirigir a la vista de login
        return RedirectToAction("Index");
    }



}