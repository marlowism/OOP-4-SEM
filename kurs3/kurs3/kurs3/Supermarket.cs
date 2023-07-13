using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs3
{
    public class Supermarket
    {
        private List<Cashier> cashiers;
        private List<Customer> customers;
        private Random random;
        private int dayOfWeek;
        private int currentTime;
        private double advertisingCost;
        private double discountPercentage;
        private double profitPercentage;
        private double baseProfit;
        private double cashierSalary;
        private double maxQueueFlowReduction;

        // Переменные статистики
        private int totalCustomersServed;
        private int totalCustomersLost;
        private int totalQueueLength;
        private int totalWaitTime;
        private double totalProfit;

        public Supermarket(int cashierCount, int maxQueueLength, double advertisingCost, double discountPercentage, double profitPercentage, double cashierSalary, double maxQueueFlowReduction)
        {
            cashiers = new List<Cashier>();
            customers = new List<Customer>();
            random = new Random();
            dayOfWeek = (int)DateTime.Now.DayOfWeek;
            currentTime = 0;

            for (int i = 0; i < cashierCount; i++)
            {
                Cashier cashier = new Cashier(maxQueueLength, cashierSalary);
                cashiers.Add(cashier);
            }

            this.advertisingCost = advertisingCost;
            this.discountPercentage = discountPercentage;
            this.profitPercentage = profitPercentage;
            baseProfit = 0.0;
            this.cashierSalary = cashierSalary;
            this.maxQueueFlowReduction = maxQueueFlowReduction;

            // Инициализация переменных статистики
            totalCustomersServed = 0;
            totalCustomersLost = 0;
            totalQueueLength = 0;
            totalWaitTime = 0;
            totalProfit = 0.0;
        }

        public void Simulate(int customerCount)
        {
            for (int i = 0; i < customerCount; i++)
            {
                currentTime++;

                if (currentTime == 1440)
                {
                    currentTime = 0;
                    dayOfWeek++;

                    if (dayOfWeek == 7)
                    {
                        dayOfWeek = 0;
                    }
                }

                if (ShouldGenerateCustomer())
                {
                    Customer customer = new Customer();
                    customers.Add(customer);

                    bool customerServed = false;
                    foreach (Cashier cashier in cashiers)
                    {
                        if (cashier.IsAvailable())
                        {
                            cashier.ServeCustomer(customer);
                            customerServed = true;
                            break;
                        }
                    }

                    if (!customerServed)
                    {
                        totalCustomersLost++;
                    }
                }


                UpdateCashierAvailability();

                // Обновление значений переменных статистики
                foreach (Cashier cashier in cashiers)
                {
                    totalCustomersServed += cashier.totalCustomersServed;
                    totalQueueLength += cashier.totalQueueLength;
                    totalWaitTime += cashier.totalWaitTime;
                    totalProfit += cashier.totalProfit;
                }

               
                Console.WriteLine("Total Customers Served: " + totalCustomersServed);
                Console.WriteLine("Total Customers Lost: " + totalCustomersLost);
                Console.WriteLine("Total Wait Time: " + totalWaitTime);
                Console.WriteLine("Total Profit: " + totalProfit);

               
                string statData = $"Клиентов обслужено:{totalCustomersServed},Всего клиентов потеряно:{totalCustomersLost}, Всего времени ожидания:{totalWaitTime}, Всего заработано:{totalProfit}";
                File.WriteAllText("statistics.txt", statData);
            }
        }

        private bool ShouldGenerateCustomer()
        {
            double lambda = CalculateLambda();
            double exponentialValue = -Math.Log(1 - random.NextDouble()) / lambda;
            double arrivalProbability = CalculateArrivalProbability();

            return exponentialValue <= 1 && arrivalProbability > random.NextDouble();
        }

        private double CalculateLambda()
        {
            double baseLambda = 0.0;

            switch (dayOfWeek)
            {
                case 0:
                case 6:
                    baseLambda = 0.12;
                    break;
                case 1:
                case 2:
                case 3:
                case 4:
                    baseLambda = 0.1;
                    break;
                case 5:
                    baseLambda = 0.14;
                    break;
            }

            double queueFlowReduction = maxQueueFlowReduction * (cashiers.Count - 1);
            double lambda = baseLambda * (1 - queueFlowReduction);

            return lambda;
        }

        private double CalculateArrivalProbability()
        {
            double baseArrivalProbability = 0.0;

            switch (currentTime)
            {
                case int n when (n >= 420 && n < 600):
                    baseArrivalProbability = 0.08;
                    break;
                case int n when (n >= 600 && n < 900):
                    baseArrivalProbability = 0.12;
                    break;
                case int n when (n >= 900 && n < 1140):
                    baseArrivalProbability = 0.1;
                    break;
                case int n when (n >= 1140 && n < 1260):
                    baseArrivalProbability = 0.13;
                    break;
                case int n when (n >= 1260 || n < 420):
                    baseArrivalProbability = 0.07;
                    break;
            }

            double arrivalProbability = baseArrivalProbability * (1 + discountPercentage);

            return arrivalProbability;
        }

        private Cashier SelectCashier()
        {
            List<Cashier> availableCashiers = cashiers.Where(c => c.IsAvailable()).ToList();
            Cashier selectedCashier = null;

            if (availableCashiers.Count > 0)
            {
                selectedCashier = availableCashiers.OrderBy(c => c.GetQueueLength()).First();
            }

            return selectedCashier;
        }

        private void UpdateCashierAvailability()
        {
            foreach (Cashier cashier in cashiers)
            {
                if (!cashier.IsAvailable() && cashier.ShouldCustomerLeave())
                {
                    cashier.ServeCustomer(new Customer());
                }
            }
        }
    }
}
