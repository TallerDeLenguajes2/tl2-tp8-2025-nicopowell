namespace MVC.Interfaces;

public interface IPresupuestoRepository
{
    public void Create(Presupuesto presupuesto);
    public List<Presupuesto> GetAll();
    public Presupuesto GetById(int id);
    public void Update(int id, Presupuesto nuevoPresupuesto);
    public void CreateDetalle(int idPresupuesto, int idProducto, int cantidad);
    public void DeleteById(int id);
}