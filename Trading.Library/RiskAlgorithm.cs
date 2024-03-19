using SQLitePCL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trading.Library.Data;

namespace Trading.Library
{
    public class RiskAlgorithm
    {
        private Database _db;
        private decimal[,] _covarianceMatrix = new decimal[19,19];
        private int[] _convictionMatrix = new int[19];
        public List<Individual> _population { get; private set; }
        public Portfolio _portfolio { get; private set; }
        public RiskAlgorithm(Database db, Portfolio portfolio)
        {
            _db = db;
            _portfolio = portfolio;
            PopulateCovarianceMatrix();
            PopulateConvictionMatrix();
        }

        public void UpdatePopulation(List<Individual> newPopulation)
        {
            _population = newPopulation;
        }
        public void ConsoleDisplayPopulation()
        {
            foreach (Individual population in _population)
            {
                List<string> stocks = ConvertChromosomeToStocks(population._chromosome);
                stocks.Sort();
                Console.WriteLine(string.Join(",", stocks));
            }
            Console.WriteLine();
        }
        public List<int> GetBestChromosome()
        {
            decimal fitness = -1;
            List<int> bestChromosome = new List<int>();
            foreach (Individual individual in _population)
            {
                if (fitness == -1)
                {
                    fitness = individual._fitness;
                    bestChromosome = individual._chromosome;
                }
                else
                {
                    if (individual._fitness < fitness)
                    {
                        fitness = individual._fitness;
                        bestChromosome = individual._chromosome;
                    }
                }
            }
            return bestChromosome;
        }

        public decimal CalcCovariance(string stock1, string stock2) //only used to populate covariance matrix
        {//db.GetAllRecords takes time, so populate covariance matrix once, and refer to the matrix, which is much faster 
            List<decimal> returns1 = _db.GetAllRecords(stock1, "Returns");
            List<decimal> returns2 = _db.GetAllRecords(stock2, "Returns");
            decimal mean1 = returns1.Average();
            decimal mean2 = returns2.Average();
            decimal covariance = 0;
            if (returns1.Count != returns2.Count)
            {
                throw new Exception("Length of the list of returns of the 2 stocks are not the same");
            }
            int n = returns1.Count;
            for (int i = 0; i < n; i++)
            {
                covariance += (returns1[i] - mean1) * (returns1[i] - mean2);
            }

            return covariance / (n - 1); // Using sample covariance formula
        }
/*        public decimal CalcStdDev(string stock)
        {
            DateTime latestDate = _db.GetMostRecentDate();
            decimal variance = _db.GetData(latestDate, stock,"Volatility");
            decimal stdDev = (decimal)Math.Sqrt((double)variance);
            return stdDev;
        }*/


        public List<int> GenerateRandomCombination(int n, int max)
        {
            Random rng = new Random();
            return Enumerable.Range(0, max).OrderBy(x => rng.Next()).Take(n).OrderBy(x => x).ToList();
        }

        public List<List<int>> GenerateRandomPortfolios()
        {
            List<List<int>> population = new List<List<int>>();
            for (int i = 0; i < 100; i++)
            {
                bool check = false;
                while (check == false)
                {
                    List<int> indexes = GenerateRandomCombination(5, 19);
                    List<int> chromosome = new List<int>(new int[5]);
                    for (int y = 0; y < chromosome.Count; y ++)
                    {
                        chromosome[y] = indexes[y];
                    }

                    if (!population.Contains(chromosome))
                    {
                        population.Add(chromosome);
                        check = true;
                    }
                }
            }
            return population;
        }
        
        public void PopulateCovarianceMatrix() //_covarianceMatrix is a 2D array as it is fixed length n * n. 
        {
            for (int i = 0; i < _covarianceMatrix.GetLength(0); i++) { //although getLength(0) (num of rows) always equals getLength(1) (num of columns as it is populated as n by n)
                for (int y = 0; y < _covarianceMatrix.GetLength(1); y++)
                {
                    
                    string stock1 = StocksTextfileProcessor._stockSymbols[i];
                    string stock2 = StocksTextfileProcessor._stockSymbols[y];
                    decimal covariance = CalcCovariance(stock1, stock2);
                    _covarianceMatrix[i, y] = covariance;
                }
            }
        }
        public void PopulateConvictionMatrix() //first with rng, then ask user to populate, if no value provided then rng. 
        {
            for (int i = 0; i < _convictionMatrix.GetLength(0); i++)
            {
                string stock = StocksTextfileProcessor._stockSymbols[i];
                int index = _portfolio._stockSymbols.IndexOf(stock);
                int conviction;
                if (_portfolio._stockSymbols.Contains(stock))
                {
                    conviction = _portfolio._conviction[index];
                    
                }
                else
                {
                    Random rng = new Random();
                    conviction = rng.Next(6); //random num 1-5.
                }
                _convictionMatrix[i] = conviction;
            }
        }
        public List<string> ConvertChromosomeToStocks(List<int> chromosome)
        {
            List<string> stocks = new List<string>();
            foreach (int i in chromosome)
            {
                stocks.Add(StocksTextfileProcessor._stockSymbols[i]);
            }
            return stocks;
        }
        public List<int> ConvertStocksToChromosome(List<string> stocks)
        {
            List<int> chromosome = new List<int>();

            foreach (string stock in stocks)
            {
                int index = StocksTextfileProcessor._stockSymbols.IndexOf(stock);
                if (index != -1)
                {
                    chromosome.Add(index);
                }
                else
                {
                    // Handle case where stock symbol is not found
                    // You may choose to throw an exception or handle it according to your requirement
                    // For now, let's print a warning message
                    Console.WriteLine($"Stock symbol '{stock}' not found.");
                }
            }
            return chromosome;
        }
        public decimal CalcFitness(List<int> chromosome, decimal lambda = 0)
        {
            decimal fitness = 0;
            foreach (int index1 in chromosome)
            {
                foreach (int index2 in chromosome)
                {
                    fitness += _covarianceMatrix[index1,index2] - (lambda*GetPortfolioConviction(chromosome));
                }
            }
            return fitness;
        }
        public decimal GetPortfolioConviction(List<int> chromosome)
        {
            decimal portfolioConviction = 0;
            foreach (int index in chromosome)
            {
                portfolioConviction += _convictionMatrix[index];
            }
            return portfolioConviction / chromosome.Count();
        }
        public List<int> Crossover(List<int> chromosome1, List<int> chromosome2)
        {
            List<int> child = new List<int>(5); //!! array better?
            for (int i = 0; i < chromosome1.Count; i ++) //chromosome1.Count = chromosome2.Count..30+.303
            {
                if (chromosome2.Contains(chromosome1[i])) //prioritise adding repeated stocks to child, which is why this has its own for loop
                {
                    child.Add(chromosome1[i]);
                }
            }
            for (int i = 0; i < chromosome1.Count; i++)
            {
                Random random = new Random();
                int bit = random.Next(2);
                if ((bit == 1) && !(child.Contains(chromosome1[i])))
                {
                    child.Add(chromosome1[i]); //I don't stop adding once length 5 has been reached, as this would favour stocks which appear early in list. 
                }
                bit = random.Next(2);
                if ((bit == 1) && !(child.Contains(chromosome2[i])))
                {
                    child.Add(chromosome2[i]);
                }
            }

            return child;
        }
        public bool CheckPopulationConvergence()
        {
            List<int> firstChromosome = _population[0]._chromosome;
            firstChromosome.Sort();
            foreach (Individual individual in _population)
            {
                List<int> chromosome = individual._chromosome;
                chromosome.Sort();
                if (!firstChromosome.SequenceEqual(chromosome))
                {
                    return false;
                }
            }
            return true;
        }


        public void Generation(decimal lambda)
        {
            bool checkValidCrossover = false;
            while (checkValidCrossover == false)
            {
                List<int> ints = GenerateRandomCombination(2, 100);
                List<Individual> sortedData = _population.OrderBy(x => x._fitness).ToList();
                _population = sortedData;
                Individual chromosome1 = _population[ints[0]];
                Individual chromosome2 = _population[ints[1]];
                checkValidCrossover = true;
                bool checkRandomCrossover = false;
                while (checkRandomCrossover == false)
                {
                    List<int> childChromosome = Crossover(chromosome1._chromosome, chromosome2._chromosome);
                    if (childChromosome.Count == 5)
                    {
                        decimal childFitness = CalcFitness(childChromosome,lambda);
                        Individual child = new Individual(childChromosome, childFitness);
                        _population[_population.Count - 1] = child; //replacing lowest performer with child.
                        checkRandomCrossover = true;
                    }
                }
            }
        }
        public void ExecuteAlgorithm(decimal lambda = 0)
        {
            List<List<int>> chromosomes = GenerateRandomPortfolios();
            List<Individual> individuals = new List<Individual>();
            foreach (List<int> chromosome in chromosomes)
            {
                decimal fitness = CalcFitness(chromosome, lambda);
                Individual val = new Individual(chromosome, fitness);
                individuals.Add(val);
            }
            UpdatePopulation(individuals);
            for (int i = 0; i < 2000; i++)
            {
                Generation(lambda);
                if (CheckPopulationConvergence())
                {
                    break;
                }
            }
        }

    }
}