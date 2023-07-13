using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs3
{
    class Customer
    {
        public int ServiceTime { get; private set; }
        public double PurchaseAmount { get; private set; }

        public Customer()
        {
            Random random = new Random();
            ServiceTime = random.Next(1, 8); //  диапазон длительности обслуживания 
            PurchaseAmount = random.Next(30, 9001); //  диапазон суммы покупки 
        }
    }
}
