namespace MVC.Interfaces;

public interface IAuthenticationService
{
    public bool Login(string username, string password);
    public void Logout();
    public bool IsAuthenticated();
    public bool HasAccessLevel(string requiredAccessLevel);
}