public class Presupuesto
{
    private int idPresupuesto;
    private string nombreDestinatario;
    private DateTime fechaCreacion;
    private List<PresupuestoDetalle> detalle;

    public string NombreDestinatario { get => nombreDestinatario; set => nombreDestinatario = value; }
    public DateTime FechaCreacion { get => fechaCreacion; set => fechaCreacion = value; }
    public int IdPresupuesto { get => idPresupuesto; set => idPresupuesto = value; }
    public List<PresupuestoDetalle> Detalle { get => detalle; set => detalle = value; }

    public Presupuesto()
    {
        Detalle = new List<PresupuestoDetalle>();
    }

    // Metodos
    public decimal montoPresupuesto()
    {
        return Detalle.Sum(det => det.Producto.Precio * det.Cantidad);
    }

    public decimal montoPresupuestoConIva()
    {
        return Detalle.Sum(det => det.Producto.Precio * det.Cantidad) * (decimal) 1.21;
    }
    
    public int CantidadProductos()
    {
        return Detalle.Sum(det => det.Cantidad);
    }
}