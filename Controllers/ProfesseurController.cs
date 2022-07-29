using Personnel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PagedList;
using System.Web.Mvc;
using System.IO;
namespace MySearchingProjet.Controllers
{
    public class ProfesseurController : Controller
    {
        db_applicationContext dc = new db_applicationContext();
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UploadFile(HttpPostedFileBase file)
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var profConnected = dc.professeurs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    string path = Server.MapPath("~/Photos/" + profConnected.photo);
                    FileInfo fileExist = new FileInfo(path);
                    if (fileExist.Exists)//check file exsit or not  
                    {
                        fileExist.Delete();   
                        
                    }               
                    if (file.ContentLength > 0)
                    {
                        string _FileName =Path.GetFileName(file.FileName);
                        string _FileNameNew =profConnected.id_professeur+"-"+ _FileName.ToString();
                        string _path = Path.Combine(Server.MapPath("~/Photos"), _FileNameNew);
                        file.SaveAs(_path);
                        profConnected.photo = _FileNameNew;
                        dc.professeurs.Add(profConnected);
                        dc.Entry(profConnected).State = System.Data.Entity.EntityState.Modified;
                        dc.SaveChanges();   
                    }
                       return RedirectToAction("ProfilProf", "Professeur");

                }
                else
                {
                    return RedirectToAction("Index", "Professeur");

                }



            }
            catch
            {
                return RedirectToAction("ProfilProf","Professeur");
            }
        }
    

        [HttpGet]
        public ActionResult Statistiques()
        {
            return View(); 
        }
        [HttpGet]
        public ActionResult VoirStatistiques()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ChoixProfesseur()
        {
            return View();
        }
          
        [HttpGet]
        public ActionResult ProfilProf()
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    ViewBag.ProfConnected = dc.professeurs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();  
                    return View();
                }
                else
                {
                    return RedirectToAction("index", "Home");
                }
            }
            catch (Exception)
            {
                return RedirectToAction("Index", "Home");
            }
        }
        [HttpGet]
        public ActionResult VoirCvProfesseur(long? Professeur)
        {
            try
            {
                if (Professeur != null)
                {
                    ViewBag.userConnected = dc.professeurs.Where(a => a.id_professeur == Professeur && a.status == true).FirstOrDefault();
                    var moncv = dc.cvs.Where(a => a.id_professeur == Professeur && a.status == true).FirstOrDefault();
                    var autresFormation = dc.autres_experiences.Where(a => a.id_cv == moncv.id_cv_Prof).ToList();
                    var experienceProfessionnelle = dc.exprerience_professionnelles.Where(a => a.id_cv == moncv.id_cv_Prof).ToList();
                    var formationEtude = dc.formation_etudes.Where(a => a.id_cv == moncv.id_cv_Prof).ToList();
                    var tpl = new Tuple<Cv_Prof, List<exprerience_professionnelle>, List<formation_etude>, List<autres_experience>>(moncv, experienceProfessionnelle, formationEtude, autresFormation);
                    return View(tpl);
                }
                else
                {
                    return RedirectToAction("ListProfesseur", "Professeur");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("CreerCvProfesseur", "Professeur");
            }
        }
        [HttpGet]
        public ActionResult CreerProfesseur()
        {
            return View();
        }
        [HttpGet]
        public ActionResult CreerCvProfesseur()
        {

            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var moncv = dc.cvs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    var autresFormation = dc.autres_experiences.Where(a => a.id_cv == moncv.id_cv_Prof).ToList();
                    var experienceProfessionnelle = dc.exprerience_professionnelles.Where(a => a.id_cv == moncv.id_cv_Prof).ToList();
                    var formationEtude = dc.formation_etudes.Where(a => a.id_cv == moncv.id_cv_Prof).ToList();
                    var tpl = new Tuple<Cv_Prof,List<exprerience_professionnelle>,List<formation_etude>, List<autres_experience>>(moncv,experienceProfessionnelle, formationEtude, autresFormation);
                    return View(tpl);
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("CreerCvProfesseur", "Professeur");
            }
        }
        [HttpPost]
        public ActionResult CreerProfesseur(string Nom, string Prenom, string MatiereDispensee1, string MatiereDispensee2, string MatiereDispensee3, string Adresse, string Pwd, string Telephone,string Email, Professeur utl,Cv_Prof monCV)
        {
            try
            {
                
                    long ph = Convert.ToInt64("221" + Telephone);
                    var checkphone = dc.professeurs.Where(a => a.telephone == ph).Select(a => a.telephone).FirstOrDefault();
                    var checkEmail = dc.professeurs.Where(a => a.email == Email).Select(a => a.email).FirstOrDefault();
                    var sizeTableProfesseurBefore = dc.professeurs.Count();

                    if (checkphone.Equals(ph))
                    {
                        ViewBag.sms = "* Ce numéro de téléphone existe déja *";
                        ViewBag.Nom = Nom;
                        ViewBag.Prenom = Prenom;
                        ViewBag.Adresse = Prenom;
                        ViewBag.Email = Email;

                        return View();
                    }
                    else if (checkEmail != null)
                    {
                        ViewBag.sms = "* Cette adresse mail existe déja *";
                        ViewBag.Nom = Nom;
                        ViewBag.Prenom = Prenom;
                        ViewBag.Adresse = Adresse;
                        ViewBag.Email = Email;

                    return View();
                    }                    
                    else
                    {

                        utl.nom = Nom;
                        utl.prenom = Prenom;
                        utl.status = true;
                        utl.telephone = ph;
                        utl.adresse = Adresse;          
                        utl.email = Email;
                        utl.date_creation = DateTime.Now;
                        utl.matiere_dispense_1 = MatiereDispensee1;
                        utl.matiere_dispense_2 = MatiereDispensee2; 
                        utl.matiere_dispense_3 = MatiereDispensee3;                        
                        utl.pwd = BCrypt.Net.BCrypt.HashPassword(Pwd);
                        var profCreate=dc.professeurs.Add(utl);
                        dc.SaveChanges();
                        var sizeTableProfesseurAfter = dc.professeurs.Count();
                        if(sizeTableProfesseurBefore < sizeTableProfesseurAfter)
                        {
                        monCV.id_professeur = profCreate.id_professeur;
                        monCV.status = true;
                        monCV.date_creation = DateTime.Now;
                        dc.cvs.Add(monCV);
                        dc.SaveChanges();
                        return RedirectToAction("ListProfesseur", "Professeur");

                        }
                        else
                        {
                        dc.professeurs.Remove(profCreate);
                        dc.SaveChanges();
                        return View();
                        }
                      


                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return View();
            }
        }

        [HttpPost]
        public ActionResult ModifierProfesseur(string Nom, string Prenom, string MatiereDispensee1, string MatiereDispensee2, string MatiereDispensee3, string Adresse, string Telephone, string Email)
        {
            try
            {

                long ph = Convert.ToInt64("221" + Telephone);
                long ID = Convert.ToInt64(Session["Prof"]);
                var checkProfphone = dc.professeurs.Where(a => a.telephone == ph).FirstOrDefault();
                var checkProfEmail = dc.professeurs.Where(a => a.email == Email).FirstOrDefault();
                var utl = dc.professeurs.Where(a => a.id_professeur== ID && a.status==true).FirstOrDefault();

                if (checkProfphone !=null && checkProfphone.id_professeur !=utl.id_professeur)
                {
                    ViewBag.sms = "* Ce numéro de téléphone existe déja *";
                    ViewBag.Nom = Nom;
                    ViewBag.Prenom = Prenom;
                    ViewBag.Adresse = Prenom;
                    ViewBag.Email = Email;

                    return View();
                }
                else if (checkProfEmail != null && checkProfEmail.id_professeur != utl.id_professeur)
                {
                    ViewBag.sms = "* Cette adresse mail existe déja *";
                    ViewBag.Nom = Nom;
                    ViewBag.Prenom = Prenom;
                    ViewBag.Adresse = Adresse;
                    ViewBag.Email = Email;

                    return View();
                }
                else
                {

                    utl.nom = Nom;
                    utl.prenom = Prenom;
                    utl.status = true;
                    utl.telephone = ph;
                    utl.adresse = Adresse;
                    utl.email = Email;
                    utl.matiere_dispense_1 = MatiereDispensee1;
                    utl.matiere_dispense_2 = MatiereDispensee2;
                    utl.matiere_dispense_3 = MatiereDispensee3;
                    dc.professeurs.Add(utl);
                    dc.Entry(utl).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                    return RedirectToAction("ListProfesseur", "Professeur");
                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return View();
            }
        }

        [HttpPost]
        public ActionResult AproposProfesseur(string propos)
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var utl = dc.cvs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    utl.apropos = propos;
                    dc.cvs.Add(utl);
                    dc.Entry(utl).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                    return RedirectToAction("CreerCvProfesseur", "Professeur");
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("CreerCvProfesseur", "Professeur");
            }
        }

        [HttpPost]
        public ActionResult LangueProfesseur(string langue)
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var utl = dc.cvs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    utl.langues = langue;
                    dc.cvs.Add(utl);
                    dc.Entry(utl).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                    return RedirectToAction("CreerCvProfesseur", "Professeur");
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("CreerCvProfesseur", "Professeur");
            }
        }

        [HttpPost]
        public ActionResult CentreInteretProfesseur(string centre_interet)
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var utl = dc.cvs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    utl.centre_interet = centre_interet;
                    dc.cvs.Add(utl);
                    dc.Entry(utl).State = System.Data.Entity.EntityState.Modified;
                    dc.SaveChanges();
                    return RedirectToAction("CreerCvProfesseur", "Professeur");
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("CreerCvProfesseur", "Professeur");
            }
        }
        [HttpGet]
        public ActionResult DeleteExperienceProfessionnelle(long? id)
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var experienceProfessionnelle = dc.exprerience_professionnelles.Where(a => a.id_exprerience_professionnelle == id && a.id_cv == ID).FirstOrDefault();
                    dc.exprerience_professionnelles.Remove(experienceProfessionnelle);
                    dc.SaveChanges();
                    return RedirectToAction("CreerCvProfesseur", "Professeur");
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("CreerCvProfesseur", "Professeur");
            }
        }

        [HttpPost]
        public ActionResult ExperienceProfessionnelle(string description,DateTime anneeDebut,DateTime anneeFin, string Titre, exprerience_professionnelle exp_prof)
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var moncv = dc.cvs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    exp_prof.annee_debut = anneeDebut;
                    exp_prof.annee_fin = anneeFin;
                    exp_prof.date_creation = DateTime.Now;
                    exp_prof.description = description;
                    exp_prof.titre = Titre;
                    exp_prof.id_cv = moncv.id_cv_Prof;
                    dc.exprerience_professionnelles.Add(exp_prof);
                    dc.SaveChanges();
                    return RedirectToAction("CreerCvProfesseur", "Professeur");
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("ListProfesseur", "Professeur");
            }
        }

        [HttpGet]
        public ActionResult DeleteEtudeFormation(long? id)
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var formation_etude = dc.formation_etudes.Where(a => a.id_formation_etude == id && a.id_cv == ID).FirstOrDefault();
                    dc.formation_etudes.Remove(formation_etude);
                    dc.SaveChanges();
                    return RedirectToAction("CreerCvProfesseur", "Professeur");
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("CreerCvProfesseur", "Professeur");
            }
        }

        [HttpPost]
        public ActionResult EtudeFormation(string description, DateTime anneeDebut, DateTime anneeFin, formation_etude formation_prof)
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var moncv = dc.cvs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    formation_prof.annee_debut = anneeDebut;
                    formation_prof.annee_fin = anneeFin;
                    formation_prof.date_creation = DateTime.Now;
                    formation_prof.description = description;
                    formation_prof.id_cv = moncv.id_cv_Prof;
                    dc.formation_etudes.Add(formation_prof);
                    dc.SaveChanges();
                    return RedirectToAction("CreerCvProfesseur", "Professeur");
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("ListProfesseur", "Professeur");
            }
        }
       
        [HttpPost]
        public ActionResult AutreExperience(string description, DateTime anneeDebut, DateTime anneeFin, autres_experience autres_experiences)
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var moncv = dc.cvs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    autres_experiences.annee_debut = anneeDebut;
                    autres_experiences.annee_fin = anneeFin;
                    autres_experiences.date_creation = DateTime.Now;
                    autres_experiences.description = description;
                    autres_experiences.id_cv = moncv.id_cv_Prof;
                    dc.autres_experiences.Add(autres_experiences);
                    dc.SaveChanges();
                    return RedirectToAction("CreerCvProfesseur", "Professeur");
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("ListProfesseur", "Professeur");
            }
        }
        [HttpGet]
        public ActionResult DeleteAutreExperience(long? id)
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var autre_experience = dc.autres_experiences.Where(a => a.id_autres_experience == id && a.id_cv == ID).FirstOrDefault();
                    dc.autres_experiences.Remove(autre_experience);
                    dc.SaveChanges();
                    return RedirectToAction("CreerCvProfesseur", "Professeur");
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("CreerCvProfesseur", "Professeur");
            }
        }

        [HttpGet]
        public ActionResult CvProfesseur()
        {
            try
            {
                if (Session["Prof"] != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    ViewBag.userConnected = dc.professeurs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    var moncv = dc.cvs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    var autresFormation = dc.autres_experiences.Where(a => a.id_cv == moncv.id_cv_Prof).ToList();
                    var experienceProfessionnelle = dc.exprerience_professionnelles.Where(a => a.id_cv == moncv.id_cv_Prof).ToList();
                    var formationEtude = dc.formation_etudes.Where(a => a.id_cv == moncv.id_cv_Prof).ToList();
                    var tpl = new Tuple<Cv_Prof, List<exprerience_professionnelle>, List<formation_etude>, List<autres_experience>>(moncv, experienceProfessionnelle, formationEtude, autresFormation);
                    return View(tpl);
                }
                else
                {
                    return RedirectToAction("Login", "Home");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible d'enregistrer cet utilisateur*";
                return RedirectToAction("CreerCvProfesseur", "Professeur");
            }
        }
        [HttpGet]
        public ActionResult Contacts(int? page, string searching)
        {
            int pageSize = 10;
            int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            long ID = Convert.ToInt64(Session["Prof"]);
            var listesContacts = dc.contacts.Where(a => a.id_professeur == ID && a.Nom.StartsWith(searching)
                                                            || a.id_professeur == ID && a.Prenom.StartsWith(searching)
                                                            || a.id_professeur == ID && searching == null)
                                                           .OrderByDescending(a => a.id_professeur).ToPagedList(pageIndex, pageSize);
            return View(listesContacts);
        }

        [HttpPost]
        public ActionResult AddContact(long? idProfesseur,string Nom, string Prenom, string Email, string Telephone,string Titre,Contact ct)
        {
            try
            {
                if (idProfesseur != null)
                {
                    long ID = Convert.ToInt64(Session["Prof"]);
                    var moncv = dc.cvs.Where(a => a.id_professeur == ID && a.status == true).FirstOrDefault();
                    ct.date_creation = DateTime.Now;
                    ct.Email = Email;
                    ct.Nom = Nom;
                    ct.Prenom = Prenom;
                    ct.Titre = Titre;
                    ct.id_professeur = idProfesseur;
                    dc.contacts.Add(ct);
                    dc.SaveChanges();
                    return RedirectToAction("PageContactProfesseur", "Professeur",new {idProfesseur = idProfesseur});
                }
                else
                {
                    ViewBag.sms = "*impossible de trouver ce professeur*";
                    return RedirectToAction("ListProfesseur", "Professeur");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible de prendre ses contacts*";
                return RedirectToAction("ListProfesseur", "Professeur");
            }
        }
        [HttpGet]
        public ActionResult PageContactProfesseur(long? idProfesseur)
        {
            try
            {
                if (idProfesseur != null)
                {
                    var professeur = dc.professeurs.Where(a => a.id_professeur == idProfesseur && a.status == true).FirstOrDefault();
                    return View(professeur);
                }
                else
                {
                    ViewBag.sms = "*impossible d'avoir ces contacts*";
                    return RedirectToAction("ListProfesseur", "Professeur");

                }
            }
            catch (Exception)
            {
                ViewBag.sms = "*impossible de prendre ses contacts*";
                return RedirectToAction("ListProfesseur", "Professeur");
            }
        }

        [HttpGet]
        public ActionResult ListProfesseur(int? page, string searching)
        {
            int pageSize = 10;
            int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var listesProfesseurs = dc.professeurs.Where(a => a.status == true && a.nom.StartsWith(searching)
                                                            || a.status == true && a.prenom.StartsWith(searching)
                                                            || a.status == true && a.matiere_dispense_1.StartsWith(searching)
                                                            || a.status == true && a.matiere_dispense_2.StartsWith(searching)
                                                            || a.status == true && a.matiere_dispense_3.StartsWith(searching)
                                                            || a.status == true && searching == null)
                                                           .OrderByDescending(a => a.id_professeur).ToPagedList(pageIndex, pageSize);
            return View(listesProfesseurs);
        }
        public ActionResult ListProfesseurMonEspace(int? page, string searching)
        {
            int pageSize = 10;
            int pageIndex = 1; pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
            var listesProfesseurs = dc.professeurs.Where(a => a.status == true && a.nom.StartsWith(searching)
                                                            || a.status == true && a.prenom.StartsWith(searching)
                                                            || a.status == true && a.matiere_dispense_1.StartsWith(searching)
                                                            || a.status == true && a.matiere_dispense_2.StartsWith(searching)
                                                            || a.status == true && a.matiere_dispense_3.StartsWith(searching)
                                                            || a.status == true && searching == null)
                                                           .OrderByDescending(a => a.id_professeur).ToPagedList(pageIndex, pageSize);
            return View(listesProfesseurs);
        }
    }
}