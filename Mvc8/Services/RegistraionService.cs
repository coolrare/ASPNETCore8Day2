namespace Mvc8.Services;

public class RegistraionService : IRegistraionService
{
    public string RegisterUser(string name)
    {
        return $"User {name} registered successfully";
    }
}
