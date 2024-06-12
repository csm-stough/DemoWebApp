using DemoWebApp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DemoWebApp.Controllers
{
    public class HomeController : Controller
    {
        IConfiguration _configuration;

        public HomeController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Index()
        {
            using (BottlesContext bottlesContext = new BottlesContext(_configuration))
            {
                return View(bottlesContext.Bottles.OrderBy(record => record.Date).ToList());
            }
        }

        //Default Create View with no model
        //there are 2 kinds of ActionMethods --> HttpGet and HttpPost.
        //by default, Action Methods are HttpGet, unless specified...
        public IActionResult Create()
        {
            return View();
        }

        //Create action method with a provided model (this is from the user entry form)
        //The HttpPost tag here indicates that this is a POST method (instead of data being embedded in the URL, its in the message body)
        [HttpPost]
        public IActionResult Create(BottlesModel bottles)
        {
            //here were using data validation to convey to the user if thier data satifies our defined constraints we defined in the model class
            if (ModelState.IsValid)
            {
                //here we add the model to the db using our context object
                using (BottlesContext bottlesContext = new BottlesContext(_configuration))
                {
                    bottlesContext.Add(bottles);
                    bottlesContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                //if validation fails then the failures will be passed along to the view and displayed
                return View(bottles);
            }
        }

        public IActionResult Edit(int id)
        {
            using (BottlesContext bottlesContext = new BottlesContext(_configuration))
            {
                return View(bottlesContext.Bottles.Find(id));
            }
        }

        [HttpPost]
        public IActionResult Edit(BottlesModel bottles)
        {
            if (ModelState.IsValid)
            {
                using (BottlesContext bottlesContext = new BottlesContext(_configuration))
                {
                    bottlesContext.Update(bottles);
                    bottlesContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(bottles);
            }
        }

        public IActionResult Delete(int id)
        {
            using (BottlesContext bottlesContext = new BottlesContext(_configuration))
            {
                return View(bottlesContext.Bottles.Find(id));
            }
        }

        [HttpPost]
        public IActionResult Delete(BottlesModel bottles)
        {
            if (ModelState.IsValid)
            {
                using (BottlesContext bottlesContext = new BottlesContext(_configuration))
                {
                    bottlesContext.Remove(bottles);
                    bottlesContext.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(bottles);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
