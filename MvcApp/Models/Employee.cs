using System.Net;
using System.Reflection;

namespace MvcApp.Models
{
    public class Employee
    {
        public Guid EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PayrollNumber { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }

        public string MobilePhone { get; set; }

        public string Address { get; set; }

        public string Address2 { get; set; }

        public string PostCode { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime BirthDate { get; set; }
    }
}