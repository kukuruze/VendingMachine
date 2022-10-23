using System;
using System.Collections.Generic;
using System.Threading;

namespace Vending_machine
{
    class Program
    {
        static Dictionary<IBeverage, int> beverageInventory { get; set; }

        static void Main(string[] args)
        {
            beverageInventory = new Dictionary<IBeverage, int>();

            var beerStats = GetRandomBeerStats();
            var waterStats = GetRandomWaterStats();
            var cokeStats = GetRandomCokeStats();

            beverageInventory.Add(beerStats.Key, beerStats.Value);
            beverageInventory.Add(waterStats.Key, waterStats.Value);
            beverageInventory.Add(cokeStats.Key, cokeStats.Value);

            PrintBalance();
            PrintBeverages(beverageInventory);
            InstructionsMain();
            ChooseMode();
        }

        private static void PrintBalance()
        {
            Console.WriteLine($"Bal: ${CurrentSession.UserBalance:F2}");
        }

        private static void PrintBeverages(Dictionary<IBeverage, int> beverageInventory)
        {
            Console.WriteLine("VENDING MACHINE HAS:");
            Console.WriteLine("-------------------------");
            char letter = 'A';
            foreach (var beverage in beverageInventory)
            {
                Console.Write(letter+") ");
                Console.WriteLine(beverage.Key.Info() + " " + " - "+ beverage.Value + " left");
                letter++;
            }
            Console.WriteLine("-------------------------");
        }

        private static void InstructionsMain()
        {
            Console.WriteLine("PRESS" + "\n"
                + "A - CHOOSE WHAT TO BUY" + "\n"
                + "B - PUT COINS IN THE VENDING MACHINE");
        }

        private static void InstructionsModeB()
        {
            Console.WriteLine("PRESS TO PLACE IN THE VENDING MACHINE:" + "\n"
                + "A - 0.05" + "\n"
                + "B - 0.10 " + "\n"
                + "C - 0.20 " + "\n"
                + "D - 0.50 " + "\n"
                + "E - EXIT " + "\n");
        }

        private static void HandleModeB()
        {
            Console.Clear();
            PrintBalance();
            PrintBeverages(beverageInventory);
            InstructionsModeB();

            ConsoleKeyInfo userInput = Console.ReadKey();
            switch (userInput.Key)
            {
                case ConsoleKey.A:
                    CurrentSession.UserBalance += 0.05m;
                    HandleModeB();
                    break;
                case ConsoleKey.B:
                    CurrentSession.UserBalance += 0.1m;
                    HandleModeB();
                    break;
                case ConsoleKey.C:
                    CurrentSession.UserBalance += 0.2m;
                    HandleModeB();
                    break;
                case ConsoleKey.D:
                    CurrentSession.UserBalance += 0.5m;
                    HandleModeB();
                    break;
                case ConsoleKey.E:
                    Console.Clear();
                    PrintBalance();
                    PrintBeverages(beverageInventory);
                    InstructionsMain();
                    ChooseMode();
                    break;
                default:
                    HandleModeB();
                    break;
            }
        }


        private static void ChooseMode()
        {
            ConsoleKeyInfo userInput = Console.ReadKey();
            //try
            //{
                switch (userInput.Key)
                {
                    case ConsoleKey.A:
                        HandleModeA();
                    break;
                    case ConsoleKey.B:
                        HandleModeB();
                        break;
                    default:
                    //throw new ArgumentOutOfRangeException();
                    Console.Clear();
                    PrintBalance();
                    PrintBeverages(beverageInventory);
                    InstructionsMain();
                    ChooseMode();
                    break;
                }
            //}
            /*catch(ArgumentOutOfRangeException)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR! PLEASE TRY AGAIN!");
                Console.ForegroundColor = ConsoleColor.White;
                InstructionsMain();
                ChooseMode();
            }*/
        }

        private static void InstructionsModeA()
        {
            Console.WriteLine("SELECT YOUR BEVERAGE...");
        }

        private static void HandleModeA()
        {
            Console.Clear();
            PrintBalance();
            PrintBeverages(beverageInventory);
            InstructionsModeA();

            char userInput = Convert.ToChar(Convert.ToString(Console.ReadKey().KeyChar).ToUpper());
            char letter = 'A';
            try
            {
                if (userInput - letter > beverageInventory.Count)
                {
                    throw new ArgumentOutOfRangeException();
                }
            }
            catch
            {
                Console.Clear();
                PrintBalance();
                PrintBeverages(beverageInventory);
                InstructionsMain();
                ChooseMode();
            }
            
            foreach (var beverage in beverageInventory)
            {
                if(letter == userInput)
                {
                    Buy(beverage.Key);
                    break;
                }   
                letter++;
            }
        }

        private static void Buy(IBeverage key)
        {
            try
            {
                if(beverageInventory[key] < 1)
                {
                    throw new InsufficientAmountException("Nedostatuchno kolichestvo");
                }
                if (key.Price > CurrentSession.UserBalance)
                {
                    throw new InsufficientBalanceException("Nedostatuchen balans");
                }
                else
                {
                    CurrentSession.UserBalance -= key.Price;
                    beverageInventory[key]--;

                    Console.Clear();
                    Console.WriteLine($"YOU JUST BOUGHT {key.Info()}");
                    if (beverageInventory[key] == 0)
                    {
                        beverageInventory.Remove(key);
                    }
                }
            }
            catch(InsufficientBalanceException)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR! INSUFFICIENT BALANCE!");
                Console.ForegroundColor = ConsoleColor.White;

                Thread.Sleep(800);
                Console.Clear();
                PrintBalance();
                PrintBeverages(beverageInventory);
                InstructionsMain();
                ChooseMode();
            }
            catch(InsufficientAmountException)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("ERROR! NO STOCK FOR THIS BEVERAGE!");
                Console.ForegroundColor = ConsoleColor.White;

                Thread.Sleep(800);
                Console.Clear();
                PrintBalance();
                PrintBeverages(beverageInventory);
                InstructionsMain();
                ChooseMode();
            }


            Console.WriteLine($"ENJOY YOUR DRINK!");
            Console.WriteLine($"CHANGE: {CurrentSession.UserBalance}");
        }

        private static KeyValuePair<Beer,int> GetRandomBeerStats()
        {
            Random random = new Random();

            Beer newBeer = new Beer(((decimal)random.Next(149, 300)) / 100);

            int beerCount = random.Next(1, 11);

            return new KeyValuePair<Beer, int>(newBeer,beerCount);
        }

        private static KeyValuePair<Water,int> GetRandomWaterStats()
        {
            Random random = new Random();

            Water newWater = new Water(((decimal)random.Next(49, 200)) / 100);

            int waterCount = random.Next(1, 11);

            return new KeyValuePair<Water, int>(newWater, waterCount);
        }

        private static KeyValuePair<Coke,int> GetRandomCokeStats()
        {
            Random random = new Random();

            Coke newCoke = new Coke(((decimal)random.Next(99, 250)) / 100);

            int cokeCount = random.Next(1, 11);

            return new KeyValuePair<Coke, int>(newCoke, cokeCount);
        }
    }
}
