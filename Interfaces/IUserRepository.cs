namespace MVC.Interfaces;

public interface IUserRepository
{
    public Usuario GetUser(string username, string password);
}