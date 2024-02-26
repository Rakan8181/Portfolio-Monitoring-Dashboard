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
        private string _company;
        private DateTime _currentDate;

        public StockInfo(string stock, string connectionString, DateTime currentDate)
        {
            InitializeComponent();
            _stock = stock;
            _currentDate = currentDate;
            label1.Text = _stock;
            _connectionString = connectionString;
            db = new Database(connectionString);

        }
        private List<decimal> ShowGraph(DateTime currentDate, DateTime oldestDate, string company) //DateTime _currentDate = new DateTime(2024, 2, 6);
        {
            List<decimal> values = new List<decimal>();
            bool checkDate = true;
            while (checkDate)
            {
                if (db.CheckDatePopulated(currentDate, company))
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

        private decimal LatestPrice(DateTime currentDate, string company)
        {
            if (db.CheckDatePopulated(currentDate, company))
            {
                decimal price = db.GetData(currentDate, company);
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

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
