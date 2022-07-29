
using Personnel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MySearchingProjet.Controllers
{
    public class HomeController : Controller
    {
        db_applicationContext dc = new db_applicationContext();
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string email, string pwd)
        {
            try
            {
                var checkUserEmail = dc.professeurs.Where(a => a.email == email).FirstOrDefault();
                if (checkUserEmail != null)
                {
                    var takepassword = BCrypt.Net.BCrypt.Verify(pwd, checkUserEmail.pwd);
                    if (takepassword)
                    {
                        Session["Prof"] = checkUserEmail.id_professeur;
                        ViewBag.sms = "* Connexion reussie *";
                        return RedirectToAction("ProfilProf", "Professeur");                      

                    }
                    else
                    {
                        ViewBag.sms = "* Mot de passe incorrect *";
                        return View();
                    }

                }
                else
                {
                    ViewBag.sms = "* Ce compte n'existe pas * ";
                    return View();
                }

            }
            catch (Exception)
            {

                ViewBag.sms = "* Impossible de vous connecter* ";
                return View();
            }
        }
        public ActionResult Deconnexion()
        {
            Session["Prof"] = null;
            return RedirectToAction("ListProfesseur", "Professeur");
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
    }
}