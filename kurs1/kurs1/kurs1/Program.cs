using System;

namespace kurs1
{

    class Program
    {
        static void Main(string[] args)
        {
            University university = new University("Университет");

            Professor professor1 = new Professor("Иван Иванов ", 40, "Компьютерные науки и Математика");
            Professor professor2 = new Professor("Мария Белая", 52, "Физика и Английский");

            Student student1 = new Student("Михаил Алексеев ", 20, "Компьютерные науки \nКурс 2");
            Student student2 = new Student("Алексей Глух ", 19, "Физика \nКурс 1");

            Course course1 = new Course("Курс 1");
            Course course2 = new Course("Курс 2");




            university.AddProfessor(professor1);
            university.AddProfessor(professor2);
            university.AddStudent(student1);
            university.AddStudent(student2);

            course1.AddStudent(student1);
            course2.AddStudent(student2);


            Console.WriteLine("Профессора:");
            foreach (Professor professor in university.Professors)
            {
                Console.WriteLine("Имя: " + professor.GetName());
                Console.WriteLine("Возраст: " + professor.GetAge());
                Console.WriteLine("Специализация: " + professor.GetSpecialization());
                Console.WriteLine("Зарплата: " + professor.GetSalary());
                Console.WriteLine();
            }

            Console.WriteLine("Студенты:");
            foreach (Student student in university.Students)
            {
                Console.WriteLine("Имя: " + student.GetName());
                Console.WriteLine("Возраст: " + student.GetAge());
                Console.WriteLine("Специализация: " + student.GetMajor());
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}