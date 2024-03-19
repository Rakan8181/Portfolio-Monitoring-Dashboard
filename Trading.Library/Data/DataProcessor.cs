using Microsoft.Data.Sqlite;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Trading.Library.Data
{
    public class DataProcessor
    {
        private readonly string _apiKey;
        private Database _db;
        private DateTime _oldestDate;
        private DateTime _newestDate;
        public DataProcessor(string apiKey, Database db)
        {

            _apiKey = apiKey;
            _db = db;
            _oldestDate = GetOldestDate();
            _newestDate = GetNewestDate();
        }
        public DateTime GetOldestDate() //example of an aggregate sql query. 
        {
            string date = "Date not found";
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = ClientDatabase.ConnectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"Select Date FROM Data ORDER BY Date ASC LIMIT 1";
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    date = dataReader.GetString(0);
                }
            }
            return ConvertStringToDateTime(date);
        }
        public DateTime GetNewestDate() //example of an aggregate sql query. 
        {
            string date = "Date not found";
            using (SqliteConnection connection = new SqliteConnection())
            {
                connection.ConnectionString = ClientDatabase.ConnectionString;
                connection.Open();
                SqliteCommand command = connection.CreateCommand();
                command.CommandText = $"Select Date FROM Data ORDER BY Date DESC LIMIT 1";
                var dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    date = dataReader.GetString(0);
                }
            }
            return ConvertStringToDateTime(date);
        }
        public async Task ProcessData(List<string> stocks)
        {
            //List<string> ftse100 = ReadFile(StockSymbolsPath);
            foreach (string stock in stocks)
            {
                string apiURL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={stock}&interval=1min&apikey={_apiKey}&outputsize=compact&datatype=json"; //needs to be in config.json !!!

                using (HttpClient httpClient = new HttpClient())
                {
                    HttpResponseMessage response = await httpClient.GetAsync(apiURL);

                    if (response.IsSuccessStatusCode)
                    {
                        string read = await response.Content.ReadAsStringAsync();
                        using (StringReader stringReader = new StringReader(read))
                        using (JReader jReader = new JReader(stringReader))
                        {
                            Database db = new Database(ClientDatabase.ConnectionString);
                            JObject jObj = JObject.Load(jReader);
                            //Console.WriteLine(jObj.ToString());
                            JObject metaData = (JObject)jObj["Meta_Data"];
                            string outputSize = metaData["Output_Size"].ToString(); //need to check if output size exists
                            string timeZone = metaData["Time_Zone"].ToString();                            
                            JObject data = (JObject)jObj["Time_Series_(Daily)"];
                            Dictionary<string, Dictionary<string, string>> stockInfo = data.ToObject<Dictionary<string, Dictionary<string, string>>>();
                            foreach (string date in stockInfo.Keys.Reverse()) //I want the oldest day first; if I start with the newest day, I cannot calc returns5 for example, because I haveen't yet added the price 5 days ago to the database. 
                            { // as a result I should be able to calculate features in the same foreach loop.
                                DateTime currentDate = ConvertStringToDateTime(date);
                                if (!_db.CheckRecordPopulated(currentDate, stock))
                                {
                                    _db.InsertRecord(date, stock, decimal.Parse(stockInfo[date]["open"]), decimal.Parse(stockInfo[date]["high"]), decimal.Parse(stockInfo[date]["low"]), decimal.Parse(stockInfo[date]["close"]), decimal.Parse(stockInfo[date]["volume"]));
                                }
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }

        }

        public void PopulateFeatures(List<string> stocks)
        {
            Features feature = new Features(_oldestDate, _db);
            foreach (string stock in stocks)
            {
                bool checkDateRange = true;
                DateTime currentDate = _oldestDate;
                while (checkDateRange)
                {
                    if (_db.CheckRecordPopulated(currentDate, stock)) //checks if date in database, only days where the stock market is open are on database
                    {
                        Dictionary<string, int> returns = new Dictionary<string, int>() { { "Returns", 1 }, { "Returns5", 5 }, { "Returns20", 20 }, { "Returns40", 40 } };
                        foreach (string field in returns.Keys)
                        {
                            int n = returns[field];
                            if (feature.CheckValidReturns(currentDate, stock, n))//nice gpt function
                            {
                                decimal value = feature.CalculateReturn(stock, currentDate, n);
                                if (!_db.CheckRecordPopulated(currentDate, stock, field))
                                {
                                    _db.UpdateValue(currentDate, stock, field, value);
                                }
                            }
                        }
                        Dictionary<string, int> volatilities = new Dictionary<string, int>() { { "Volatility5", 5 }, { "Volatility20", 20 }, { "Volatility40", 40 } };
                        foreach (KeyValuePair<string, int> keyValuePair in volatilities)
                        {
                            string field = keyValuePair.Key;
                            int n = keyValuePair.Value;
                            if (feature.CheckValidReturns(currentDate, stock, n))
                            {
                                decimal value = feature.CalculateVolatility(stock, currentDate, n);
                                if (!_db.CheckRecordPopulated(currentDate, stock, field))
                                {
                                    _db.UpdateValue(currentDate, stock, field, value);
                                }
                            }
                        }
                        if (_db.CheckRecordPopulated(currentDate, stock, "Returns40")) //returns5 will have to be populated if returns40 is populated
                        {
                            decimal oscillatorPrice = feature.CalculateOscillator(stock, currentDate, "Price");
                            decimal oscillatorVolatility = feature.CalculateOscillator(stock, currentDate, "Volatility");
                            _db.UpdateValue(currentDate, stock, "Oscillator_Price", oscillatorPrice);
                            _db.UpdateValue(currentDate, stock, "Oscillator_Volatility", oscillatorVolatility);
                        }

                    }
                    currentDate = currentDate.AddDays(1);
                    if (currentDate > _newestDate) //dates newer than newest date in database --> leave the while loop
                    {
                        checkDateRange = false;
                    }
                }
            }
            



        }


        public DateTime ConvertStringToDateTime(string inputDate)
        {
            string dateFormat = "yyyy-MM-dd";
            DateTime date = DateTime.ParseExact(inputDate, dateFormat, CultureInfo.InvariantCulture);
            return date;
        }
    }
}