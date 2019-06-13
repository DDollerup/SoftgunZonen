using SoftgunZonen.Factories;
using SoftgunZonen.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SoftgunZonen.Areas.Admin.Controllers
{
    [Authorize]
    public class CMSController : Controller
    {
        DBContext context = DBContext.Instance;
        public ActionResult Index()
        {
            return View(context.ContactMessageFactory.GetAll().Where(x => x.Read == false).ToList());
        }

        #region Pages
        public ActionResult EditIndex()
        {
            return View(context.PageFactory.Get(1));
        }

        [HttpPost]
        public ActionResult EditIndex(Page page)
        {
            context.PageFactory.Update(page);
            TempData["SYS_MSG"] = "Index has been updated.";
            return View(page);
        }

        public ActionResult EditStore()
        {
            return View(context.PageFactory.Get(2));
        }

        [HttpPost]
        public ActionResult EditStore(Page page, HttpPostedFileBase imageFile)
        {
            // contentlength = filens størrelse i bytes
            if (imageFile?.ContentLength > 0)
            {
                page.Image = imageFile.FileName;
                string appPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Pages/";
                imageFile.SaveAs(appPath + savePath + imageFile.FileName);
            }

            context.PageFactory.Update(page);
            TempData["SYS_MSG"] = "Store has been updated";
            return View(page);
        } 
        #endregion

        #region Categories
        public ActionResult Categories()
        {
            return View(context.CategoryFactory.GetAll());
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
                string appPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Categories/";
                imageFile.SaveAs(appPath + savePath + imageFile.FileName);
            }
            else
            {
                category.Image = "no-img.png";
            }

            context.CategoryFactory.Insert(category);
            TempData["SYS_MSG"] = "Category has been added";
            return RedirectToAction("Categories");
        }

        // id er Category.ID
        public ActionResult EditCategory(int id = 0)
        {
            return View(context.CategoryFactory.Get(id));
        }

        [HttpPost]
        public ActionResult EditCategory(Category category, HttpPostedFileBase imageFile)
        {
            if (imageFile?.ContentLength > 0)
            {
                category.Image = imageFile.FileName;
                string appPath = Request.PhysicalApplicationPath;
                string savePath = @"/Content/Images/Categories/";
                imageFile.SaveAs(appPath + savePath + imageFile.FileName);
            }

            context.CategoryFactory.Update(category);
            TempData["SYS_MSG"] = "Category has been updated";
            return RedirectToAction("Categories");
        }

        public ActionResult DeleteCategory(int id = 0)
        {
            context.CategoryFactory.Delete(id);
            TempData["SYS_MSG"] = "Category has been deleted";
            return RedirectToAction("Categories");
        } 
        #endregion

        #region Products
        public ActionResult Products()
        {
            return View(context.ProductFactory.GetAll());
        }

        public ActionResult AddProduct()
        {
            ViewBag.Categories = context.CategoryFactory.GetAll();
            return View();
        }

        [HttpPost]
        public ActionResult AddProduct(Product product, HttpPostedFileBase imageFile)
        {
            if (Upload.Image(imageFile, Request.PhysicalApplicationPath + @"/Content/Images/Products/", out string fileName, 530))
            {
                product.Image = fileName;
            }
            else
            {
                product.Image = "no-img.png";
            }

            context.ProductFactory.Insert(product);
            TempData["SYS_MSG"] = "Product has been added";
            return RedirectToAction("Products");
        }

        // id er Product.ID
        public ActionResult EditProduct(int id = 0)
        {
            ViewBag.Categories = context.CategoryFactory.GetAll();
            return View(context.ProductFactory.Get(id));
        }

        [HttpPost]
        public ActionResult EditProduct(Product product, HttpPostedFileBase imageFile)
        {
            if (Upload.Image(imageFile, Request.PhysicalApplicationPath + @"/Content/Images/Products/", out string fileName, 530))
            {
                product.Image = fileName;
            }

            context.ProductFactory.Update(product);
            TempData["SYS_MSG"] = "Product has been updated";
            return RedirectPermanent("Products");
        }

        //id = Product.ID
        public ActionResult DeleteProduct(int id = 0)
        {
            Product product = context.ProductFactory.Get(id);
            string filePath = Request.PhysicalApplicationPath + @"/Content/Images/Products/" + product.Image;
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }

            context.ProductFactory.Delete(id);

            TempData["SYS_MSG"] = "Product has been deleted";
            return RedirectToAction("Products");
        }
        #endregion

        #region Members
        public ActionResult Members()
        {
            return View(context.MemberFactory.GetAll());
        }

        // id = Member.ID
        public ActionResult EditMember(int id = 0)
        {
            ViewBag.MemberRoles = context.MemberRoleFactory.GetAll();
            return View(context.MemberFactory.Get(id));
        }

        [HttpPost]
        public ActionResult EditMember(Member member, string newPassword)
        {
            context.MemberFactory.Update(member);
            TempData["SYS_MSG"] = "Member has been updated";
            return RedirectToAction("Members");
        }

        public ActionResult DeleteMember(int id = 0)
        {
            context.MemberFactory.Delete(id);
            TempData["SYS_MSG"] = "Member has been deleted";
            return RedirectToAction("Members");
        }
        #endregion

        #region ContactMessages
        public ActionResult ContactMessages()
        {
            return View(context.ContactMessageFactory.GetAll());
        }

        public ActionResult EditContactMessage(int id = 0)
        {
            return View(context.ContactMessageFactory.Get(id));
        }

        [HttpPost]
        public ActionResult EditContactMessage(ContactMessage contactMessage)
        {
            contactMessage.Read = true;
            context.ContactMessageFactory.Update(contactMessage);

            TempData["SYS_MSG"] = "Contact Message has been read";
            return RedirectToAction("ContactMessages");
        }
        #endregion

        #region Login Logout

        [AllowAnonymous]
        public ActionResult Login(string returnurl)
        {
            TempData["ReturnURL"] = returnurl;
            return View();
        }

        [AllowAnonymous, HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            User user = context.UserFactory.Login(email, password);

            if (user?.ID > 0)
            {
                FormsAuthentication.SetAuthCookie(email, false);
                string returnUrl = TempData["ReturnURL"]?.ToString() ?? "Index";
                return Redirect(returnUrl);
            }

            TempData["SYS_MSG"] = "Wrong username or password";

            return View();
        }
        #endregion
    }
}