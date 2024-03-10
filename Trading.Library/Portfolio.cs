using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    public class Portfolio
    {
        public List<string> _stockSymbols { get; private set; }
        public List<int> _quantity { get; private set; }
        public List<int> _conviction { get; private set; }
        public Portfolio(List<string> stockSymbols, List<int> quantity, List<int> conviction) {
            _stockSymbols = stockSymbols;
            _quantity = quantity;
            _conviction = conviction;
        }

    }
}
