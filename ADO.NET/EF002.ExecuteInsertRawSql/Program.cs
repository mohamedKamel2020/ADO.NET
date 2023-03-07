using System.Data;
using System.Data.SqlClient;
using EF002.ExecuteInsertRawSql;
using Microsoft.Extensions.Configuration;

public class Program
{
    public static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();
        //read from user input 
        var walletToInsert = new Wallet
        {
            Holder = "Mohammed",
            Balance = 5000
        };
        SqlConnection conn = new SqlConnection(configuration.GetSection("constr").Value);
        var sql = "INSERT INTO WALLETS (Holder,Balance) VALUES" + $"(@Holder,@Balance);"+$"SELECT CAST(scope_identity()AS int)";

        SqlParameter holderParameter = new SqlParameter
        {
            ParameterName = "@Holder",
            SqlDbType = SqlDbType.VarChar,
            Direction = ParameterDirection.Input,
            Value = walletToInsert.Holder,
        };
        SqlParameter balanceParameter = new SqlParameter
        {
            ParameterName = "@Balance",
            SqlDbType = SqlDbType.Decimal,
            Direction = ParameterDirection.Input,
            Value = walletToInsert.Balance,
        };
        SqlCommand command=new SqlCommand(sql, conn);
        command.Parameters.Add(holderParameter);
        command.Parameters.Add(balanceParameter);
        command.CommandType= CommandType.Text;
        conn.Open();
        walletToInsert.Id = (int)command.ExecuteScalar();
        Console.WriteLine($"wallet for {walletToInsert} added ");
        //if (command.ExecuteNonQuery() > 0)
        //{
        //    Console.WriteLine($"wallet for {walletToInsert.Holder} added " );
        //}
        //else
        //{
        //    Console.WriteLine($"wallet for {walletToInsert.Holder} was not added");
        //}
        conn.Close();
    }
}