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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Trading.GUI
{
    public partial class ClientDashboard : Form
    {
        public Client client;
        public string connectionstring = "Data Source=C:\\Users\\44734\\source\\NEA\\Company Database.db;Mode=ReadWrite;";
        // private Dictionary<string, int> _portfolio;
        public Portfolio _portfolio;
        private Database _db;

        public ClientDashboard(Client _client, Database db)
        {
            InitializeComponent();
            client = _client;
            _db = db;
            label12.Text = string.Join(",", StocksTextfileProcessor._stockSymbols);
            _portfolio = ClientDatabase.ClientPortfolio(client.clientid); // change this so it return a Portfolio class
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

            // Dictionary<string, int> portfolio = ClientDatabase.ClientPortfolio(client.clientid);
            label4.Text = string.Join(", ", _portfolio._stockSymbols);
            List<string> stockNames = ClientDatabase.GetStockSymbols(client.clientid);
            label5.Text = string.Join(", ", stockNames);
            label6.Text = client.firstName + " " + client.secondName + "'s Dashboard";
            label9.Text = string.Join(", ", _portfolio._quantity);
            label15.Text = string.Join(",", _portfolio._conviction);
            foreach (string stock in _portfolio._stockSymbols)
            {
                comboBox1.Items.Add(stock);
            }
            DisplayRiskAlgorithm();
        }
        private void DisplayRiskAlgorithm()
        {
            RiskAlgorithm riskAlgorithm = new RiskAlgorithm(_db, _portfolio);
            riskAlgorithm.ExecuteAlgorithm(); //guarantees convergence??
            List<int> chromosome = riskAlgorithm.GetBestChromosome();
            LowPortfolio.Text = string.Join(",", riskAlgorithm.ConvertChromosomeToStocks(chromosome));
            LowRisk.Text = Math.Round(riskAlgorithm.CalcFitness(chromosome),4).ToString();
            LowConviction.Text = riskAlgorithm.GetPortfolioConviction(chromosome).ToString();

            decimal lambda = 1;
            riskAlgorithm.ExecuteAlgorithm(lambda); //guarantees convergence??
            chromosome = riskAlgorithm.GetBestChromosome();
            MediumPortfolio.Text = string.Join(",", riskAlgorithm.ConvertChromosomeToStocks(chromosome));
            MediumRisk.Text = Math.Round(riskAlgorithm.CalcFitness(chromosome), 4).ToString();
            MediumConviction.Text = riskAlgorithm.GetPortfolioConviction(chromosome).ToString();
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
            Portfolio portfolio = ClientDatabase.ClientPortfolio(client.clientid);
            string stock = textBox1.Text;
            string inputShares = textBox2.Text;
            string userConviction = textBox4.Text;
            if (int.TryParse(userConviction, out int conviction))
            {
                if (conviction > 5 || conviction < 1)
                {
                    MessageBox.Show("Conviction level must be between 1 and 5");
                }
                else
                {
                    if (portfolio._stockSymbols.Contains(stock))
                    {
                        MessageBox.Show("This stock is already in your portfolio");
                    }
                    else if (!StocksTextfileProcessor._stockSymbols.Contains(stock))
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
                            ClientDatabase.AddStock(client.clientid, stock, shares, conviction);
                            MessageBox.Show($"{shares} shares in {stock} has been added to your portfolio at a conviction level of: {conviction}");
                            ResetForm();
                        }
                    }
                }

            }
            else
            {
                MessageBox.Show("Conviction level must be an integer");
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
            _portfolio = ClientDatabase.ClientPortfolio(client.clientid);
            Dashboard_Load(this, EventArgs.Empty);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string stock = textBox1.Text;
            if (_portfolio._stockSymbols.Contains(stock))
            {
                ClientDatabase.ClientRemovesStock(client.clientid, stock);
                MessageBox.Show($"Successfully removed {stock} from your portfolio");
                ResetForm();
            }
            else
            {
                MessageBox.Show("This stock is not in your portfolio");
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }
    }
}
