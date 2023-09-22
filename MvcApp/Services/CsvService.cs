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

