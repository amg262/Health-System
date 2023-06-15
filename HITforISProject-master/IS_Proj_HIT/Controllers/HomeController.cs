using Microsoft.AspNetCore.Mvc;

namespace IS_Proj_HIT.Controllers
{
    
    public class HomeController : Controller
    {
        public HomeController() { }

        
        public IActionResult Index() => View();
        
        // pull url from appsettings.json file
        public IActionResult BritRedirect() => Redirect("https://hcsdev.wctc.edu:4443");
        
        public IActionResult Privacy()
        {
            ViewBag.privacy = "privacy";
            return View("Privacy");
        }

    }
}
