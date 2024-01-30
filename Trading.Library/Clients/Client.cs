using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    public abstract class Client
    {
        public string firstName { get; }
        public string secondName { get; }
        public int clientid { get; set; }
        protected int days;
        public int numStocks;

        public Client(int _clientid, string _firstName, string _secondName)
        {
            clientid = _clientid;
            firstName = _firstName;
            secondName = _secondName;
        }


/*        public string DisplayStocks()
        {
            string result = "";
            foreach (string stock in stocks)
            {
                result += stock;
            }
            return result;
        }

        public void RemoveStock(string stockSymbol)
        {
            if (stocks.Contains(stockSymbol))
            {
                stocks.Remove(stockSymbol);
            }
            else
            {
                throw new InvalidOperationException($"Stock symbol {stockSymbol} not found in the client's stock list.");
            }
        }*/        
    }
}
    