using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Library_Management_System.Controllers
{
    public class AssignbooksController : Controller
    {
        private LibraryDataBaseEntities1 db = new LibraryDataBaseEntities1();

        public ActionResult AssignBook()
        {
            var sss = db.tbl_dashbord.ToList();
            return View();
        }

        public ActionResult IsueBook()
        {
            return Json(db.tbl_dashbord.Select(x => new
            {
                ID = x.ID,
                BookName = x.BookName,
                Author    =x.Author
            }).ToList(), JsonRequestBehavior.AllowGet);;;
        }
        [HttpPost]
        public ActionResult AssignBook(tbl_dashbord tbl_Dashbord)
        {
            int val = Convert.ToInt32(tbl_Dashbord.BookName);
            tbl_Dashbord.ID = val;
            int val1 = Convert.ToInt32(tbl_Dashbord.Author);
            tbl_Dashbord.ID = val1;
            tbl_dashbord tbl_Dashbord1 = db.tbl_dashbord.Find(tbl_Dashbord.ID);
            tbl_Dashbord1.DateOfIssue = tbl_Dashbord.DateOfIssue;
            tbl_Dashbord1.DateOfReturn = tbl_Dashbord.DateOfReturn;
            tbl_Dashbord1.Date = DateTime.Now;
            tbl_Dashbord1.Fine = tbl_Dashbord.Fine;
            tbl_Dashbord1.IssureName = tbl_Dashbord.IssureName;
            db.Entry(tbl_Dashbord1).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("GetLibrary","Home");

        }

    }
}
