using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftgunZonen.Factories;
using SoftgunZonen.Models;

namespace SoftgunZonen.Controllers
{
    public class HomeController : Controller
    {
        DBContext context = DBContext.Instance;

        // GET: Home
        public ActionResult Index()
        {
            Page index = context.PageFactory.Get(1);
            return View(index);
        }

        public ActionResult Products()
        {

            List<Category> allCategories = context.CategoryFactory.GetAll();
            return View(allCategories);
        }

        public ActionResult ProductList(int id = 0)
        {
            ViewBag.Categories = context.CategoryFactory.GetAll();
            ViewBag.Category = context.CategoryFactory.Get(id);


            List<Product> productsByCategoryID = (id > 0 ? context.ProductFactory.GetAllBy("CategoryID", id) : context.ProductFactory.GetAll());
            return View(productsByCategoryID);
        }

        public ActionResult ShowProduct(int id = 0)
        {
            Product product = context.ProductFactory.Get(id);
            return View(product);
        }

        [ChildActionOnly]
        public ActionResult FooterInfo()
        {
            Contact contact = context.ContactFactory.Get(1);
            return PartialView("FooterInfoPartial", contact);
        }

        [ChildActionOnly]
        public ActionResult Slider()
        {
            List<Slider> sliders = context.SliderFactory.GetAll();
            return PartialView("SliderPartial", sliders);
        }
    }
}