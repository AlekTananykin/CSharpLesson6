using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    class Student
    {
        public string FirstName
        { get; set; }

        public string SecondName
        { get; set; }

        public string Univercity
        { get; set; }

        public string Faculty
        { get; set; }

        public string Department
        { get; set; }

        public int Age
        { get; set; }

        public int Course
        { get; set; }

        public int Group
        { get; set; }

        public string City
        { get; set; }

        public override string ToString()
        {
            return String.Format("{0}, {1}, {2}, {3}, {4}, " +
                "возраст {5}, курс {6}, группа {7}, город {8} ",
                    FirstName, SecondName, Univercity,
                    Faculty, Department, Age,
                    Course, Group, City);
        }
    }
}
