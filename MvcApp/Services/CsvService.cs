using MvcApp.Models;


namespace MvcApp.Services
{
    public class CsvService : ICsvService
    {
        public List<Employee> ParseCsv(Stream csvStream)
        {
            var employees = new List<Employee>();

            using (var reader = new StreamReader(csvStream))
            {
                var isFirstRow = true;

                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(',');

                    if (isFirstRow)
                    {
                        isFirstRow = false;
                        continue; // Skip the header row (if applicable)
                    }

                    var employee = MapToEmployee(values);
                    employees.Add(employee);
                }
            }

            return employees;
        }

        //public List<Employee> ReadCsvFile(MemoryStream csvStream)
        //{
        //    var employees = new List<Employee>();

        //    using (var reader = new StreamReader(csvStream))
        //    using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //    {
        //        // Read the header line.
        //        csv.ReadHeader();

        //        // Read each record in the CSV file.
        //        while (csv.Read())
        //        {
        //            // Create a new employee object.
        //            Employee employee = new Employee();

        //            // Map the CSV fields to the employee properties.
        //            employee.PayrollNumber = csv.GetField(0);
        //            employee.FirstName = csv.GetField(1);
        //            employee.LastName = csv.GetField(2);
        //            employee.BirthDate = csv.GetField<DateTime>(3);
        //            employee.Phone = csv.GetField(4);
        //            employee.MobilePhone = csv.GetField(5);
        //            employee.Address = csv.GetField(6);
        //            employee.Address2 = csv.GetField(7);
        //            employee.PostCode = csv.GetField(8);
        //            employee.Email = csv.GetField(9);
        //            employee.StartDate = csv.GetField<DateTime>(10);

        //            // Add the employee to the list.
        //            employees.Add(employee);
        //        }
        //    }

        //    return employees;
        //}

        private Employee MapToEmployee(string[] values)
        {
            return new Employee
            {
                PayrollNumber = values[0].Trim(),
                FirstName = values[1].Trim(),
                LastName = values[2].Trim(),
                BirthDate = DateTime.Parse(values[3].Trim()),
                Phone = values[4].Trim(),
                MobilePhone = values[5].Trim(),
                Address = values[6].Trim(),
                Address2 = values[7].Trim(),
                PostCode = values[8].Trim(),
                Email = values[9].Trim(),
                StartDate = DateTime.Parse(values[10].Trim())
            };
        }
    }
}

