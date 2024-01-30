using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trading.Library;

namespace Trading.GUI
{
    public partial class Dashboard : Form
    {
        public Client client;
        public string connectionstring = "Data Source=C:\\Users\\44734\\source\\NEA\\Company Database.db;Mode=ReadWrite;";


        public Dashboard(Client _client)
        {
            InitializeComponent();
            client = _client;
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {
            Dictionary<string, int> portfolio = ClientDatabase.ClientPortfolio(client.clientid);
            label4.Text = string.Join(",", portfolio.Keys.ToList());
            List<string> symbols = ClientDatabase.GetStockSymbol(client.clientid);
            label5.Text = string.Join(",", symbols);
            label6.Text = client.firstName + " " + client.secondName + "'s Dashboard";
            label9.Text = string.Join(",", portfolio.Values.ToList());
            progressBar1.Value = symbols.Count() * 10;
        }
        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string val = textBox1.Text; //validation!!!
            if (int.TryParse(textBox2.Text, out int quantity))
            {
                ClientDatabase.AddStock(client.clientid, val, quantity);
                MessageBox.Show($"{quantity} shares in {val} has been added to your portfolio");
            }
            else
            {
                MessageBox.Show("Please enter a valid quantity.");
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }
    }
}
