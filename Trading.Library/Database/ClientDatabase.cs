using Microsoft.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    public static class ClientDatabase
    {

        private static string _connectionString = "Data Source=C:\\Users\\44734\\source\\NEA\\Company Database.db;Mode=ReadWrite;";
        public static void AddClientToDatabase(Client client)
        {
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "insert into Clients values(@ClientID,@FirstName,@SecondName)";
                var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                clientIDParameter.Value = client.clientid;
                var firstNameParameter = command.Parameters.Add("@FirstName", SqliteType.Text);
                firstNameParameter.Value = client.firstName;
                var secondNameParameter = command.Parameters.Add("@SecondName", SqliteType.Text);
                secondNameParameter.Value = client.secondName;

                command.ExecuteNonQuery();

            }
        }
        public static Dictionary<string,int> ClientPortfolio(int clientid)
        {
            Dictionary<string,int> portfolio = new Dictionary<string, int>();
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select StockName,Quantity from Client_Holdings where ClientID = @ClientID";
                var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                clientIDParameter.Value = clientid;
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    portfolio.Add(dataReader.GetString(0),dataReader.GetInt16(1));
                }
            }
            return portfolio;
        }
        public static int NextAvailableClientID()
        {
            int n = 0; //should always be updated. is this good practice to initiate this variable here?
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select max(ClientID) from Clients";

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    n = dataReader.GetInt16(0);
                }
            }
            
            return n+1;
        }
        public static Dictionary<int, string> DisplayClients()
        {
            Dictionary<int, string> clients = new Dictionary<int, string>();
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select ClientID,ClientFirstName,ClientSecondName from Clients";
                var dataReader = command.ExecuteReader();
                int c = 0;
                while (dataReader.Read())
                {
                    int clientid = dataReader.GetInt16(0);
                    string firstname = dataReader.GetString(1);
                    string secondname = dataReader.GetString(2);
                    clients[clientid] = firstname + " " + secondname;
                }
            }
            List<string> names = new List<string>();
            foreach (KeyValuePair<int, string> kvp in clients)
            {
                names.Add(kvp.Value + ":" + kvp.Key);
            }
            Console.WriteLine(string.Join("\n", names));
            return clients;
        }

        public static List<string> GetStockSymbol(int clientID)
        {
            List<string> stockSymbols = new List<string>();
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select s.StockID from Client_Holdings c join Stocks s on c.StockName = s.StockName where ClientID = @ClientID";
                var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                clientIDParameter.Value = clientID;
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    string symbol = dataReader.GetString(0);
                    stockSymbols.Add(symbol);
                }
            }



            return stockSymbols;

        }
        public static void AddStock(int clientid, string stock, int quantity) //first need to check if portfolio is full
        {
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "insert into Client_Holdings values (@ClientID,@StockName,@Quantity)";
                var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                clientIDParameter.Value = clientid;
                var stockNameParameter = command.Parameters.Add("@StockName", SqliteType.Text);
                stockNameParameter.Value = stock;
                var quantityParameter = command.Parameters.Add("@Quantity", SqliteType.Text);
                quantityParameter.Value = quantity;
                command.ExecuteNonQuery();
                Console.WriteLine($"Successfully added addded {stock} to your portfolio");
            }
        }
        public static void ClientRemovesStock(int clientid, string stock)
        {
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "delete from Client_Holdings where ClientID = @ClientID and StockName = @Stock";
                var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                clientIDParameter.Value = clientid;
                var stockNameParameter = command.Parameters.Add("@Stock", SqliteType.Text);
                stockNameParameter.Value = stock;
                command.ExecuteNonQuery();
                Console.WriteLine("Successfully removed stock from portfolio");
            }
        }
        public static int GetClientID(string firstname, string secondname)
        {
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select ClientID from Clients where ClientFirstName = @FirstName and ClientSecondName = @SecondName";
                var firstNameParameter = command.Parameters.Add("@FirstName", SqliteType.Text);
                firstNameParameter.Value = firstname;
                var secondNameParameter = command.Parameters.Add("@SecondName", SqliteType.Text);
                secondNameParameter.Value = secondname;
                var dataReader = command.ExecuteReader();
                int clientid = -1;
                while (dataReader.Read())
                {
                    clientid = dataReader.GetInt32(0);
                    
                }
                return clientid; //if client not in database returns -1
            }
            
        }
        public static void RemoveClient(int clientid)
        {
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "delete from Clients where ClientID = @ClientID";
                var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                clientIDParameter.Value = clientid;;
                command.ExecuteNonQuery();
            }
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "delete from Client_Holdings where ClientID = @ClientID";
                var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                clientIDParameter.Value = clientid; ;
                command.ExecuteNonQuery();
            }


        }
    }
}
