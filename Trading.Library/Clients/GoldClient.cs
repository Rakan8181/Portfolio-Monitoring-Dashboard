using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    public class GoldClient : Client
    {
        /*public string name { get; }
        private List<string> stocks;
        public int days;*/

        public GoldClient(int clientid, string firstName, string secondName) : base(clientid, firstName, secondName)
        {
            numStocks = 10;
            days = 10;
        }

        

        
    }
}
