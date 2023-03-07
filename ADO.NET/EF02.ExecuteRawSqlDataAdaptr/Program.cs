using System.Data;
using System.Data.SqlClient;
using EF02.ExecuteRawSqlDataAdaptr;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
         .AddJsonFile("appsettings.json")
         .Build();
        SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);
        var sql = "SELECT * FROM WALLETS";
        conn.Open();
        SqlDataAdapter adapter =new SqlDataAdapter(sql,conn);
        DataTable dt=new DataTable();
        adapter.Fill(dt);
        conn.Close();
        foreach(DataRow dr in dt.Rows)
        {
            var wallet = new Wallet
            {
                Id = Convert.ToInt32(dr["id"]),
                Holder = Convert.ToString(dr["Holder"]),
                Balance = Convert.ToDecimal(dr["Id"])
            };
            Console.WriteLine(wallet);

        }
        Console.ReadKey();
    }
}