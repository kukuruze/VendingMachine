using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_machine
{
    class Coke : IBeverage
    {
        private decimal price;
        public decimal Price { get => price; private set => price = value; }

        public string Info()
        {
            return $"Coke - ${Price:F2}";
        }

        public Coke()
        {

        }

        public Coke(decimal price)
        {
            Price = price;
        }
    }
}
