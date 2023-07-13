using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs1
{
    public class Student : IPerson
    {
        private string name;
        private int age;
        private string major;

        public Student(string name, int age, string major)
        {
            this.name = name;
            this.age = age;
            this.major = major;
        }

        public string GetName()
        {
            return name;
        }

        public int GetAge()
        {
            return age;
        }

        public string GetMajor()
        {
            return major;
        }
    }
}
