using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp6.Models
{
   public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public int Age{ get; set; }
        public long Phone{ get; set; }
        public User( string name, string surname, int age, long phone)
        {
            Name = name;
            Surname = surname;
            Age = age;
            Phone = phone;
        }
    }
}
