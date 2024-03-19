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
        private DateTime _oldestDate;
        private Database _db;
        
        public Features(DateTime oldestDate, Database db)
        {
            _oldestDate = oldestDate;
            _db = db;
        }
        public bool CheckValidReturns(DateTime date, string company, int days)
        {
            if (!_db.CheckRecordPopulated(date,company) || date <= _oldestDate)
            {
                return false; // Date is not valid or is before the oldest date in the database
            }

            DateTime currentDate = date;
            int validDaysCount = 0;

            while (currentDate > _oldestDate && validDaysCount < days)
            {
                // Move to the previous day
                currentDate = currentDate.AddDays(-1);

                if (_db.CheckRecordPopulated(currentDate,company))
                {
                    validDaysCount++; // Count this as a valid trading day
                }
            }

            return validDaysCount >= days;
        }

        public decimal CalculateReturn(string company, DateTime currentDate, int days)
        {
            decimal currentPrice = _db.GetData(currentDate, company);
            DateTime oldDate = CalculateOldDate(days, currentDate,company);
            decimal initialPrice = _db.GetData(oldDate, company);
            return (currentPrice -  initialPrice) / initialPrice;
        }
        public decimal CalculateVolatility(string company, DateTime currentDate, int days, string fieldName = "Close") //volatiltiy = STD. volatility between the past n days
        {
            //maybe to make things easier just always calculate volatility of Close price. then can remove the if statement
            //for now ill keep things as they are 
            // Validating the field name
            

            // Calculating volatility
            List<decimal> vals = new List<decimal>();
            while (days > 0)
            {
                // Assuming GetData handles validation of field name
                if (_db.CheckRecordPopulated(currentDate,company))
                {
                    vals.Add(_db.GetData(currentDate, company, fieldName));
                    days--;
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
                decimal val1 = _db.GetData(currentDate, company, "Returns40");
                decimal val2 = _db.GetData(currentDate, company, "Returns5");
                return (val1 - val2)/val1; //formula for oscillator: 5 and 40 are arbitary numbers which can be changed (5 days in stock market week)
            }
            else if (type == "Volatility")
            {
                decimal val1 = _db.GetData(currentDate, company, "Volatility40");
                decimal val2 = _db.GetData(currentDate, company, "Volatility5");
                return (val1 - val2)/val1; //formula for oscillator: 5 and 40 are arbitary numbers which can be changed (5 days in stock market week)
            }
            return -1; //!!need better defensive programming
        }
        public DateTime CalculateOldDate(int days, DateTime currentDate, string company) //assumes currentDate is in database
        {
            //use this so CalculateOsciallor works, and change CalculateReturns/Volaatiility so that it takes an oldDate and startdate because then i don't need to call db.CheckvalidDate every time. but i don't know . maybe don
            //maybe don;t need this.?? the main focus is getting this to work. becuase right now
            while (days > 0)
            {
                // Assuming GetData handles validation of field name

                DateTime tempDate = currentDate.AddDays(-1);
                if (_db.CheckRecordPopulated(tempDate, company))
                {
                    days--;
                }
                currentDate = tempDate;
            }
            return currentDate;
        }
    }
}
