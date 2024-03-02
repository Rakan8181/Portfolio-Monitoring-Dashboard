using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    internal class RiskAlgorithm
    {

        List<decimal> _list1;
        List<decimal> _list2;
        public RiskAlgorithm(List<decimal> list1, List<decimal> list2)
        {
            _list1 = list1;
            _list2 = list2;
        }

        public decimal CalcCovariance()
        {
            if (_list1.Count != _list2.Count)
                throw new InvalidOperationException("Lists must be of the same length.");

            int n = _list1.Count;
            decimal mean1 = _list1.Average();
            decimal mean2 = _list2.Average();

            decimal covariance = 0m;
            for (int i = 0; i < n; i++)
            {
                covariance += (_list1[i] - mean1) * (_list2[i] - mean2);
            }

            return covariance / (n - 1); // Using sample covariance formula
        }
    }
}
