using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending_machine
{
    [Serializable]
    public class InsufficientBalanceException : Exception
    {
        public InsufficientBalanceException() { }

        public InsufficientBalanceException(string message)
            : base(message) { }

        public InsufficientBalanceException(string message, Exception inner)
            : base(message, inner) { }
    }
}
