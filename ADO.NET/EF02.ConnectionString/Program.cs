using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
      var configuration =new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();
        Console.WriteLine(configuration.GetSection("constr").Value);
        Console.ReadLine();

    }
}