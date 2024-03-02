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
    public partial class ClientDashboard : Form
    {
        public Client client;
        public string connectionstring = "Data Source=C:\\Users\\44734\\source\\NEA\\Company Database.db;Mode=ReadWrite;";
        List<string> stockSymbols = new List<string>
        {
            "MSFT", "AAPL", "NVDA", "AMZN", "META", "GOOGL", "GOOG", "BRK.B", "AVGO", "LLY",
            "TSLA", "JPM", "UNH", "V", "XOM", "MA", "JNJ", "PG", "HD", "MRK",
            "COST", "ABBV", "ADBE", "CRM", "AMD", "CVX", "NFLX", "WMT", "KO", "PEP",
            "ACN", "BAC", "MCD", "TMO", "CSCO", "ABT", "LIN", "CMCSA", "ORCL", "INTC",
            "VZ", "DIS", "INTU", "WFC", "AMGN", "IBM", "DHR", "NOW", "QCOM", "CAT",
            "PFE", "UNP", "SPGI", "GE", "TXN", "PM", "AMAT", "UBER", "ISRG", "RTX",
            "COP", "HON", "T", "LOW", "GS", "NKE", "BKNG", "NEE", "PLD", "BA",
            "MDT", "AXP", "ELV", "SYK", "VRTX", "TJX", "BLK", "MS", "LRCX", "SBUX",
            "C", "ETN", "PANW", "DE", "PGR", "MDLZ", "UPS", "REGN", "ADP", "CB",
            "BMY", "GILD", "ADI", "MMC", "BSX", "CVS", "LMT", "MU", "SCHW", "AMT"
        };
        public ClientDashboard(Client _client)
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
            else if (!stockSymbols.Contains(stock))
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
            ResetForm();
        }

        private void button3_Click(object sender, EventArgs e) //add shares
        {
            string inputShares = textBox3.Text;
            ChangeShares(inputShares, false);
            ResetForm();

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
                    MessageBox.Show($"You cannot remove {sharesAdded * -1} as your new level of shares would be {newShares}");
                }
                else
                {
                    ClientDatabase.UpdateShareNumber(client.clientid, stock, newShares);
                    if (negative == true)
                    {
                        MessageBox.Show($"Successfully removed {sharesAdded * -1} shares from {stock}");
                    }
                    else
                    {
                        MessageBox.Show($"Successfully added {sharesAdded} shares to {stock}");
                    }
                }
            }
        }

        private void button2_Click_1(object sender, EventArgs e) //update number of shares button 
        {
            label11.Visible = true;
            comboBox1.Visible = true;
            label10.Visible = true;
            textBox3.Visible = true;
            button3.Visible = true;
            button4.Visible = true;
        }
        private void ResetForm()
        {
            comboBox1.Items.Clear();


            Dashboard_Load(this, EventArgs.Empty);
        }

    }
}
