﻿using System;
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
        
        public ActionResult Index()
        {
            // Den opretter en variabel af typen Page og kalder den Index.
            // Vi henter vores model data gennem PageFactory hvor vi henter ud fra id 1
            Page index = context.PageFactory.Get(1);
            // Og så sender vi index variablen tilbage til viewet.
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

            ProductVM productVM = new ProductVM();
            productVM.Product = product;
            productVM.Comments = new List<CommentVM>();
            foreach (Comment item in context.CommentFactory.GetAllBy("ProductID", id))
            {
                CommentVM cvm = new CommentVM()
                {
                    Comment = item,
                    Member = context.MemberFactory.Get("Token", item.TokenKey)
                };
                productVM.Comments.Add(cvm);
            }

            return View(productVM);
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

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string email, string password)
        {
            Member member = context.MemberFactory.Login(email, password);
            if (member.ID > 0)
            {
                Session["Member"] = member;
            }
            return RedirectToAction("UserProfile");
        }

        public ActionResult Logout()
        {
            Session["Member"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult UserProfile()
        {
            if (Session["Member"] != null)
            {
                Member member = Session["Member"] as Member;
                MemberVM memberVM = new MemberVM()
                {
                    Member = member,
                    MemberRole = context.MemberRoleFactory.Get(member.MemberRoleID),
                    Comments = context.CommentFactory.GetAllBy("TokenKey", member.Token)
                };
                return View(memberVM); 
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

        public ActionResult CreateUser()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateUser(Member member)
        {
            if (context.MemberFactory.ExistsBy("Email", member.Email) == false)
            {
                member.Password = AutoFactory<Member>.GenerateSHA512Hash(member.Password);
                context.MemberFactory.Insert(member);
                return RedirectToAction("Login");
            }
            ViewBag.CreateMemberError = true;
            return View(member);
        }

        [HttpPost]
        public ActionResult CreateComment(Comment comment)
        {
            comment.DateTime = DateTime.Now;
            context.CommentFactory.Insert(comment);
            return Redirect("/Home/ShowProduct/" + comment.ProductID);
        }

        public ActionResult DeleteComment(int id = 0)
        {
            Comment comment = context.CommentFactory.Get(id);
            Member member = (Session["Member"] as Member);
            if (comment.TokenKey == member.Token || member.MemberRoleID == 2)
            {
                context.CommentFactory.Delete(id);
            }

            return Redirect(Request.UrlReferrer.PathAndQuery);
        }
    }
}