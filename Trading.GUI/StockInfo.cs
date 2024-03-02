using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows.Forms;
using Trading.Library;

namespace Trading.GUI
{
    public partial class StockInfo : Form
    {
        public string _stock;
        private string _connectionString;
        private Database db;
        private DateTime _currentDate;

        public StockInfo(string stock, string connectionString, DateTime currentDate)
        {
            InitializeComponent();
            InitializeDataComboBox();
            _stock = stock;
            _currentDate = currentDate;
            label1.Text = _stock;
            _connectionString = connectionString;
            db = new Database(connectionString);


        }
        public void InitializeDataComboBox()
        {
            List<string> values = new List<string> { "Open", "High", "Low", "Close", "Volume" };
            foreach (string value in values)
            {
                comboBox1.Items.Add(value);
            }
            comboBox1.SelectedItem = "Close";

        }
        private List<decimal> ShowGraph(DateTime currentDate, DateTime oldestDate, string company) //DateTime _currentDate = new DateTime(2024, 2, 6);
        {
            List<decimal> values = new List<decimal>();
            bool checkDate = true;
            while (checkDate)
            {
                if (db.CheckFieldPopulated(currentDate, company))
                {
                    decimal price = db.GetData(currentDate, company);
                    values.Add(price);
                }
                if (currentDate > oldestDate)
                {
                    currentDate = currentDate.AddDays(-1);
                }
            }
            return values;
        }

        private decimal LatestPrice(DateTime currentDate, string company, string value = "Close")
        {

            if (db.CheckFieldPopulated(currentDate, company))
            {   
                decimal price = db.GetData(currentDate, company, value);
                return price;
            }
            else
            {
                throw new Exception("Provided date has not data");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label3.Text = LatestPrice(_currentDate, _stock).ToString();
            Graph graph = new Graph();
            graph.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            string value = comboBox1.Text.ToString(); //either "" or one of the values provided
            ShowValueAtDate(value, selectedDate);   
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            string value = comboBox1.Text.ToString(); //either "" or one of the values provided
            ShowValueAtDate(value, selectedDate);
        }
        public void ShowValueAtDate(string value, DateTime selectedDate)
        {
            if (!string.IsNullOrEmpty(_stock)) // Check if _company is not null or empty
            {
                if (db.CheckFieldPopulated(selectedDate, _stock))
                {
                    string price = "An error has occured"; //is this good, should never happen, but if i did not include this then label5.Text = , would not recognize price, potentially could have done: decimal price = -1;
                    if (comboBox1.SelectedItem != null) //user has not changed comboBox1
                    {
                        decimal data = db.GetData(selectedDate, _stock, value);
                        price = data.ToString();
                    }
                    else
                    {
                        decimal data = db.GetData(selectedDate, _stock); //value will be automatically close
                        price = data.ToString();
                    }
                    label5.Text = price.ToString();
                }
                else
                {
                    label5.Text = $"No data for this particular date: {selectedDate.ToString()}";
                }
            }
            else
            {
                label5.Text = "Make your choice!"; // Provide appropriate message if _company is null or empty
            }
        }
    }
}
