using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using uts.plus.Models;
using System.Web.Mvc;
using System.Data.Entity;
using System.Web.Helpers;
using System.Security.Cryptography;

namespace uts.plus.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        utsplusEntities db = new utsplusEntities();
        // GET: Account
        public ActionResult Index()
        {
            var info= db.utsplus.ToList();
            return View(info);
        }
        public ActionResult Delete(int id)
        {
            var info =db.utsplus.Where(x=>x.Id==id).First();
            db.utsplus.Remove(info);
            db.SaveChanges();
            var list= db.utsplus.ToList();
            return View("Index", list);
        }
        public ActionResult edit(utsplu uts)
        {
            ViewBag.Status1 = true;
           
            return View(uts);
        }
        [HttpPost]
        public ActionResult Edit(utsplu uts)
        {

            if (Convert.ToBoolean(uts.FirstName == null) || Convert.ToBoolean(uts.LastName == null) || Convert.ToBoolean(uts.EmailId == null)
                || Convert.ToBoolean(uts.PhoneNumber == null) || Convert.ToBoolean(uts.Address == null))
            {
                return View();
            }

            using (var db = new utsplusEntities())
            {
                var v = db.utsplus.Where(u => u.Id == uts.Id).FirstOrDefault();
                uts.Password = v.Password;
                uts.ConfirmPassword = v.Password;
            }
            using (utsplusEntities dsave = new utsplusEntities())
            {
                
                dsave.Entry(uts).State = EntityState.Modified;
                dsave.SaveChanges();
                return RedirectToAction("Index", "Account");
            }
           
        }
        
        public ActionResult createnew()
        {
            ViewBag.Status3 = true;
            return View("~/Views/Home/Registration.cshtml");
        }
    }
}