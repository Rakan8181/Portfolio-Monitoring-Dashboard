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
            if (!db.CheckFieldPopulated(date,company) || date <= oldestDate)
            {
                return false; // Date is not valid or is before the oldest date in the database
            }

            DateTime currentDate = date;
            int validDaysCount = 0;

            while (currentDate > oldestDate && validDaysCount < days)
            {
                // Move to the previous day
                currentDate = currentDate.AddDays(-1);

                if (db.CheckFieldPopulated(currentDate,company))
                {
                    validDaysCount++; // Count this as a valid trading day
                }
            }

            return validDaysCount >= days;
        }

        public decimal CalculateReturn(string company, DateTime currentDate, int n)
        {
            decimal currentPrice = db.GetData(currentDate, company);
            bool check = true;
            DateTime oldDate = CalculateOldDate(n, currentDate,company);

            decimal initialPrice = db.GetData(oldDate, company);
            return (currentPrice -  initialPrice) / initialPrice;
        }
        public decimal CalculateVolatility(string company, DateTime currentDate, int n, string fieldName = "Close") //volatiltiy = STD. volatility between the past n days
        {
            //maybe to make things easier just always calculate volatility of Close price. then can remove the if statement
            //for now ill keep things as they are 
            // Validating the field name
            if (fieldName != "Open" && fieldName != "High" && fieldName != "Low" && fieldName != "Close" && fieldName != "Volume")
            {
                throw new ArgumentException("Invalid field name.");
            }

            // Calculating volatility
            List<decimal> vals = new List<decimal>();
            while (n > 0)
            {
                // Assuming GetData handles validation of field name
                if (db.CheckFieldPopulated(currentDate,company))
                {
                    vals.Add(db.GetData(currentDate, company, fieldName));
                    n--;
                }
                
                currentDate = currentDate.AddDays(-1);
            }

            decimal mean = vals.Average();
            decimal sumSquaredDifferences = vals.Sum(value => (value - mean) * (value - mean));
            decimal variance = sumSquaredDifferences / (vals.Count - 1); //sample variance
            return (decimal)Math.Sqrt((double)variance);
        }
        public decimal CalculateOscillator(string company, DateTime currentDate, string type)//if this method has the same parameters as calculate return is there a point of a new method?
        {
            
            if (type == "Price")
            {
                decimal val1 = db.GetData(currentDate, company, "Returns40");
                decimal val2 = db.GetData(currentDate, company, "Returns5");
                return (val1 - val2)/val1; //formula for oscillator: 5 and 40 are arbitary numbers which can be changed (5 days in stock market week)
            }
            else if (type == "Volatility")
            {
                decimal val1 = db.GetData(currentDate, company, "Volatility40");
                decimal val2 = db.GetData(currentDate, company, "Volatility5");
                return (val1 - val2)/val1; //formula for oscillator: 5 and 40 are arbitary numbers which can be changed (5 days in stock market week)
            }
            return -1; //!!need better defensive programming
        }
        public DateTime CalculateOldDate(int n, DateTime currentDate, string company) //assumes currentDate is in database
        {
            //use this so CalculateOsciallor works, and change CalculateReturns/Volaatiility so that it takes an oldDate and startdate because then i don't need to call db.CheckvalidDate every time. but i don't know . maybe don
            //maybe don;t need this.?? the main focus is getting this to work. becuase right now
            while (n > 0)
            {
                // Assuming GetData handles validation of field name

                DateTime tempDate = currentDate.AddDays(-1);
                if (db.CheckFieldPopulated(tempDate, company))
                {
                    n--;
                }
                currentDate = tempDate;
            }
            return currentDate;
        }

        public void PopulateDatabase()
        {
            
            List<string> features = new List<string> { "Returns", "Returns5", "Returns20", "Returns40", "Volatility5", "Volatility20", "Volatility40", "Oscillator_Price", "Oscillator_Volatility", "Oscillator_Volume" };
            foreach (string feature in features)
            {
                continue;
            }
        }
    }
}
