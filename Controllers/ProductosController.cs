using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Web.ViewModels;
public class ProductosController : Controller
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
        return View(new ProductoViewModel());
    }

    [HttpPost]
    public IActionResult Create(ProductoViewModel productoVM)
    {
        // 1. CHEQUEO DE SEGURIDAD DEL SERVIDOR
        if (!ModelState.IsValid)
        {
            // Si falla: Devolvemos el ViewModel con los datos y errores a la Vista
            return View(productoVM);
        }

        // 2. SI ES VÁLIDO: Mapeo Manual de VM a Modelo de Dominio
        var nuevoProducto = new Producto
        {
            Descripcion = productoVM.Descripcion,
            Precio = productoVM.Precio
        };

        // 3. Llamada al Repositorio
        _productoRepository.Create(nuevoProducto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Producto producto = _productoRepository.GetById(id);

        // 1. Mapeo Manual de Modelo a ViewModel
        var productoVM = new ProductoViewModel
        {
            IdProducto = producto.IdProducto,
            Descripcion = producto.Descripcion,
            Precio = producto.Precio
        };

        return View(productoVM); //  Pasa el ViewModel
    }

    [HttpPost]
    public IActionResult Edit(int id, ProductoViewModel productoVM)
    {
        if (id != productoVM.IdProducto) return NotFound();

        // 1. CHEQUEO DE SEGURIDAD DEL SERVIDOR 
        if (!ModelState.IsValid)
        {
            return View(productoVM);
        }

        // 2. Mapeo Manual de VM a Modelo de Dominio
        var productoAEditar = new Producto
        {
            IdProducto = productoVM.IdProducto, // Necesario para el UPDATE
            Descripcion = productoVM.Descripcion,
            Precio = productoVM.Precio
        };

        // 3. Llamada al Repositorio
        _productoRepository.Update(id, productoAEditar);
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