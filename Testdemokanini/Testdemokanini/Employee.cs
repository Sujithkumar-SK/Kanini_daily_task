using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Testdemokanini
{
    public class Employee
    {
        string Name;
        int Age;

        public Employee(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public string name
        {
            get
            {
                return Name;
            }
            set
            {
                Name = value;
            }
        }
    }
}
