using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs1
{
    public abstract class Employee : IPerson
    {
        protected string name;
        protected int age;

        public Employee(string name, int age)
        {
            this.name = name;
            this.age = age;
        }

        public string GetName()
        {
            return name;
        }

        public int GetAge()
        {
            return age;
        }

        public abstract double GetSalary();
    }
}
