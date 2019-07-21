using System;
using System.Collections.Generic;

namespace WebApplication1.Models
{
    public class EmployeeModel
    {
        public IList<Employee> employee { get; set; }
    }

    public class Employee
    {
        public int ID_Employee { get; set; }
        public string Family { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Gender { get; set; }
        public string citizenship { get; set; }
        public string Address { get; set; }
        public string FamilyStatus { get; set; }
        public string Education { get; set; }
    }
}
