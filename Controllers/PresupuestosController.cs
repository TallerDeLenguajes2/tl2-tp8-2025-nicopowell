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
    public IActionResult Details(int idPresupuesto)
    {
        Presupuesto presupuesto = _presupuestoRepository.GetById(idPresupuesto);
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
}