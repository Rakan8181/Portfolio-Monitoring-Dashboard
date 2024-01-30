using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Data.Sqlite;
using Trading.Library;

namespace Trading.Library
{
    public class Database
    {
        public string _connectionString;
        //"Data Source=C:\\Users\\44734\\source\\NEA\\Company Database.db;Mode=ReadWrite;";
        public Database(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void InsertRecord(string date, string company, decimal open, decimal high, decimal low, decimal close, decimal volume)
        {
            using (SqliteConnection connection = new SqliteConnection())
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

        public decimal GetData(DateTime _date, string company, string columnName = "Close") //why Close? Most commonly used in stock prices
        {
            string date = _date.ToString("yyyy-MM-dd");
            bool check = true;
            List<decimal> prices = new List<decimal>();
            int index = 0;
            switch (columnName)
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
                    command.CommandText = "select * from Data where Company = (@Company)";
                    var companyParameter = command.Parameters.Add("@Company", SqliteType.Text);
                    companyParameter.Value = company;
                    var dataReader = command.ExecuteReader();
                    while (dataReader.Read())
                    {
                        string dateRecord = dataReader.GetString(0);
                        if (dateRecord == date)
                        {
                            string val = dataReader.GetString(index);
                            decimal price = decimal.Parse(val);
                            //Console.WriteLine("Successfully got price");
                            return price;
                        }
                    }
                }
            }
            return -1;
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
    }
}
