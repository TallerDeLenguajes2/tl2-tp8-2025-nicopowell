using Microsoft.AspNetCore.Mvc;

public class PresupuestosController: Controller
{
    private readonly PresupuestoRepository _presupuestoRepository;

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
        return View();
    }

    [HttpPost]
    public IActionResult Create(Presupuesto presupuesto)
    {
        presupuesto.FechaCreacion = DateTime.Now;
        _presupuestoRepository.Create(presupuesto);
        return RedirectToAction("Index");
    }

    [HttpGet]
    public IActionResult Edit(int id)
    {
        Presupuesto presupuesto = _presupuestoRepository.GetById(id);
        return View(presupuesto);
    }

    [HttpPost]
    public IActionResult Edit(int id, Presupuesto presupuestoModificado)
    {
        _presupuestoRepository.Update(id, presupuestoModificado);
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
}