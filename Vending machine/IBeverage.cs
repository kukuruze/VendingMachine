using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_machine
{
    interface IBeverage
    {
        decimal Price { get; }
        string Info();
    }
}
