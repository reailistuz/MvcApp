using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcApp.Data;
using MvcApp.Models;
using Newtonsoft.Json;
using System;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace MvcApp.Services
{
    public class UtilityService : IUtilityService
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly MvcAppContext _context;

        public UtilityService(IHostingEnvironment hostingEnvironment, MvcAppContext context)
        {
            _hostingEnvironment = hostingEnvironment;
            _context = context;
        }

        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null || _context.Employee == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employee.FindAsync(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(employee);
        //}
    }
}
