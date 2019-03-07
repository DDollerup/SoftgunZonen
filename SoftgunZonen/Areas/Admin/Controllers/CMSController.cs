using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SoftgunZonen.Factories;
using SoftgunZonen.Models;
using System.IO;
using System.Web.Security;

namespace SoftgunZonen.Areas.Admin.Controllers
{
    [Authorize]
    public class CMSController : Controller
    {
        DBContext context = DBContext.Instance;
        // GET: Admin/CMS
        public ActionResult Index()
        {
            return View();
        }

        #region Index
        public ActionResult EditIndex()
        {
            Page index = context.PageFactory.Get(1);
            return View(index);
        }

        [HttpPost]
        public ActionResult EditIndex(Page page)
        {
            context.PageFactory.Update(page);
            return RedirectToAction("EditIndex");
        }
        #endregion

        #region Store
        public ActionResult EditStore()
        {
            Page store = context.PageFactory.Get(2);
            return View(store);
        }

        [HttpPost]
        public ActionResult EditStore(Page page, HttpPostedFileBase imageFile)
        {
            if (imageFile?.ContentLength > 0)
            {
                page.Image = imageFile.FileName;
                string rootPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Pages/" + imageFile.FileName;
                imageFile.SaveAs(rootPath + savePath);
            }

            context.PageFactory.Update(page);

            return RedirectToAction("EditStore");
        }
        #endregion

        #region Category
        public ActionResult Categories()
        {
            List<Category> categories = context.CategoryFactory.GetAll();
            return View(categories);
        }

        public ActionResult EditCategory(int id = 0)
        {
            Category category = context.CategoryFactory.Get(id);
            return View(category);
        }

        [HttpPost]
        public ActionResult EditCategory(Category category, HttpPostedFileBase imageFile)
        {
            if (imageFile?.ContentLength > 0)
            {
                category.Image = imageFile.FileName;
                string rootPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Categories/" + imageFile.FileName;
                imageFile.SaveAs(rootPath + savePath);
            }

            context.CategoryFactory.Update(category);
            return RedirectToAction("Categories");
        }

        public ActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCategory(Category category, HttpPostedFileBase imageFile)
        {
            if (imageFile?.ContentLength > 0)
            {
                category.Image = imageFile.FileName;
                string rootPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Categories/" + imageFile.FileName;
                imageFile.SaveAs(rootPath + savePath);
            }
            else
            {
                category.Image = "no-img.png";
            }

            context.CategoryFactory.Insert(category);
            return RedirectToAction("Categories");
        }

        public ActionResult DeleteCategory(int id = 0)
        {
            Category category = context.CategoryFactory.Get(id);

            if (category.Image != "no-img.png")
            {
                string rootPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Categories/" + category.Image;
                System.IO.File.SetAttributes(rootPath + savePath, FileAttributes.Normal);
                System.IO.File.Delete(rootPath + savePath);
            }

            context.CategoryFactory.Delete(id);
            return RedirectToAction("Categories");
        }
        #endregion

        #region Products
        public ActionResult Products()
        {
            List<Product> products = context.ProductFactory.GetAll();
            return View(products);
        }

        public ActionResult AddProduct()
        {
            ViewBag.Categories = context.CategoryFactory.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, string price, HttpPostedFileBase imageFile)
        {
            if (imageFile?.ContentLength > 0)
            {
                product.Image = imageFile.FileName;
                string rootPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Products/" + imageFile.FileName;
                imageFile.SaveAs(rootPath + savePath);
            }
            else
            {
                product.Image = "no-img.png";
            }

            if (price != null)
            {
                product.Price = decimal.Parse(price.Replace(".", ","));
            }

            context.ProductFactory.Insert(product);

            return RedirectToAction("Products");
        }

        public ActionResult EditProduct(int id = 0)
        {
            ViewBag.Categories = context.CategoryFactory.GetAll();
            Product product = context.ProductFactory.Get(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, string price, HttpPostedFileBase imageFile)
        {
            if (imageFile?.ContentLength > 0)
            {
                product.Image = imageFile.FileName;
                string rootPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Products/" + imageFile.FileName;
                imageFile.SaveAs(rootPath + savePath);
            }

            if (price != null)
            {
                product.Price = decimal.Parse(price.Replace(".", ","));
            }

            context.ProductFactory.Update(product);

            return RedirectToAction("Products");
        }

        public ActionResult DeleteProduct(int id = 0)
        {
            Product product = context.ProductFactory.Get(id);

            if (product.Image != "no-img.png")
            {
                string rootPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Products/" + product.Image;
                System.IO.File.SetAttributes(rootPath + savePath, FileAttributes.Normal);
                System.IO.File.Delete(rootPath + savePath);
            }

            context.ProductFactory.Delete(id);

            return RedirectToAction("Products");
        }
        #endregion

        #region Contact
        public ActionResult EditContact()
        {
            Contact contact = context.ContactFactory.Get(1);
            return View(contact);
        }

        [HttpPost]
        public ActionResult EditContact(Contact contact, HttpPostedFileBase imageFile)
        {
            if (imageFile?.ContentLength > 0)
            {
                contact.Image = imageFile.FileName;
                string rootPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Contact/" + imageFile.FileName;
                imageFile.SaveAs(rootPath + savePath);
            }

            context.ContactFactory.Update(contact);
            return RedirectToAction("EditContact");
        }
        #endregion

        #region Login Logout
        [AllowAnonymous]
        public ActionResult Login(string returnurl)
        {
            TempData["ReturnUrl"] = returnurl;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken, AllowAnonymous]
        public ActionResult Login(string email, string password)
        {
            User user = context.UserFactory.SqlQuery($"SELECT * FROM [User] WHERE Email='{email}' AND Password='{AutoFactory<User>.GenerateSHA512Hash(password)}'");

            if (user?.ID > 0)
            {
                FormsAuthentication.SetAuthCookie(email, false);
                string returnUrl = TempData["ReturnUrl"]?.ToString() ?? "Index";
                return Redirect(returnUrl);
            }

            return View();
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        #endregion
    }
}