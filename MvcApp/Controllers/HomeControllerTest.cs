using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MvcApp.Data;
using MvcApp.Services;

namespace MvcApp.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private readonly HomeController _homeController;

        [TestMethod]
        public async Task TestEditViewAsync()
        {
            // Act
            var result = await _homeController.EditAsync("F449BEA2-CF5A-4AED-E612-08DBBB654768"); // Make sure the action method is asynchronous and you await it.

            // Assert
            // Check if the result is of type RedirectToActionResult
            Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

            // Cast the result to RedirectToActionResult to access its properties
            var redirectResult = (RedirectToActionResult)result;

            // Assert that the action name is "Edit"
            Assert.AreEqual("Edit", redirectResult.ActionName);
        }
    }
}
