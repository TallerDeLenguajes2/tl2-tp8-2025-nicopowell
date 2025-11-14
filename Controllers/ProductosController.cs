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

    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Producto producto)
    {
        _productoRepository.Create(producto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Producto producto = _productoRepository.GetById(id);
        return View(producto);
    }

    [HttpPost]
    public IActionResult Edit(int id, Producto productoEditado)
    {
        _productoRepository.Update(id, productoEditado);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Producto producto = _productoRepository.GetById(id);
        return View(producto);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmado(int id)
    {
        _productoRepository.DeleteById(id);
        return RedirectToAction("Index");
    }
}