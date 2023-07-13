using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs1
{
    public class Professor : Employee
    {
        private string specialization;
        private Random random;

        public Professor(string name, int age, string specialization) : base(name, age)
        {
            this.specialization = specialization;
            random = new Random();
        }

        public override double GetSalary()
        {
            double salary = random.Next(20000, 100000);
            return salary;
        }

        public string GetSpecialization()
        {
            return specialization;
        }
    }
}
