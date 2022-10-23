using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_machine
{
    class Water : IBeverage
    {
        private decimal price;
        public decimal Price { get => price; private set => price = value; }

        public string Info()
        {
            return $"Water - ${Price:F2}";
        }

        public Water()
        {

        }

        public Water(decimal price)
        {
            Price = price;
        }
    }
}
