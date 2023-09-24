using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcApp.Data;
using MvcApp.Models;
using MvcApp.Services;
using System.Diagnostics;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly MvcAppContext _dbContext;

        private readonly ICsvService _csvService;

        public HomeController(ILogger<HomeController> logger, Data.MvcAppContext dbContext, ICsvService csvService)
        {
            _logger = logger;
            _dbContext = dbContext;
            _csvService = csvService;
        }

        public IActionResult Index(string sortOrder, string searchString)
        {
            return GetEmployees(sortOrder, searchString);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> UploadAsync(IFormFile uploadedFile)
        {
            int processedRowCount = 0; // Initialize a counter for successfully processed rows

            if (uploadedFile != null && uploadedFile.Length > 0)
            {
                try
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        // Copy the uploaded file to a memory stream
                        uploadedFile.CopyTo(memoryStream);
                        memoryStream.Flush();
                        memoryStream.Position = 0;

                        var employees = _csvService.ParseCsv(memoryStream);

                        _dbContext.Employee.AddRange(employees);
                        await _dbContext.SaveChangesAsync();

                        // Count the successfully processed rows
                        processedRowCount = employees.Count;

                        // Provide user feedback
                        ViewBag.Message = $"{employees.Count} records imported successfully.";
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = $"Error: {ex.Message}";
                }
            }
            else
            {
                ViewBag.Message = "No file uploaded.";
            }

            // Pass the processedRowCount to the view for displaying the count
            ViewBag.ProcessedRowCount = processedRowCount;

            return View("Index");
        }

        [HttpGet]
        public IActionResult GetEmployees(string sortOrder, string searchString)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.DateSortParm = sortOrder == "date" ? "date_desc" : "date";
            var employees = from s in _dbContext.Employee
                            select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                employees = employees.Where(s => s.LastName.Contains(searchString)
                                       || s.FirstName.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    employees = employees.OrderByDescending(s => s.LastName);
                    break;
                case "date":
                    employees = employees.OrderBy(s => s.StartDate);
                    break;
                case "date_desc":
                    employees = employees.OrderByDescending(s => s.StartDate);
                    break;
                default:
                    employees = employees.OrderBy(s => s.LastName);
                    break;
            }
            return View(employees.ToList());
        }

        public async Task<IActionResult> EditAsync(string? id)
        {
            if (string.IsNullOrEmpty(id) || _dbContext.Employee == null)
            {
                return NotFound();
            }

            var employee = await _dbContext.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }
    }
}