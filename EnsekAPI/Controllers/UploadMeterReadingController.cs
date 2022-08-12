using System;
using Microsoft.AspNetCore.Mvc;

namespace EnsekAPI.Controllers
{
    /// <summary>
    /// Upload meter reading controller
    /// </summary>
    public class UploadMeterReadingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}