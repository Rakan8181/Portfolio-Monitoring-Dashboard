using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    public class Portfolio: IEquatable<Portfolio>
    {
        public List<string> _stockSymbols { get; }
        public List<int> _quantity { get; }
        public List<int> _conviction { get; }
        public Portfolio(List<string> stockSymbols, List<int> quantity, List<int> conviction) {
            _stockSymbols = stockSymbols;
            _quantity = quantity;
            _conviction = conviction;
        }

        public bool Equals(Portfolio? other)
        {
            if (other is null)
            {
                return false;
            }

            return _stockSymbols.SequenceEqual(other._stockSymbols) && _quantity.SequenceEqual(other._quantity) && _conviction.SequenceEqual(other._conviction);
        }
    }
}
