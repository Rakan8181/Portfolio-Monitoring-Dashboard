using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Microsoft.VisualBasic;
using Trading.Library;
using Trading.Library.Data;

namespace Trading.Library
{
    public class Database
    {
        public string _connectionString;
        //"Data Source=C:\\Users\\44734\\source\\NEA\\Company Database.db;Mode=ReadWrite;";
        public List<string> _stockNames;
        public List<string> _stockSymbols;
        public DateTime _currentDate;
        public Database(string connectionString)
        {
            _connectionString = connectionString;
            _currentDate = new DateTime();
        }
        public void InsertRecord(string date, string company, decimal open, decimal high, decimal low, decimal close, decimal volume)
        {
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "insert into Data (Date,Company,Open,High,Low,Close,Volume) values ((@Date),(@Company),(@Open),(@High),(@Low),(@Close),(@Volume))";
                var nameParameter1 = command.Parameters.Add("@Date", SqliteType.Text);
                nameParameter1.Value = date;
                var nameParameter2 = command.Parameters.Add("@Company", SqliteType.Text);
                nameParameter2.Value = company;
                var nameParameter3 = command.Parameters.Add("@Open", SqliteType.Real);
                nameParameter3.Value = open;
                var nameParameter4 = command.Parameters.Add("@High", SqliteType.Real);
                nameParameter4.Value = high;
                var nameParameter5 = command.Parameters.Add("@Low", SqliteType.Real);
                nameParameter5.Value = low;
                var nameParameter6 = command.Parameters.Add("@Close", SqliteType.Real);
                nameParameter6.Value = close;
                var nameParameter7 = command.Parameters.Add("@Volume", SqliteType.Real);
                nameParameter7.Value = volume;
                command.ExecuteNonQuery();
            }
        }

        public decimal GetData(DateTime _date, string company, string fieldName = "Close") //why Close? Most commonly used in stock prices
        {
            string date = _date.ToString("yyyy-MM-dd");
            bool check = true;
            List<decimal> prices = new List<decimal>();
            int index = 0;
            switch (fieldName)
            {
                case "Open":
                    index = 2;
                    break;
                case "High":
                    index = 3;
                    break;
                case "Low":
                    index = 4;
                    break;
                case "Close":
                    index = 5;
                    break;
                case "Volume":
                    index = 6;
                    break;
                case "Returns":
                    index = 7;
                    break;
                case "Returns5":
                    index = 8;
                    break;
                case "Returns20":
                    index = 9;
                    break;
                case "Returns40":
                    index = 10;
                    break;
                case "Volatility5":
                    index = 11;
                    break;
                case "Volatility20":
                    index = 12;
                    break;
                case "Volatility40":
                    index = 13;
                    break;
                default:
                    Console.WriteLine("Invalid price type entered");
                    check = false;
                    break;
            }
            if (check)
            {
                using (SqliteConnection connection = new SqliteConnection())
                {
                    connection.ConnectionString = _connectionString;
                    connection.Open();
                    SqliteCommand command = connection.CreateCommand();
                    command.CommandText = $"select {fieldName} from Data where Company = (@Company) and Date = @Date";
                    var companyParameter = command.Parameters.Add("@Company", SqliteType.Text);
                    companyParameter.Value = company;
                    var dateParameter = command.Parameters.Add("@Date", SqliteType.Text);
                    dateParameter.Value = date;
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        decimal value = dataReader.GetDecimal(0);
                        return value;
                    }
                }
            }
            return -1;
        }
        public bool CheckFieldPopulated(DateTime _date, string company, string fieldName = "Close")
        {
            string date = _date.ToString("yyyy-MM-dd");

            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.Open();
                using (SqliteCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"SELECT {fieldName} FROM Data WHERE Company = @Company AND Date = @Date";
                    command.Parameters.AddWithValue("@Company", company);
                    command.Parameters.AddWithValue("@Date", date);

                    using (SqliteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // Check if the field value is not null
                            if (!reader.IsDBNull(0))
                            {
                                // Field is not null
                                return true;
                            }
                        }
                    }
                }
            }

            // Field is null or no records found for the given date and company
            return false;
        }

        /*public bool CheckFieldPopulated(DateTime _date, string company, string field) //check if the date has already been populated into database (including prices and features)
        {//i don't think i need this method as getdata returns -1 if date not in the database!!
            string date = _date.ToString("yyyy-MM-dd");
            int count = 0;
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"select Count({field}) from Data where Date = @Date and Company = @Company";
                var dateParameter = command.Parameters.Add("@Date", SqliteType.Text);
                dateParameter.Value = date;
                var companyParameter = command.Parameters.Add("@Company", SqliteType.Text);
                companyParameter.Value = company;

                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    count = dataReader.GetInt16(0);
                    
                }
            }
            if (count == 1)
            {
                return true;
            }
            else if (count == 0)
            {
                return false;
            }
            else
            {
                throw new Exception("Multiple primary keys in database!"); //!!don't remember why or where I got this from!
            }
        }*/
        public void PopulateStocksTable(List<string> _stockNames, List<string> _stockSymbols) //one time method to populate stocks table
        {//don't know why there are 2 usings but it worked when i used it icl !!
            using (var connection = new SqliteConnection(_connectionString))
            {
                connection.Open();

                for (int i = 0; i < _stockNames.Count; i++)
                {
                    using (var command = connection.CreateCommand())
                    {
                        command.CommandText = "insert into Stocks (StockName, StockSymbol) values (@StockName, @StockSymbol)";
                        var stockNameParameter = command.Parameters.Add("@StockName", SqliteType.Text);
                        stockNameParameter.Value = _stockNames[i];
                        var stockSymbolParameter = command.Parameters.Add("@StockSymbol", SqliteType.Text);
                        stockSymbolParameter.Value = _stockSymbols[i];
                        command.ExecuteNonQuery();
                    }
                }
            }
        }
        public void UpdateValue(DateTime inputdate, string company, string fieldName, decimal value) //what if value is not decimal such as volume !!
        {
            string date = inputdate.ToString("yyyy-MM-dd");
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"update Data set {fieldName} = @Value where Company = @Company and Date = @Date";
                var dateParameter = command.Parameters.Add("@Date", SqliteType.Text);
                dateParameter.Value = date;
                var companyParameter = command.Parameters.Add("@Company", SqliteType.Text);
                companyParameter.Value = company;
                var valueParameter = command.Parameters.Add("@Value", SqliteType.Text);
                valueParameter.Value = value;
                command.ExecuteNonQuery();
            }
        }
        public void DeleteRecords()
        {
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = "delete from Data";
                command.ExecuteNonQuery();
                
            }
        }
        public List<decimal> GetAllRecords(string company, string field)
        {
            List<decimal> values = new List<decimal>(); 
            using (SqliteConnection connection = new SqliteConnection(_connectionString))
            {
                connection.ConnectionString = _connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"select {field} from Data where Company = @Company";
                var companyParameter = command.Parameters.Add("@Company", SqliteType.Text);
                companyParameter.Value = company;
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    if (!dataReader.IsDBNull(0))
                    {
                        decimal val = dataReader.GetDecimal(0);
                        values.Add(val);
                    }
                }
            }
            return values;
        }

    }
}
