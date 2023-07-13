using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace kurs1
{
    public class Course
    {
        private string name;
        public List<Professor> Professors { get; private set; }
        public List<Student> Students { get; private set; }

        public Course(string name)
        {
            this.name = name;
            Professors = new List<Professor>();
            Students = new List<Student>();
        }

        public void AddProfessor(Professor professor)
        {
            Professors.Add(professor);
        }

        public void RemoveProfessor(Professor professor)
        {
            Professors.Remove(professor);
        }

        public void AddStudent(Student student)
        {
            Students.Add(student);
        }

        public void RemoveStudent(Student student)
        {
            Students.Remove(student);
        }

    }
}
