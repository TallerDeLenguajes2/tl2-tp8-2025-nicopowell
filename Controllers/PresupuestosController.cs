using Microsoft.AspNetCore.Mvc;
using SistemaVentas.Web.ViewModels;
using MVC.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering; // Necesario para SelectList

public class PresupuestosController : Controller
{
    private readonly IPresupuestoRepository _presupuestoRepository;
    // Necesitamos el repositorio de Productos para llenar el dropdown
    private readonly ProductoRepository _productoRepo = new ProductoRepository();

    public PresupuestosController()
    {
        _presupuestoRepository = new PresupuestoRepository();
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Presupuesto> presupuestos = _presupuestoRepository.GetAll();
        return View(presupuestos);
    }

    [HttpGet]
    public IActionResult Details(int id)
    {
        Presupuesto presupuesto = _presupuestoRepository.GetById(id);
        return View(presupuesto);
    }

    [HttpGet]
    public IActionResult Create()
    {
        return View(new PresupuestoViewModel());
    }

    [HttpPost]
    public IActionResult Create(PresupuestoViewModel presupuestoVM)
    {
        // 1. CHEQUEO DE SEGURIDAD DEL SERVIDOR
        if (!ModelState.IsValid)
        {
            // Si falla: Devolvemos el ViewModel con los datos y errores a la Vista
            return View(presupuestoVM);
        }

        // 2. SI ES VÁLIDO: Mapeo Manual de VM a Modelo de Dominio
        var nuevoPresupuesto = new Presupuesto
        {
            NombreDestinatario = presupuestoVM.NombreDestinatario,
            FechaCreacion = DateTime.Now
        };

        // 3. Llamada al Repositorio
        _presupuestoRepository.Create(nuevoPresupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Presupuesto presupuesto = _presupuestoRepository.GetById(id);

        // 1. Mapeo Manual de Modelo a ViewModel
        var presupuestoVM = new PresupuestoViewModel
        {
            IdPresupuesto = presupuesto.IdPresupuesto,
            NombreDestinatario = presupuesto.NombreDestinatario,
            FechaCreacion = presupuesto.FechaCreacion
        };

        return View(presupuestoVM);
    }

    [HttpPost]
    public IActionResult Edit(int id, PresupuestoViewModel presupuestoVM)
    {
        if (id != presupuestoVM.IdPresupuesto) return NotFound();

        // 1. CHEQUEO DE SEGURIDAD DEL SERVIDOR 
        if (!ModelState.IsValid)
        {
            return View(presupuestoVM);
        }

        // 2. Mapeo Manual de VM a Modelo de Dominio
        var presupuestoAEditar = new Presupuesto
        {
            IdPresupuesto = presupuestoVM.IdPresupuesto, // Necesario para el UPDATE
            NombreDestinatario = presupuestoVM.NombreDestinatario,
            FechaCreacion = presupuestoVM.FechaCreacion
        };

        // 3. Llamada al Repositorio
        _presupuestoRepository.Update(id, presupuestoAEditar);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Delete(int id)
    {
        Presupuesto presupuesto = _presupuestoRepository.GetById(id);
        return View(presupuesto);
    }

    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmado(int id)
    {
        _presupuestoRepository.DeleteById(id);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult AgregarProducto(int id)
    {
        // 1. Obtener los productos para el SelectList
        List<Producto> productos = _productoRepo.GetAll();
        // 2. Crear el ViewModel
        AgregarProductoViewModel model = new AgregarProductoViewModel
        {
            IdPresupuesto = id, // Pasamos el ID del presupuesto actual

            // 3. Crear el SelectList
            ListaProductos = new SelectList(productos, "IdProducto", "Descripcion")
        };
        return View(model);
    }

    [HttpPost]
    public IActionResult AgregarProducto(AgregarProductoViewModel model)
    {
        // 1. Chequeo de Seguridad para la Cantidad
        if (!ModelState.IsValid)
        {
            // LÓGICA CRÍTICA DE RECARGA: Si falla la validación,
            // debemos recargar el SelectList porque se pierde en el POST.
            var productos = _productoRepo.GetAll();
            model.ListaProductos = new SelectList(productos, "IdProducto", "Descripcion");
            // Devolvemos el modelo con los errores y el dropdown recargado
            return View(model);
        }
        // 2. Si es VÁLIDO: Llamamos al repositorio para guardar la relación
        _presupuestoRepository.CreateDetalle(model.IdPresupuesto, model.IdProducto, model.Cantidad);
        // 3. Redirigimos al detalle del presupuesto
        return RedirectToAction(nameof(Details), new { id = model.IdPresupuesto });
    }
}
