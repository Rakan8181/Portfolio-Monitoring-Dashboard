using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library.Data
{
    public static class StocksTextfileProcessor
    {
        public static List<string> _stockNames { get; private set; }
        public static List<string> _stockSymbols { get; private set; }
        private static string _stockNamesPath = "C:\\Users\\44734\\source\\NEA\\Trading-App\\SandP500Stocks.txt";
        private static string _stockSymbolsPath = "C:\\Users\\44734\\source\\NEA\\Trading-App\\SandP500StocksSymbols.txt";

        // Static constructor to initialize the list
        static StocksTextfileProcessor()
        {
            LoadSymbols(_stockSymbolsPath);
            LoadNames(_stockNamesPath);

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
