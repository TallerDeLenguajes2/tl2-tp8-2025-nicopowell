using Microsoft.AspNetCore.Mvc;

public class ProductosController: Controller
{
    private readonly ProductoRepository _productoRepository;

    public ProductosController()
    {
        _productoRepository = new ProductoRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Producto> productos = _productoRepository.GetAll();
        return View(productos);
    }
}