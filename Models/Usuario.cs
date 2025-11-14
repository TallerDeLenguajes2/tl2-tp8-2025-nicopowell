public class Usuario
{
    public int IdUsuario { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string Nombre { get; set; }
    public Roles Rol { get; set; }

}

public enum Roles
{
    Administrador,
    Cliente
}