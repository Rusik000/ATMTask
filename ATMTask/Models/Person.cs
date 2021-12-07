using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMTask.Models
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public double Balance { get; set; }

        public string CardNumber { get; set; }
        public override string ToString()
        {
            return Name+Surname;
        }



    }
}
