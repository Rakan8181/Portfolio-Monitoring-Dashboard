using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Trading.Library.Clients
{
    public class ClientManager
    {
        public string _connectionString;
        public List<string> ftse100;
        public List<string> ftse100Symbols;

        // Constructor to initialize the connection string
        public ClientManager(string connectionString, List<string> ftse100, List<string> ftse100Symbols)
        {
            _connectionString = connectionString;
            this.ftse100 = ftse100;
            this.ftse100Symbols = ftse100Symbols;
        }

        public void Menu()
        {

            bool check = false;
            while (check == false)
            {
                Console.WriteLine();
                Console.WriteLine("MENU");
                
                Console.WriteLine("Enter 1 if you would like to add a client");
                Console.WriteLine("Enter 2 if you are an existing client");
                Console.WriteLine("Enter 3 if you would like to remove a client");
                Console.WriteLine("Press enter if you have finished");
                string val = Console.ReadLine();


                if (val == "1")
                {
                    Console.WriteLine("Enter your first name");
                    string firstname = Console.ReadLine();
                    Console.WriteLine("Enter your second name");
                    string secondname = Console.ReadLine();
                    int clientid = ClientDatabase.NextAvailableClientID();
                    GoldClient client = new GoldClient(clientid,firstname,secondname);
                    ClientDatabase.AddClientToDatabase(client);
                }
                else if (val == "2")
                {

                    bool check1 = false;
                    int clientid = GetClientID();
                    Dictionary<string, int> portfolio = ClientDatabase.ClientPortfolio(clientid);

                    while (check1 == false)
                    {
                        Console.WriteLine("Enter 1 if you would like to display your portfolio");
                        Console.WriteLine("Enter 2 if you would like to add a stock(s) to your portfolio");
                        Console.WriteLine("Enter 3 if you would like to remove a stock from your portfolio");
                        Console.WriteLine("Press enter if you would like to go back to the menu");

                        string val1 = Console.ReadLine();
                        if (val1 == "1")
                        {
                            Console.WriteLine(string.Join(",", portfolio.Keys.ToList()));
                            Console.WriteLine(string.Join(",", portfolio.Values.ToList()));
                            List<string> symbols = ClientDatabase.GetStockSymbol(clientid);
                            Console.WriteLine(string.Join(",", symbols));
                            //Console.WriteLine("Stock Symbols: " + symbols);
                            //Console.WriteLine("Stock Names: " + portfolio); check1 = true;
                        }
                        else if (val1 == "2")
                        {
                            bool check2 = false;
                            string stock = "";
                            while (check2 == false)
                            {
                                Console.WriteLine("Enter the stock from the FTSE 100 you would like to add to your portfolio, click enter to finish");

                                stock = Console.ReadLine();
                                if (portfolio.Keys.ToList().Contains(stock))
                                {
                                    Console.WriteLine("This stock is already in your portfolio");
                                }
                                else if (ftse100.Contains(stock)) //need to check if user already owns this stock. portfolio at top.
                                {
                                    bool check4 = false;

                                    Console.WriteLine($"Enter the number of stocks you own of {stock}");
                                    int quantity = 0;
                                    while (check4 == false)
                                    {
                                        quantity = Convert.ToInt32(Console.ReadLine());
                                        if (quantity > 0)
                                        {
                                            check4 = true;
                                        }

                                    }
                                    ClientDatabase.AddStock(clientid, stock, quantity);

                                }
                                else if (stock == "")
                                {
                                    check2 = true;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid stock entered: must be from FTSE100");
                                }

                            }


                        }
                        else if (val1 == "3")
                        {
                            bool check3 = false;
                            while (check3 == false)
                            {
                                Console.WriteLine("Enter the stock you would like to remove or click enter to exit");
                                string stock = Console.ReadLine();
                                if (portfolio.Keys.ToList().Contains(stock))
                                {
                                    ClientDatabase.ClientRemovesStock(clientid, stock);
                                }
                                else if (stock == "")
                                {
                                    check3 = true;
                                }
                                else
                                {
                                    Console.WriteLine("Invalid input. This stock is not in your portfolio");
                                }
                            }

                            // int n = ClientDatabase.FindStockNumber(clientid, stock);
                            // ClientDatabase.ClientRemovesStock(clientid, n);
                        }
                        else if (val1 == "")
                        {
                            check1 = true;
                        }
                        else
                        {
                            Console.WriteLine("Invalid input entered");
                        }
                    }


                }
                else if (val == "3")
                {
                    bool check5 = false;
                    while (check5 == false)
                    {
                        Console.WriteLine("Enter your first name");
                        string firstname = Console.ReadLine();
                        Console.WriteLine("Enter your second name");
                        string secondname = Console.ReadLine();
                        int clientid = ClientDatabase.GetClientID(firstname, secondname);
                        if (clientid == -1)
                        {
                            Console.WriteLine($"Client {firstname} {secondname} not in database");
                        }
                        else
                        {
                            ClientDatabase.RemoveClient(clientid);
                            check5 = true;
                        }
                    }
                    
                }

                else if (val == "")
                {
                    Console.WriteLine("Thank you for playing my game");
                    check = true;
                }
                else
                {
                    Console.WriteLine("Invalid input!");
                }

            }

        }
//this needs to be in program.cs for console
/*        public void AddUserStockChoices(string firstName, string secondName)
        {
            List<string> stocks = new List<string>();

            Dictionary<string, int> portfolio = UserStockChoice(client);
            stocks = portfolio.Keys.ToList();
            foreach (KeyValuePair<string, int> pair in portfolio)
            {
                ClientDatabase.AddStock(clientid, pair.Key, pair.Value);
            }
        }*/
        public Dictionary<string, int> UserStockChoice(Client client)
        {
            Dictionary<string, int> portfolio = new Dictionary<string, int>();
            bool check = false;
            while (check == false)
            {
                Console.WriteLine("Enter a stock you own from FTSE100 or press enter to indicate you have finished");
                string val = Console.ReadLine();

                if (val == "")
                {
                    check = true;
                }
                else if (portfolio.Keys.Contains(val))
                {
                    Console.WriteLine("You have already added this stock. Try again");
                }
                else if (ftse100.Contains(val))
                {
                    Console.WriteLine("Enter the number of stocks you have");
                    int n = Convert.ToInt32(Console.ReadLine());
                    portfolio.Add(val, n);
                }
                else
                {
                    Console.WriteLine("Invalid input entered!");
                }
            }
            return portfolio;
        }
        public static int GetClientID()
        {
            Dictionary<int, string> names = ClientDatabase.DisplayClients();
            Console.WriteLine("Enter the number that corresponds with your name");
            int clientid = Convert.ToInt32(Console.ReadLine());
            return clientid;
        }
    }
}
