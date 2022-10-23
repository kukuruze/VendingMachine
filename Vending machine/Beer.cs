using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_machine
{
    class Beer : IBeverage
    {
        private decimal price;
        public decimal Price { get => price; private set => price = value; }

        public string Info()
        {
            return $"Beer - ${Price:F2}";
        }

        public Beer()
        {

        }

        public Beer(decimal price)
        {
            Price = price;
        }
    }
}
