using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_machine
{
    public static class CurrentSession
    {
        public static decimal UserBalance { get; set; }
        public static int SugarLevel { get; set; }
    }
}
