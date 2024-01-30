using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    public class SilverClient : Client
    {
        /*public string name { get; }
        private List<string> stocks;
        public int days;*/

        public SilverClient(int clientid, string firstName, string secondName) : base(clientid, firstName, secondName)
        {
            numStocks = 3;
            days = 3;
        }
    }
}
