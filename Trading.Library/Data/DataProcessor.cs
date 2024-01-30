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
        private readonly string _apiKey;
        private readonly string _apiUrl;
        private readonly string _connectionString;


        public DataProcessor(string apiKey, string apiUrl, string connectionString)
        {
            _apiKey = apiKey;
            _apiUrl = apiUrl;
            _connectionString = connectionString;

        }

        public async Task ProcessData()
        {
            // Your data processing logic here, similar to the existing Main method
            using (HttpClient httpClient = new HttpClient())
            {
                HttpResponseMessage response = await httpClient.GetAsync(_apiUrl);

                if (response.IsSuccessStatusCode)
                {
                    string read = await response.Content.ReadAsStringAsync();
                    JObject jObj = JObject.Parse(read);

                    // Your existing data processing logic
                    // ...

                    // Database interactions
                    Database db = new Database(_connectionString);
                    Features feature = new Features(db);
                    db.DeleteRecords();
                    

                    // Additional database interactions based on processed data
                    // ...
                }
                else
                {
                    Console.WriteLine($"HTTP Error: {response.StatusCode} - {response.ReasonPhrase}");
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
