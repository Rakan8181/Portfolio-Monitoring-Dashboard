using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    public class Individual
    {
        public List<int> _chromosome { get; set; }
        public decimal _fitness { get; set; }

        public Individual(List<int> chromsome, decimal fitness)
        {
            _chromosome = chromsome;
            _fitness = fitness;
        }
    }
}
