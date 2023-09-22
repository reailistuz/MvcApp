using MvcApp.Models;

namespace MvcApp.Services
{
    public interface ICsvService
    {
        /// <summary>
        /// Read and Parse CSV file
        /// </summary>
        /// <param name="filePath">file path</param>
        List<Employee> ParseCsv(Stream filePath);
    }
}
