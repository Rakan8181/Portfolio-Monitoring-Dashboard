using System.Configuration;
using System.Diagnostics;
using Trading.Library;
using Trading.Library.Clients;
using Trading.Library.Data;
using static Azure.Core.HttpHeader;
namespace Trading.GUI
{
    public partial class Form1 : Form
    {
        private ClientManager clientManager;
        public string connectionString = "Data Source=C:\\Users\\44734\\source\\NEA\\Company Database.db;Mode=ReadWrite;";
        public static string ftse100StocksPath = "C:\\Users\\44734\\source\\NEA\\FTSE100Stocks.txt";
        public static string ftse100StockSymbolsPath = "C:\\Users\\44734\\source\\NEA\\FTSE100Symbols.txt";
        public string apiKey = "30CYWGJ89N4IZQZP";
        public string apiUrl = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=AAPL&interval=1min&apikey={apiKey}&outputsize=compact&datatype=json";

        public Dictionary<int, string> clients = ClientDatabase.DisplayClients();


        public Form1()
        {
            InitializeComponent();
            InitializeClientManager();

        }


        // Initialize ClientManager synchronously
        private void InitializeClientManager()
        {

            DataProcessor dataProcessor = new DataProcessor(apiKey, apiUrl, connectionString);
            var stocks = dataProcessor.ReadFile(ftse100StocksPath);
            var stockSymbols = dataProcessor.ReadFile(ftse100StockSymbolsPath);
            clientManager = new ClientManager(connectionString, stocks, stockSymbols);
            Debug.WriteLine($"Connection String after ClientManager initialization: {clientManager._connectionString}");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string firstname = textBox1.Text;
            string secondname = textBox2.Text;
            int clientid = ClientDatabase.NextAvailableClientID();
            GoldClient client = new GoldClient(clientid, firstname, secondname);
            ClientDatabase.AddClientToDatabase(client);
            MessageBox.Show($"{firstname} {secondname} has been added to the database");
        }
        //use ClientDatabase.AddStock() to add new or existing clients' stocks!!!!
        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> names = clients.Values.ToList();
            label3.Text = string.Join(Environment.NewLine, names);
        }



        private void button2_Click(object sender, EventArgs e)
        {
            string firstname = textBox1.Text;
            string secondname = textBox2.Text;
            int clientid = ClientDatabase.GetClientID(firstname, secondname);
            GoldClient client = new GoldClient(clientid, firstname, secondname);
            Dashboard dashboard = new Dashboard(client);
            dashboard.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string firstname = textBox1.Text;
            string secondname = textBox2.Text;
            int clientid = ClientDatabase.GetClientID(firstname, secondname);
            if (clientid == -1)
            {
                MessageBox.Show($"Client {firstname} {secondname} not in database");
            }
            else
            {
                ClientDatabase.RemoveClient(clientid);
                MessageBox.Show($"{firstname} {secondname} has been removed from the database");
            }

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }
    }
}