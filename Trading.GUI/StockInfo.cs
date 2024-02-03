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
        public string stock;
        private string connectionString;

        public StockInfo(string _stock, string connectionString)
        {
            InitializeComponent();
            stock = _stock;
            label1.Text = stock;
            this.connectionString = connectionString;
        }
        private void ShowGraph()
        {
            Database db = new Database(connectionString);
            

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
