using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library.Data
{
    public class StocksTextfileProcessor
    {
        public static List<string> _stockNames { get; private set; }
        public static List<string> _stockSymbols { get; private set; }

        // Static constructor to initialize the list
        static StocksTextfileProcessor()
        {
            LoadSymbols("C:\\Users\\44734\\source\\NEA\\Trading-App\\SandP500StocksSymbols.txt");
            LoadNames("C:\\Users\\44734\\source\\NEA\\Trading-App\\SandP500Stocks.txt");

        }

        // Method to load symbols from a file
        private static void LoadSymbols(string filePath)
        {
            try
            {
                // Read all lines from the file and convert to a list
                _stockSymbols = new List<string>(File.ReadAllLines(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading stock symbols: {ex.Message}");
                // Initialize with an empty list to avoid null reference issues
                _stockSymbols = new List<string>();
            }
        }
        private static void LoadNames(string filePath)
        {
            try
            {
                // Read all lines from the file and convert to a list
                _stockNames = new List<string>(File.ReadAllLines(filePath));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading stock symbols: {ex.Message}");
                // Initialize with an empty list to avoid null reference issues
                _stockNames = new List<string>();
            }
        }
    }
}
