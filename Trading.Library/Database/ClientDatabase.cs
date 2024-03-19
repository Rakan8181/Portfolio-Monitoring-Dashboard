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
        public static readonly string databasePath = "C:\\Users\\44734\\source\\NEA\\Trading-App\\Company Database.db";
        private static readonly string _connectionString = $"Data Source={databasePath};Mode=ReadWrite;";

        public static string ConnectionString
        {
            get
            {
                return _connectionString;
            }
        }
        
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
        public static Portfolio ClientPortfolio(int clientid)
        {
            List<string> stockSymbols = new List<string>();
            List<int> quantities = new List<int>();
            List<int> convictions = new List<int>();
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select StockSymbol,Quantity,Conviction from Client_Holdings where ClientID = @ClientID";
                var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                clientIDParameter.Value = clientid;
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    stockSymbols.Add(dataReader.GetString(0));
                    quantities.Add(dataReader.GetInt32(1));
                    convictions.Add(dataReader.GetInt32(2));
                }
            }
            Portfolio portfolio = new Portfolio(stockSymbols, quantities, convictions);
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
        public static Dictionary<int, string> GetClients()
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

        public static List<string> GetStockNames(int clientID)
        {
            List<string> stockSymbols = new List<string>();
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "select s.StockName from Stocks s join Client_Holdings c on c.StockSymbol = s.StockSymbol where ClientID = @ClientID";
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
            public static void AddStock(int clientid, string stock, int quantity, int conviction) //first need to check if portfolio is full
            {
                using (SqliteConnection connection = new SqliteConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = "insert into Client_Holdings values (@ClientID,@StockSymbol,@Quantity,@Conviction)";
                    var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                    clientIDParameter.Value = clientid;
                    var stockSymbolParameter = command.Parameters.Add("@StockSymbol", SqliteType.Text);
                    stockSymbolParameter.Value = stock;
                    var quantityParameter = command.Parameters.Add("@Quantity", SqliteType.Text);
                    quantityParameter.Value = quantity;
                    var convictionParameter = command.Parameters.Add("@Conviction", SqliteType.Text);
                    convictionParameter.Value = conviction;
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
                    command.CommandText = "delete from Client_Holdings where ClientID = @ClientID and StockSymbol = @Stock";
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
            public static void UpdateShareNumber(int clientid, string stock, int shares)
            {
            
                using (SqliteConnection connection = new SqliteConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = "update Client_Holdings set Quantity = @Shares where ClientID = @ClientID and StockSymbol = @Stock";
                    var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                    clientIDParameter.Value = clientid;
                    var stockParameter = command.Parameters.Add("@Stock", SqliteType.Text);
                    stockParameter.Value = stock;
                    var sharesParameter = command.Parameters.Add("@Shares", SqliteType.Text);
                    sharesParameter.Value = shares;
                    command.ExecuteNonQuery();
                }
            }
            public static int GetShares(int clientid, string stock)
            {
                int shares = 0;
                using (SqliteConnection connection = new SqliteConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = "select Quantity from Client_Holdings where ClientID = @ClientID and StockSymbol = @Stock";
                    var clientIDParameter = command.Parameters.Add("@ClientID", SqliteType.Text);
                    clientIDParameter.Value = clientid;
                    var stockParameter = command.Parameters.Add("@Stock", SqliteType.Text);
                    stockParameter.Value = stock;
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        shares = dataReader.GetInt32(0);
                    }
                }
                return shares;
            }
    }
}
