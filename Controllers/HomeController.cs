using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;

namespace LibraryManagement.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    [Authorize]
    public IActionResult Privacy()
    {
        return View();
    }

    

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            
            var customStatusCode = HttpContext.Items["CustomStatusCode"] as int?;
            var statusCode = HttpContext.Response.StatusCode;

            if (customStatusCode.HasValue && customStatusCode.Value == 0000)
        {
            ViewData["StatusCode"] = 0000;
            ViewData["ErrorMessage"] = "The Name/Title already exists. Please check your input.";
            }
            else
            {
               return RedirectToAction("HandleError", new { statusCode = statusCode });
            }

            
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

       public IActionResult HandleError(int statusCode)
        {
            ViewData["StatusCode"] = statusCode;
            ViewData["ErrorMessage"] = statusCode switch
            {
                
                401 => "Access denied. Please log in with the correct credentials to view this resource.",
                404 => "We can't seem to find the page you're looking for. It might have been moved or deleted.",
                429 => "You have sent too many requests in a given amount of time. Please wait a while and try again.",
                503 => "The server is currently unable to handle the request due to a temporary overload or maintenance. Please try again later.",
                _ => "An unexpected error occurred [Handle Error]."
            };
            return View("Error", new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    



}

    

