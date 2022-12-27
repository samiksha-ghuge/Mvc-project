using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Sockets;
using System.Security.Policy;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.UI.WebControls;
using uts.plus.Models;

namespace uts.plus.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(logintab log, string ReturnUrl="")
        {
            string message = "";

            using (utsplusEntities db = new utsplusEntities())
            {
                if (log.EmailId == null)
                {
                   return View();
                }

                var v = db.utsplus.Where(a => a.EmailId == log.EmailId).FirstOrDefault();
               
                if(v!= null)
                {
                    if (log.Password == null)
                    {
                        return View();
                    }
                    if (string.Compare(Crypto.Hash(log.Password),v.Password)==0)
                    {
                        int timeout = log.RememberMe ? 525600 : 20;
                        var ticket = new FormsAuthenticationTicket(log.EmailId, log.RememberMe, timeout);
                        string encrypted = FormsAuthentication.Encrypt(ticket);
                        var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                        cookie.Expires = DateTime.Now.AddMinutes(timeout);
                        cookie.HttpOnly = true;
                        Response.Cookies.Add(cookie);
                        if (Url.IsLocalUrl(ReturnUrl))
                        {
                            return Redirect(ReturnUrl);
                        }
                        else
                        {
                            return RedirectToAction("loginsuccessp", "Home");
                        }
                    }
                    else
                    {
                        message = "Invalid credentials";
                    }

                }
                else
                {
                    message = "Invalid credentials";
                }
            }
            ViewBag.Message = message;
            return View();
        }
        [Authorize]
        public ActionResult loginsuccessp()
        {
            return View();
         }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        public ActionResult Registration()
        {
            /*ViewBag.Status2 = true;*/
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(utsplu uts)
        {
                string message = "";
                bool Status = false;
            if (ModelState.IsValid)
            {
              

                    var isExist = isEmailExists(uts.EmailId);
                    if (isExist)
                    {
                        ModelState.AddModelError("EmailExist", "Email already exists!");
                        return View(uts);
                    }
               
                    uts.Password = Crypto.Hash(uts.Password);
                    uts.ConfirmPassword = Crypto.Hash(uts.ConfirmPassword);
                    using (utsplusEntities db = new utsplusEntities())
                    {

                        db.utsplus.Add(uts);
                        db.SaveChanges();
                        message = "Registration Successful";
                        Status = true;
                    }
              }
            else
            {
                message = "Invalid Request";
            }
                ViewBag.Message = message;
                ViewBag.Status = Status;
           /* return RedirectToAction("Index", "Account");*/
           return View();
            

        }
        public ActionResult ForgetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ForgetPassword(forgetpass forgetpass)
        {
            string message = "";
            if (forgetpass.Email == null)
            {


            }
            else
            {
                using (utsplusEntities db = new utsplusEntities())
                {
                    var v = db.utsplus.Any(x => x.EmailId == forgetpass.Email);
                    if (v != false)
                    {

                        message = "email exists go to reset password";
                        /*ViewBag = message;*/

                    }
                    else
                    {
                        message = "email does not exist";
                    }
                }

            }


            ViewBag.Message = message;
            return View();
        }
        public ActionResult ResetPassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ResetPassword(PasswordDetail details)
        {
            string message = "";
            using (utsplusEntities db = new utsplusEntities())
            {
                var v = db.utsplus.Where(a => a.EmailId == details.EmailID).FirstOrDefault();
                if (v != null)
                {
                    v.Password = details.Password;
                    db.SaveChanges();
                    message = "Your password has been successfully Reset!!!";
                }



            }
            ViewBag.Message = message;

            return View();
        }
        [NonAction]
        public bool isEmailExists(string emailid)
        {
            using(utsplusEntities db = new utsplusEntities())
            {
                var v = db.utsplus.Where(a=>a.EmailId==emailid).FirstOrDefault();
                return v!= null;
            }

        }
       
    }
}