namespace ApiCatalogo.Services;

public class MyService : IMyService
{
    public string Greeting(string name)
    {
        return $"Bem-vindo, {name} \n\n {DateTime.UtcNow}";
    }
}