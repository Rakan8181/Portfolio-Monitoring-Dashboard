using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    public class Features
    {
        public string _connectionString;
        //"Data Source=C:\\Users\\44734\\source\\NEA\\Company Database.db;Mode=ReadWrite;";
        private readonly Database _database;
        

        public Features(Database database)
        {
            _database = database;
        }

        public decimal CalculateReturn(string company, DateTime startDate, DateTime endDate)
        {
            decimal startPrice = _database.GetData(startDate, company);
            decimal endPrice = _database.GetData(endDate, company);

            if (startPrice != -1 && endPrice != -1)
            {
                return (endPrice / startPrice) - 1;
            }
            return 0; //need to change this to proper defensive programming
        }

        /*public decimal CalculateVolatility(string company, DateTime startDate, DateTime endDate, string columnName = "Close") //volatility = STD
        {
            if (columnName == "Open" || columnName == "High" || columnName == "Low" || columnName == "Close" || columnName == "Volume")
            {
                List<decimal> vals = new List<decimal>();
                while (startDate < endDate)
                {
                    vals.Add(_database.GetData(startDate, company, columnName)); //getData should check if column name is valid so don't need to check it here
                    do
                    {
                        startDate = startDate.AddDays(1);
                    } while (IsWeekendOrChristmas(startDate));

                }
                decimal mean = vals.Average();
                decimal sumSquaredDifferences = vals.Sum(value => (value - mean) * (value - mean));
                decimal variance = sumSquaredDifferences / (vals.Count - 1); //sample variance
                return (decimal) Math.Sqrt((double)variance);
            }
            else
            {
                return 0; //need to change this to proper defensive programming
            }
        }*/

        public decimal CalculateOscillator(string company, DateTime endDate, string columnName)//if this method has the same parameters as calculate return is there a point of a new method?
        {
            
            if (columnName == "Oscillator_Price")
            {
                decimal val1 = _database.GetData(endDate, "IBM", "Returns40");
                decimal val2 = _database.GetData(endDate, "IBM", "Returns5");
                return val1 - val2; //formula for oscillator: 5 and 40 are arbitary numbers which can be changed (5 days in stock market week)
            }
            else if (columnName == "Oscillator_Volatility")
            {
                decimal val1 = _database.GetData(endDate, "IBM", "Volatility40");
                decimal val2 = _database.GetData(endDate, "IBM", "Volatility5");
                return val1 - val2; //formula for oscillator: 5 and 40 are arbitary numbers which can be changed (5 days in stock market week)
            }
            else
            {
                return 0;
            }
        }
        public void PopulateDatabase()
        {
            
            List<string> features = new List<string> { "Returns", "Returns5", "Returns20", "Returns40", "Volatility5", "Volatility20", "Volatility40", "Oscillator_Price", "Oscillator_Volatility", "Oscillator_Volume" };
            foreach (string feature in features)
            {
                continue;
            }
        }
        public void UpdateFeature(string columnName, string company, DateTime startDate, DateTime endDate) //error handling needed
        {
            decimal result = 0;
            if (columnName == "Returns" || columnName == "Returns5" || columnName == "Returns20" || columnName == "Returns40")
            {
                result = CalculateReturn(company, startDate, endDate);
                if (result == 0)
                {
                    throw new Exception("edge case");
                }
            }
            else if (columnName == "Volatility5" || columnName == "Volatility20" || columnName == "Volatility40")
            {
                result = CalculateVolatility(company, startDate, endDate, "Close");
            }
            else if (columnName == "Oscillator_Price" || columnName == "Oscillator_Volatility" || columnName == "Oscillator_Volume")
            {
                result = CalculateOscillator(company, endDate, columnName);
            }
            string end = endDate.ToString("yyyy-MM-dd");
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = _database._connectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"update Data set ({columnName}) = @Result where Date = @Date and Company = @Company";
                var dateParameter = command.Parameters.Add("@Date", SqliteType.Text);
                dateParameter.Value = end;
                var returnParameter = command.Parameters.Add("@Result", SqliteType.Text);
                returnParameter.Value = result;
                var companyParameter = command.Parameters.Add("@Company", SqliteType.Text);
                companyParameter.Value = company;
                command.ExecuteNonQuery();
                //Console.WriteLine($"Successfully updated {columnName}");
            }


        }
        public static int CalculateVolatility(string company, DateTime startdate, DateTime endDate, string columnName)
        {
            return 1;
        }
    }
}
