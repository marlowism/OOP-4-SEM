using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs3
{
    class Cashier
    {
        private bool available;
        private Queue<Customer> queue;
        private int maxQueueLength;
        private double leavingProbability;
        private double dailySalary;

        // Переменные статистики
        public int totalCustomersServed;
        public int totalQueueLength;
        public int totalWaitTime;
        public double totalProfit;

        public int MaxQueueLength => maxQueueLength;

        public Cashier(int maxQueueLength, double dailySalary)
        {
            available = true;
            queue = new Queue<Customer>();
            this.maxQueueLength = maxQueueLength;
            leavingProbability = CalculateLeavingProbability();
            this.dailySalary = dailySalary;
        }

        public bool IsAvailable()
        {
            return available;
        }

        public void ServeCustomer(Customer customer)
        {
            available = false;
            queue.Enqueue(customer);

            int serviceTime = customer.ServiceTime;
            System.Threading.Thread.Sleep(serviceTime * 1000);

            queue.Dequeue();

            if (queue.Count == 0)
            {
                available = true;
            }
            else if (queue.Count == maxQueueLength - 1)
            {
                leavingProbability = CalculateLeavingProbability();
            }

            // Обновление статистики
            this.totalCustomersServed++;
            this.totalQueueLength += GetQueueLength();
            this.totalWaitTime += customer.ServiceTime;
            this.totalProfit += customer.PurchaseAmount;

        }

        public int GetQueueLength()
        {
            return queue.Count + (IsAvailable() ? 0 : 1);
        }

        public bool ShouldCustomerLeave()
        {
            double randomValue = new Random().NextDouble();
            return randomValue < leavingProbability;
        }

        private double CalculateLeavingProbability()
        {
            int queueDifference = maxQueueLength - queue.Count - 1;
            return 0.2 * queueDifference;
        }

    }
}
