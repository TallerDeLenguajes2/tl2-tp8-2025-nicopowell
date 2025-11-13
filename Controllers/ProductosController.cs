using Microsoft.AspNetCore.Mvc;

public class ProductosController: Controller
{
    private ProductoRepository productoRepository;

    public ProductosController()
    {
        productoRepository = new ProductoRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Producto> productos = productoRepository.GetAll();
        return View(productos);
    }
}