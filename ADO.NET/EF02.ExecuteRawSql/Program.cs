using System.Data;
using System.Data.SqlClient;
using EF02.ExecuteRawSql;
using Microsoft.Extensions.Configuration;

public  class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json")
           .Build();
        SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);
        var sql = "SELECT * FROM WALLETS";
        SqlCommand command= new SqlCommand(sql, conn);
        command.CommandType = CommandType.Text;
        conn.Open();
        SqlDataReader reader = command.ExecuteReader();
        Wallet wallet;
        while (reader.Read())
        {
            wallet = new Wallet
            {
                Id = reader.GetInt32("Id"),
                Holder = reader.GetString("Holder"),
                Balance = reader.GetDecimal("Balance"),
            };
            Console.WriteLine(wallet);
        }
        conn.Close();
        Console.ReadLine();

    }
}