using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library
{
    public class Client
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


      
    }
}
    