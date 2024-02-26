using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    public class Features
    {
        //connectionString : "Data Source=C:\\Users\\44734\\source\\NEA\\Company Database.db;Mode=ReadWrite;";
        public DateTime oldestDate { get; }
        public Database db { get; }
        public string company { get; set; } //why set I have no clue !!!
        
        public Features(DateTime _oldestDate, Database _db)
        {
            oldestDate = _oldestDate;
            db = _db;
        }
        public bool CheckValidReturns(DateTime date, string company, int days)
        {
            if (!db.CheckDatePopulated(date,company) || date <= oldestDate)
            {
                return false; // Date is not valid or is before the oldest date in the database
            }

            DateTime currentDate = date;
            int validDaysCount = 0;

            while (currentDate > oldestDate && validDaysCount < days)
            {
                // Move to the previous day
                currentDate = currentDate.AddDays(-1);

                if (db.CheckDatePopulated(currentDate,company))
                {
                    validDaysCount++; // Count this as a valid trading day
                }
            }

            return validDaysCount >= days;
        }

        public decimal CalculateReturn(string company, DateTime date, int days)
        {
            decimal currentPrice = db.GetData(date, company);
            bool check = true;
            DateTime olderDate = date.AddDays(days * -1);
            for (int i = 0; i < days;)
            {
                date = date.AddDays(-1); // Always go back one day

                // Check if the date is populated in the database
                if (db.CheckDatePopulated(date, company)) //or db.GetData(date,company) == -1 
                {
                    i++; // Only increment the counter if the date is populated
                }

                // Check if the targetDate has gone beyond the oldest date we are willing to check
            }
            decimal initialPrice = db.GetData(date, company);
            return (currentPrice -  initialPrice) / initialPrice;
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
                decimal val1 = db.GetData(endDate, "IBM", "Returns40");
                decimal val2 = db.GetData(endDate, "IBM", "Returns5");
                return val1 - val2; //formula for oscillator: 5 and 40 are arbitary numbers which can be changed (5 days in stock market week)
            }
            else if (columnName == "Oscillator_Volatility")
            {
                decimal val1 = db.GetData(endDate, "IBM", "Volatility40");
                decimal val2 = db.GetData(endDate, "IBM", "Volatility5");
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
                result = CalculateReturn(company, startDate, 5);
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
                connection.ConnectionString = db._connectionString;
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
