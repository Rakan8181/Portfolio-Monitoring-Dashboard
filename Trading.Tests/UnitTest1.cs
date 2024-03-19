using ScottPlot.Finance;
using System.Text.Json;
using Trading.Library;
using Trading.Library.Data;

namespace Trading.Tests
{
    public class UnitTest1
    {
        private string connectionString = "Data Source=C:\\Users\\44734\\source\\NEA\\Trading-App\\Company Database.db; Mode=ReadWrite;";
        private Database db;
        private Features features;
        private RiskAlgorithm riskAlgorithm;
        private Portfolio portfolio;
        public UnitTest1()
        {
            // Load configuration data
            // Initialize database with the loaded connection string
            db = new Database(connectionString);
            portfolio = new Portfolio(new List<string> { "AAPL", "JPM" }, new List<int> { 1, 5 }, new List<int> { 2, 4 });
            riskAlgorithm = new RiskAlgorithm(db,portfolio);
        }




        [Fact]
        public void GetClientIDTest()
        {
            Assert.Equal(10, ClientDatabase.GetClientID("Alex", "Rathour"));
        }


        //Database class tests:
        [Fact]
        public void InsertRecordTest()
        {
            db.InsertRecord("2024-12-12", "AAPL", 1, 1, 1, 1, 1);
            DateTime dateTime = new DateTime(2024, 12, 12);
            bool check = db.CheckRecordPopulated(dateTime, "AAPL");
            Assert.True(check);
        }
        [Fact]
        public void GetDataTest() //valid GetData call
        {
            DateTime dateTime = new DateTime(2023, 10, 9);
            decimal price = 178.99m;
            decimal p = db.GetData(dateTime, "AAPL");
            Assert.Equal(p, price);
        }
        [Fact]
        public void GetDataTest2() //handles the exception by returning -1, as "Column" is not a valid column name.
        {
            DateTime dateTime = new DateTime(2023, 10, 9);
            decimal price = -1;
            decimal p = db.GetData(dateTime, "AAPL","Column");
            Assert.Equal(p, price);
        }
        [Fact]
        public void CheckRecordPopulatedTest()
        {
            DateTime dateTime = new DateTime(2023, 10, 9);
            bool check = db.CheckRecordPopulated(dateTime, "AAPL");
            Assert.True(check);
        }
        [Fact]
        public void UpdateValueTest()
        {
            DateTime dateTime = new DateTime(2023, 10, 9);
            db.UpdateValue(dateTime, "AAPL", "Open", 1);
            decimal newData = db.GetData(dateTime, "AAPL","Open");
            Assert.Equal(newData, 1);
            db.UpdateValue(dateTime, "AAPL", "Open", 178.99m);
        }
        [Fact]
        public void GetAllStocksTest()
        {
            
            List<string> allStocks = db.GetAllStocks();
            List<string> allStocksCheck = StocksTextfileProcessor._stockSymbols.ToList();
            Assert.Equal(allStocks,allStocksCheck);
        }

        //ClientDatabase class tests:
        [Fact]
        public void ClientPortfolioTest()
        {
            //ClientID = 5
            List<string> stocks = new List<string> { "AAPL", "JPM" };
            List<int> quantities = new List<int> { 1, 1 };
            List<int> convictions = new List<int> { 4,3 };
            Portfolio portfolio = new Portfolio(stocks, quantities, convictions);
            Assert.Equal(ClientDatabase.ClientPortfolio(5), portfolio);
        }
        [Fact]
        public void NextAvailableClientIDTest()
        {
            Assert.Equal(ClientDatabase.NextAvailableClientID(), 12);
        }
        [Fact]
        public void GetStocksNamesTest()
        {
            List<string> stocks = new List<string> { "Apple Inc.", "Jpmorgan Chase & Co." };
            Assert.Equal(ClientDatabase.GetStockNames(5), stocks);
        }
        [Fact]
        public void ClientDatabaseGetClientIDTest()
        {
            Assert.Equal(ClientDatabase.GetClientID("Charlie", "Shaw"), 5);
        }
        [Fact]
        public void GetSharesTest()
        {
            Assert.Equal(ClientDatabase.GetShares(5, "JPM"), 1);
        }
        [Fact]
        public void UpdateShareNumberTest()
        {
            ClientDatabase.UpdateShareNumber(5, "AAPL", 1);
            Assert.Equal(1, ClientDatabase.GetShares(5,"AAPL"));
        }

        //RiskAlgorithm class tests:
        [Fact]
        public void ConvertChromosomeToStocksTest()
        {
            List<int> chromosome = new List<int> { 1, 3, 6 };
            Assert.Equal(riskAlgorithm.ConvertChromosomeToStocks(chromosome), new List<string> { "NVDA", "META", "BRK.B" });
        }
        [Fact]
        public void ConvertStocksToChromosomeTest()
        {
            List<string> stocks = new List<string> { "NVDA", "META", "BRK.B" };
            Assert.Equal(riskAlgorithm.ConvertStocksToChromosome(stocks), new List<int> { 1,3,6 });
        }
    }
}