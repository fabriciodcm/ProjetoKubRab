using Microsoft.AspNetCore.Mvc;
using ProjectKubRab.ProductsWebApp.Models;
using System.Diagnostics;
using System.Xml.Linq;

namespace ProjectKubRab.ProductsWebApp.Controllers
{
    public class HomeController : Controller
    {
        
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Products()
        {
            var Products = new List<Product>() {
                new Product("MSI RTX 5060 Ti Ventus 2X OC Plus", "R$ 2.699,99") ,
                new Product("MSI GeForce RTX 5070 12G VENTUS 2X OC", "R$ 4.399,99" ),
                new Product("ASRock RX 9060 XT CL 16GB AMD Radeon", "R$ 2.799,99" ),
                new Product("ASRock RX 9070 XT Challenger AMD 16GB", "R$ 4.299,99" ),
                new Product("Asus TUF-RTX 5070 TI 16G GAMING 16GB", "R$ 9.226,00" ),
                new Product("AMD Ryzen 7 5700X", "R$ 1.199,99" ),
                new Product("AMD Ryzen 7 7700X", "R$ 1.499,99" ),
                new Product("AMD Ryzen 7 7800X3D", "R$ 1.979,90" ),
                new Product("Intel Core i9-14900F", "R$ 2.549,99" ),
                new Product("AMD Ryzen 9 9900X3D", "R$ 3.099,99" )
            };
            return View(Products);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
