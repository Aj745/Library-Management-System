using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    public class HomeController : Controller
    {

       



        HttpClient st = new HttpClient();
        private LibraryDataBaseEntities1 db = new LibraryDataBaseEntities1();

        // GET: Library
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]

        public ActionResult verify(User libLog)
        {

            var Login = db.Users.Where(a => a.UserName.Equals(libLog.UserName) && a.Password.Equals(libLog.Password)).FirstOrDefault();
            if (Login != null)
            {
                Session["UserName"] = libLog.UserName.ToString();
                Session["Password"] = libLog.Password.ToString();
                return RedirectToAction("GetLibrary");
            }
            else
            {

                return View("Errorr");
            }
        }

        public ActionResult Errorr()
        {
            return View();

        }


        [HttpGet]
        public ActionResult GetLibrary()
        {
            IEnumerable<tbl_dashbord> cities = null;
            using (var clients = new HttpClient())
            {
                clients.BaseAddress = new Uri("https://localhost:44308");
                //HTTP GET

                var responseTask = clients.GetAsync("/Api/ApiLibrary/GetLibrary");
                responseTask.Wait();
                var results = responseTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    var readTask = results.Content.ReadAsAsync<IList<tbl_dashbord>>();
                    readTask.Wait();
                    cities = readTask.Result;
                }
                else //web api sent error response 
                {
                    //log response status here..
                    cities = Enumerable.Empty<tbl_dashbord>();
                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View(cities);
            }

        }
        public ActionResult AddBook()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddBook(tbl_dashbord st)
        {

            using (var clients = new HttpClient())
            {
                clients.BaseAddress = new Uri("https://localhost:44308/Api/");
                //HTTP POST
                var postTask = clients.PostAsJsonAsync<tbl_dashbord>("ApiLibrary", st);
                postTask.Wait();
                var results = postTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View(st);
            }

        }
        [HttpPost]
        public ActionResult DeleteBook(tbl_dashbord st)
        {

            using (var clients = new HttpClient())
            {
                clients.BaseAddress = new Uri("https://localhost:44308/Api/");
                //HTTP POST
                var postTask = clients.DeleteAsync("ApiLibrary/" + st.ID.ToString());
                postTask.Wait();
                var results = postTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "Home");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return RedirectToAction("Index", "Home");
            }
        }
        
        public ActionResult Edit (int id)
        {
            using (var clients = new HttpClient())
            {
                clients.BaseAddress = new Uri("https://localhost:44308/Api/");
                //HTTP POST
                var postTask = clients.GetAsync("ApiLibrary"+ id.ToString());
                postTask.Wait();
                var results = postTask.Result;
                if (results.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else //web api sent error response 
                {
                    //log response status here..

                    ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
                }
                return View(id);
            }
        }
 
        [HttpPost]
        public ActionResult Edit(tbl_dashbord bk)
        {

            HttpClient clients = new HttpClient();
            clients.BaseAddress = new Uri("https://localhost:44308/Api/");
            var postTask = clients.PutAsJsonAsync<tbl_dashbord>("ApiLibrary/", bk);
            postTask.Wait();
            var results = postTask.Result;
            if (results.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            else //web api sent error response 
            {
                //log response status here..

                ModelState.AddModelError(string.Empty, "Server error. Please contact administrator.");
            }
            return View(bk);
        }

    }
}
  