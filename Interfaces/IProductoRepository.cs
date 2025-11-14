namespace MVC.Interfaces;

public interface IProductoRepository
{
    public void Create(Producto producto);
    public void Update(int id, Producto nuevoProducto);
    public List<Producto> GetAll();
    public Producto GetById(int id);
    public void DeleteById(int id);
}