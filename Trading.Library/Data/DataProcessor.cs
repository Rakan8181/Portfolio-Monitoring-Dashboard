using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library.Data
{
    public class DataProcessor
    {
        private readonly string apiKey;
        private readonly string connectionString;
        public string path = ClientDatabase.ftse100StockSymbolsPath;

        public DataProcessor(string _apiKey, string _connectionString)
        {
            apiKey = _apiKey;
            connectionString = _connectionString;

        }
        public async Task ProcessData()
        {
            List<string> ftse100 = ReadFile(path);
            List<string> prices = new List<string> { "open", "high", "low", "close", "volume" };
            // Your data processing logic here, similar to the existing Main method
            foreach (string company in ftse100)
            {
                string apiURL = $"https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol={company}&interval=1min&apikey={apiKey}&outputsize=compact&datatype=json"; //needs to be in config.json !!!

                using (HttpClient httpClient = new HttpClient())
                {
                    
                    HttpResponseMessage response = await httpClient.GetAsync(apiURL);

                    if (response.IsSuccessStatusCode)
                    {
                        string read = await response.Content.ReadAsStringAsync();
                        using (StringReader stringReader = new StringReader(read))
                        using (JReader jReader = new JReader(stringReader))
                        {
                            Database db = new Database(connectionString);
                            JObject jObj = JObject.Load(jReader);
                            //Console.WriteLine(jObj.ToString());
                            var metaData = jObj["Meta_Data"];
                            string outputSize = metaData["Output_Size"].ToString();
                            string timeZone = metaData["Time_Zone"].ToString();
                            Console.WriteLine(metaData);

                            var data = jObj["Time_Series_(Daily)"];
                            Dictionary<string, Dictionary<string, string>> stockInfo = data.ToObject<Dictionary<string, Dictionary<string, string>>>();
                            foreach (string date in stockInfo.Keys)
                            {
                                if (!stockInfo.ContainsKey(date))
                                {
                                    continue;
                                    //db.InsertRecord(date, company, Convert.ToDecimal(data[date]["open"]), Convert.ToDecimal(data[date]["high"]), Convert.ToDecimal(data[date]["low"]), Convert.ToDecimal(data[date]["close"]), Convert.ToDecimal(data[date]["volume"]));
                                }
                            }
                            Features feature = new Features(db);
                            //db.DeleteRecords();
                        }
                    }
                    else
                    {
                        Console.WriteLine($"HTTP Error: {response.StatusCode} - {response.ReasonPhrase}");
                    }
                }
            }
            
        }
        public List<string> ReadFile(string filePath)
        {
            List<string> stocks = new List<string>();
            // Check if the file exists
            if (File.Exists(filePath))
            {
                // Open the file with a StreamReader
                using (StreamReader reader = new StreamReader(filePath))
                {
                    // Read the line and split it by commas to get stock symbols
                    string line = reader.ReadLine();
                    if (line != null)
                    {
                        stocks.AddRange(line.Split(','));
                    }
                }
            }
            else
            {
                Console.WriteLine("File not found: " + filePath);
            }
            return stocks;
        }
    }
}
