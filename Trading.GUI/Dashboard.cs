using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Trading.Library;
using Trading.Library.Data;

namespace Trading.GUI
{
    public partial class Dashboard : Form
    {
        public Client client;
        public string connectionstring = "Data Source=C:\\Users\\44734\\source\\NEA\\Company Database.db;Mode=ReadWrite;";
        public readonly string[] ftse100 = new string[] {
"3i Group PLC", "Admiral Group", "Airtel Africa PLC", "Anglo American", "Antofagasta PLC",
"Ashtead Group PLC", "Associated British Foods PLC", "AstraZeneca PLC", "Auto Trader Group PLC",
"Aviva PLC", "B&M European Value Retail SA", "BAE Systems PLC", "Barclays PLC",
"Barratt Developments PLC", "Beazley PLC", "Berkeley Group Holdings PLC", "BP PLC",
"British American Tobacco PLC", "BT Group PLC", "Bunzl PLC", "Burberry Group PLC",
"Centrica PLC", "Coca-Cola HBC AG", "Compass Group PLC", "ConvaTec Group PLC",
"Croda International PLC", "DCC PLC", "Dechra Pharmaceuticals PLC", "Diageo PLC",
"Diploma PLC", "Endeavour Mining PLC", "Entain PLC", "Experian PLC", "F&C Investment Trust PLC",
"Flutter Entertainment PLC", "Frasers Group PLC", "Fresnillo PLC", "Glencore PLC", "GSK PLC",
"Haleon PLC", "Halma PLC", "Hikma Pharmaceuticals PLC", "Howden Joinery Group PLC",
"HSBC Holdings PLC", "IMI PLC", "Imperial Brands Group", "Informa PLC",
"InterContinental Hotels Group PLC", "Intermediate Capital Group PLC",
"International Consolidated Airlines Group SA", "Intertek Group PLC", "JD Sports Fashion PLC",
"Kingfisher PLC", "Land Securities Group PLC", "Legal & General Group PLC",
"Lloyds Banking Group PLC", "London Stock Exchange Group PLC", "M&G PLC",
"Marks & Spencer Group PLC", "Melrose Industries PLC", "Mondi PLC", "National Grid PLC",
"NatWest Group PLC", "Next PLC", "Ocado Group PLC", "Pearson PLC",
"Pershing Square Holdings Ltd", "Phoenix Group Holdings PLC", "Prudential PLC",
"Reckitt Benckiser Group PLC", "RELX PLC", "Rentokil Initial PLC", "Rightmove PLC",
"Rio Tinto PLC", "Rolls Royce Holdings PLC", "RS Group PLC", "Sage Group PLC",
"Sainsbury (J) PLC", "Schroders PLC", "Scottish Mortgage Investment Trust PLC",
"Segro PLC", "Severn Trent PLC", "Shell PLC", "Smith & Nephew PLC", "Smith (DS) PLC",
"Smiths Group PLC", "Smurfit Kappa Group PLC", "Spirax-Sarco Engineering PLC",
"SSE PLC", "St James's Place PLC", "Standard Chartered PLC", "Taylor Wimpey PLC",
"Tesco PLC", "Unilever PLC", "Unite Group PLC", "United Utilities Group PLC",
"Vodafone Group PLC", "Weir Group PLC", "Whitbread PLC", "WPP PLC"};
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
            foreach (string stock in portfolio.Keys)
            {
                comboBox1.Items.Add(stock);
            }
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
            Dictionary<string, int> portfolio = ClientDatabase.ClientPortfolio(client.clientid);
            string stock = textBox1.Text;
            string inputShares = textBox2.Text;
            if (portfolio.ContainsKey(stock))
            {
                MessageBox.Show("This stock is already in your portfolio");
            }
            else if (!ftse100.Contains(stock))
            {
                MessageBox.Show("Invalid stock: not in FTSE100");
            }
            else
            {
                if (!int.TryParse(inputShares, out int shares))
                {
                    MessageBox.Show("Enter an integer number of shares");
                }
                else
                {
                    ClientDatabase.AddStock(client.clientid, stock, shares);
                    MessageBox.Show($"{shares} shares in {stock} has been added to your portfolio");
                }
            }
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e) //remove shares
        {
            string inputShares = textBox3.Text;
            ChangeShares(inputShares, true);
        }

        private void button3_Click(object sender, EventArgs e) //add shares
        {
            string inputShares = textBox3.Text;
            ChangeShares(inputShares, false);

        }
        private void ChangeShares(string inputShares, bool negative)
        {
            if (!int.TryParse(inputShares, out int sharesAdded))
            {
                MessageBox.Show("Enter an integer number of shares");
            }
            else if (sharesAdded <= 0)
            {
                MessageBox.Show("Enter a positive number of shares");
            }
            else
            {

                if (negative == true)
                {
                    sharesAdded *= -1;
                }
                string stock = comboBox1.Text;
                int currentShares = ClientDatabase.GetShares(client.clientid, stock);
                int newShares = currentShares + sharesAdded;
                if (newShares <= 0)
                {
                    MessageBox.Show($"Your new level of shares is {newShares}");
                }
                else
                {
                    ClientDatabase.UpdateShareNumber(client.clientid, stock, newShares);
                    if (negative == true)
                    {
                        MessageBox.Show($"Successfully removed {sharesAdded} shares from {stock}");
                    }
                    else
                    {
                        MessageBox.Show($"Successfully added {sharesAdded} shares to {stock}");
                    }
                }
            }
        }
    }
}
