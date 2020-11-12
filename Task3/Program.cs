using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*Тананыкин
 * 
 * Переделать программу Пример использования коллекций для решения следующих задач:
 * а) Подсчитать количество студентов учащихся на 5 и 6 курсах;
 * б) подсчитать сколько студентов в возрасте от 18 до 20 лет на каком курсе учатся (*частотный массив);
 * в) отсортировать список по возрасту студента;
 * г) *отсортировать список по курсу и возрасту студента;
 */
namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            string studentsPath = Directory.GetCurrentDirectory() +
               "/students_1.csv";
            try
            {
                IList<Student> students = ReadStudents(studentsPath);

                Console.WriteLine("Список студентов");

                PrintStudents(students);

                PrintCountOnCourses56(students);

                PrintFreq(students);

                AgeSorting(students);

                AgeAndCourseSorting(students);


            }
            catch (StudentsCountException ex)
            {
                Console.WriteLine(ex.Message);
                if (null != ex.InnerException)
                    Console.WriteLine(ex.InnerException.Message);
            }
            Console.WriteLine("\nНажмите любую клавишу для завершения программы.");
            Console.ReadKey();
        }

        private static void AgeAndCourseSorting(IList<Student> students)
        {
            Console.WriteLine("\nг)Сортировка по курсу и возрасту студента:");
            ((List<Student>)students).Sort(delegate (Student st1, Student st2)
            {
                if (st1.Age < st2.Age)
                    return 1;

                if (st1.Age > st2.Age)
                    return -1;

                if (st1.Course < st2.Course)
                    return 1;

                if (st1.Course > st2.Course)
                    return -1;

                return 0;
            });
            PrintStudents(students);
        }

        private static void AgeSorting(IList<Student> students)
        {
            Console.WriteLine("\nв)Сортировка по возрасту студента:");
            ((List<Student>)students).Sort(delegate (Student st1, Student st2)
            {
                if (st1.Age < st2.Age)
                    return 1;

                if (st1.Age > st2.Age)
                    return -1;

                return 0;
            });
            PrintStudents(students);
        }

        private static void PrintCountOnCourses56(IList<Student> students)
        {
            int countOfStudents = 0;
            foreach (Student student in students)
            {
                if (5 == student.Course || 6 == student.Course)
                    ++countOfStudents;
            }
            Console.WriteLine(
                "\nа)Количество студентов, которые участся на 5 и 6 курсах: {0}",
                countOfStudents);
        }

        private static void PrintFreq(IList<Student> students)
        {
            IDictionary<int, int> freq = new Dictionary<int, int>();
            foreach (Student student in students)
            {
                if (18 <= student.Age && 20 >= student.Age)
                {
                    if (freq.ContainsKey(student.Course))
                        ++freq[student.Course];
                    else
                        freq.Add(student.Course, 1);
                }
            }
            Console.WriteLine("\nб)Студенты в возрасте от 18 лет до 20(включительно)");
            foreach (KeyValuePair<int, int> kvp in freq)
                Console.WriteLine(
                    "курс {0}; количество студентов: {1}", kvp.Key, kvp.Value);
        }


        private static IList<Student> ReadStudents(string filePath)
        {
            List <Student> students = new List<Student>();
            try
            {
                using (StreamReader sr = new StreamReader(filePath))
                {
                    while (!sr.EndOfStream)
                    {
                        string line = sr.ReadLine();
                        students.Add(CreateStudent(line));
                    }
                }
            }
            catch (Exception ex)
            {
                throw new StudentsCountException(
                    "Ошибка считывания студентов из файла. ", ex);
            }

            return students;
        }

        private static void PrintStudents(IList<Student> students)
        {
            foreach (Student student in students)
                Console.WriteLine(student.ToString());
        }

        private static Student CreateStudent(string line)
        {
            string[] columns = line.Split(';');

            if (9 != columns.Length)
                throw new StudentsCountException(
                    "Колличество колонок в считываемом файле не равно 9.");

            return new Student() {
                FirstName = columns[0],
                SecondName = columns[1],
                Univercity = columns[2],
                Faculty = columns[3],
                Department = columns[4],
                Age = int.Parse(columns[5]),
                Course = int.Parse(columns[6]),
                Group = int.Parse(columns[7]),
                City = columns[8]
            };
        }
    }
}
