using Newtonsoft.Json.Linq;
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

            DataProcessor dataProcessor = new DataProcessor(apiKey, apiUrl, ClientDatabase.ConnectionString);
            var stocks = dataProcessor.ReadFile(ClientDatabase.ftse100StocksPath); ;
            var stockSymbols = dataProcessor.ReadFile(ClientDatabase.ftse100StockSymbolsPath);
            clientManager = new ClientManager(ClientDatabase.ConnectionString, stocks, stockSymbols);
            Debug.WriteLine($"Connection String after ClientManager initialization: {clientManager._connectionString}");

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

            string firstname = textBox1.Text;
            firstname = CleanName(firstname);
            string secondname = textBox2.Text;
            secondname = CleanName(secondname);
            secondname = secondname.Replace(" ", "");
            if (!CheckNameValidity(firstname))
            {
                MessageBox.Show("Invalid first name");
            }
            else if (!CheckNameValidity(secondname))
            {
                MessageBox.Show("Invalid second name");
            }
            else
            {
                int clientid = ClientDatabase.NextAvailableClientID();
                GoldClient client = new GoldClient(clientid, firstname, secondname);
                ClientDatabase.AddClientToDatabase(client);
                MessageBox.Show($"{firstname} {secondname} has been added to the database");
            }

        }
        //use ClientDatabase.AddStock() to add new or existing clients' stocks!!!!
        private void Form1_Load(object sender, EventArgs e)
        {
            List<string> names = clients.Values.ToList();
            foreach (string name in names)
            {
                comboBox1.Items.Add(name);
            }
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
            Dashboard dashboard = new Dashboard(client);
            dashboard.Show();

        }
    }
}