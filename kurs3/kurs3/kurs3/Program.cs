using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs3
{
    class Program
    {
        static void Main(string[] args)
        {
            int cashierCount = 5;
            int maxQueueLength = 5;
            double advertisingCost = 500.0;
            double discountPercentage = 0.05;
            double profitPercentage = 0.2;
            double cashierSalary = 1500.0;
            double maxQueueFlowReduction = 0.05;
            int customerCount = 1000;

            Supermarket supermarket = new Supermarket(cashierCount, maxQueueLength, advertisingCost, discountPercentage, profitPercentage, cashierSalary, maxQueueFlowReduction);
            supermarket.Simulate(customerCount);

            Console.ReadLine();
        }
    }

}
