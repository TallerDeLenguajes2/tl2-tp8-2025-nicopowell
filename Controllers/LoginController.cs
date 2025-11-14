using Microsoft.AspNetCore.Mvc;
using MVC.Interfaces;
using MVC.ViewModels;

public class LoginController : Controller
{
    private readonly IAuthenticationService _authenticationService;

    public LoginController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    [HttpPost]
    public IActionResult Login(LoginViewModel loginVM)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVM);
        }

        if (_authenticationService.Login(loginVM.Username, loginVM.Password))
        {
            return RedirectToAction("Index", "Home");
        }

        loginVM.ErrorMessage = "Datos incorrectos";
        return View("Index", loginVM);
    }

    [HttpGet]
    public IActionResult Logout()
    {
        _authenticationService.Logout();
        return RedirectToAction("Index");
    }
}