using Newtonsoft.Json.Linq;
using OxyPlot.Series;
using OxyPlot;
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
using OxyPlot;
using OxyPlot.Series;
using System.Linq.Expressions;
using OxyPlot.Axes;
using Trading.Library.Data;

namespace Trading.GUI
{
    public partial class StockInfo : Form
    {
        //            LatestPriceLabel.Text = LatestPrice(_currentDate, _stock).ToString();
        public string _stock;
        private string _connectionString;
        private Database _db;
        private DateTime _currentDate;
        private DateTime _oldestDate;
        //constructor is called only once, when a new instance of the form is created and is not called again unless a new instance of the form is created. 
        public StockInfo(string stock, string connectionString, DateTime currentDate, DateTime oldestDate)
        {
            _stock = stock;
            _currentDate = currentDate;
            _connectionString = connectionString;
            _oldestDate = oldestDate;

            _db = new Database(_connectionString);
            InitializeComponent();

        }
        //can be called again based on user changes to form, constructor cannot. 
        private void StockInfo_Load(object sender, EventArgs e)
        {
            label1.Text = _stock;
            InitializeDataComboBox();
            DisplayStockPriceGraph(_currentDate, _oldestDate, _stock);
        }
        private void DisplayStockPriceGraph(DateTime currentDate, DateTime oldestDate, string stock)
        {
            List<(DateTime, decimal)> stockPrices = FetchStockPriceHistory(currentDate, oldestDate, stock);

            PlotModel graphModel = new PlotModel { Title = "Stock Prices Over Time" };
            LineSeries priceLineSeries = new LineSeries { Title = stock, MarkerType = MarkerType.Circle };

            foreach ((DateTime date, decimal price) in stockPrices)
            {
                // Convert DateTime to OxyPlot's DateTimeAxis format
                double dateAxis = DateTimeAxis.ToDouble(date);
                priceLineSeries.Points.Add(new DataPoint(dateAxis, (double)price));
            }

            graphModel.Series.Add(priceLineSeries);
            graphModel.Axes.Add(new DateTimeAxis
            {
                Position = AxisPosition.Bottom,
                StringFormat = "yyyy-MM-dd",
                Title = "Date",
                IntervalType = DateTimeIntervalType.Days,
                MinorIntervalType = DateTimeIntervalType.Days,
                MajorGridlineStyle = LineStyle.Solid,
                MinorGridlineStyle = LineStyle.Dot,

            });
            Graph.Model = graphModel;
        }
        public void InitializeDataComboBox()
        {
            decimal latestPrice = LatestPrice(_currentDate, _stock);
            if (latestPrice == -1)
            {
                LatestPriceLabel.Text = ($"{_stock} not in database");
            }
            else
            {
                LatestPriceLabel.Text = latestPrice.ToString();
            }

            List<string> values = new List<string> { "Open", "High", "Low", "Close", "Volume" };
            foreach (string value in values)
            {
                comboBox1.Items.Add(value);
            }
            comboBox1.SelectedItem = "Close";

        }
        private List<(DateTime, decimal)> FetchStockPriceHistory(DateTime endDate, DateTime oldestDate, string stock)
        {
            List<(DateTime, decimal)> stockPrices = new List<(DateTime, decimal)>();
            while (endDate > oldestDate)
            {
                if (_db.CheckRecordPopulated(endDate, stock))
                {
                    decimal price = _db.GetData(endDate, stock);
                    stockPrices.Add((endDate, price));
                }
                endDate = endDate.AddDays(-1);
            }
            return stockPrices;
        }

        private decimal LatestPrice(DateTime currentDate, string company, string value = "Close")
        {
            decimal price = _db.GetData(currentDate, company, value);
            return price;
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            string value = comboBox1.Text.ToString(); //either "" or one of the values provided
            ShowValueAtDate(value, selectedDate);
        }

        private void StockAttributesComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime selectedDate = dateTimePicker1.Value;
            string value = comboBox1.Text.ToString(); //either "" or one of the values provided
            ShowValueAtDate(value, selectedDate);
        }
        public void ShowValueAtDate(string value, DateTime selectedDate)
        {
            if (!string.IsNullOrEmpty(_stock)) // Check if _company is not null or empty
            {
                if (_db.CheckRecordPopulated(selectedDate, _stock))
                {
                    string price = "An error has occured"; //is this good, should never happen, but if i did not include this then label5.Text = , would not recognize price, potentially could have done: decimal price = -1;
                    if (comboBox1.SelectedItem != null) //user has not changed comboBox1
                    {
                        decimal data = _db.GetData(selectedDate, _stock, value);
                        price = data.ToString();
                    }
                    else
                    {
                        decimal data = _db.GetData(selectedDate, _stock); //value will be automatically close
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