using Newtonsoft.Json.Linq;
using System.Configuration;
using System.Diagnostics;
using System.Net.Sockets;
using Trading.Library;
using Trading.Library.Clients;
using Trading.Library.Data;
using static Azure.Core.HttpHeader;
namespace Trading.GUI

{
    public partial class Menu : Form
    {
        private ClientManager _clientManager;
        private string _apiKey = "30CYWGJ89N4IZQZP";
        private string _apiUrl = "https://www.alphavantage.co/query?function=TIME_SERIES_DAILY&symbol=AAPL&interval=1min&apikey={apiKey}&outputsize=compact&datatype=json";
        private string _connectionString = "Data Source=C:\\Users\\44734\\source\\NEA\\Trading-App\\Company Database.db;Mode=ReadWrite;";
        private Database _db;



        public Menu(Database db)
        {
            InitializeComponent();
            InitializeClientManager();
            _db = db;
        }
        // Initialize ClientManager synchronously
        private void InitializeClientManager()
        {

            DataProcessor dataProcessor = new DataProcessor(_apiKey, ClientDatabase.ConnectionString, _db);
            var stocks = dataProcessor.ReadFile(ClientDatabase.SymbolsPath);
            var stockSymbols = dataProcessor.ReadFile(ClientDatabase.SymbolsPath);
            _clientManager = new ClientManager(ClientDatabase.ConnectionString, stocks, stockSymbols);
            foreach (var stock in stocks)
            {
                comboBox2.Items.Add(stock);
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Dictionary<int, string> clients = ClientDatabase.DisplayClients();
            List<string> names = clients.Values.ToList();
            foreach (string name in names)
            {
                comboBox1.Items.Add(name);
            }
        }

        public static string CleanName(string name)
        {
            // Remove spaces
            name = name.Replace(" ", "");

            // Convert to lowercase
            name = name.ToLower();

            // Capitalize the first letter
            if (!string.IsNullOrEmpty(name))
            {
                name = char.ToUpper(name[0]) + name.Substring(1);
            }

            return name;
        }

        public static bool CheckNameValidity(string name) //check if any numbers in user input, check no symbols, no random spaces
        {
            name = name.Replace(" ", "");
            int length = name.Length;
            if (length < 2)
            {
                return false;
            }
            else
            {
                foreach (char c in name)
                {
                    if (!char.IsLetter(c))
                    {
                        return false;
                    }
                }
                return true;
            }

        }
        private void button1_Click(object sender, EventArgs e)
        {
            List<string> names = ClientDatabase.DisplayClients().Values.ToList();
            string firstname = textBox1.Text;
            firstname = CleanName(firstname);
            string secondname = textBox2.Text;
            secondname = CleanName(secondname);
            secondname = secondname.Replace(" ", "");
            string name = firstname + " " + secondname;
            if (!CheckNameValidity(firstname))
            {
                MessageBox.Show("Invalid first name");
            }
            else if (!CheckNameValidity(secondname))
            {
                MessageBox.Show("Invalid second name");
            }
            else if (names.Contains(name))
            {
                MessageBox.Show($"{firstname} {secondname} already is a client");
            }
            else
            {
                int clientid = ClientDatabase.NextAvailableClientID();
                GoldClient client = new GoldClient(clientid, firstname, secondname);
                ClientDatabase.AddClientToDatabase(client);
                MessageBox.Show($"{firstname} {secondname} has been added to the database");
            }
            ResetForm();

        }
        //use ClientDatabase.AddStock() to add new or existing clients' stocks!!!!



        private void button2_Click(object sender, EventArgs e)
        {
            string firstname = textBox1.Text;
            string secondname = textBox2.Text;
            int clientid = ClientDatabase.GetClientID(firstname, secondname);
            GoldClient client = new GoldClient(clientid, firstname, secondname);
            ClientDashboard dashboard = new ClientDashboard(client,_db);
            dashboard.Show();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            string firstname = textBox1.Text;
            firstname = CleanName(firstname);
            string secondname = textBox2.Text;
            secondname = CleanName(secondname);
            int clientid = ClientDatabase.GetClientID(firstname, secondname);
            if (clientid == -1)
            {
                MessageBox.Show($"Client: {firstname} {secondname} not in database");
            }
            else
            {
                ClientDatabase.RemoveClient(clientid);
                MessageBox.Show($"{firstname} {secondname} has been removed from the database");
            }
            ResetForm();

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = comboBox1.SelectedItem.ToString();
            int indexSpace = name.IndexOf(" ");
            string firstname = name.Substring(0, indexSpace);
            string secondname = name.Substring(indexSpace + 1);
            int clientid = ClientDatabase.GetClientID(firstname, secondname);
            GoldClient client = new GoldClient(clientid, firstname, secondname);
            ClientDashboard dashboard = new ClientDashboard(client, _db);
            dashboard.Show();
        }
        private void ResetForm()
        {
            comboBox1.Items.Clear();
            Form1_Load(this, EventArgs.Empty);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime currentDate = new DateTime(2023, 9, 14);
            string stock = comboBox2.SelectedItem.ToString();
            StockInfo stockinfo = new StockInfo(stock, _connectionString, currentDate);
            stockinfo.Show();

        }
    }
}