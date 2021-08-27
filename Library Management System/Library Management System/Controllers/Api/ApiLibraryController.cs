using Library_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Library_Management_System.Controllers.Api
{
    public class ApiLibraryController : ApiController
    {
        private LibraryDataBaseEntities1 db = new LibraryDataBaseEntities1();
        public ApiLibraryController()
        {

        }
        [HttpGet]
        public HttpResponseMessage GetLibrary()
        {
            var item = db.tbl_dashbord.ToList();
            if (item == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Request.CreateResponse(HttpStatusCode.OK, item);
        }

        [HttpPost]
        public HttpResponseMessage AddBook(tbl_dashbord st)
        {

            st.Date = DateTime.Now;
            db.tbl_dashbord.Add(st);
            db.SaveChanges();
            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, st);
            return response;
        }
        [HttpDelete]
        public HttpResponseMessage DeleteBook(int ID)
        {
            var Book = db.tbl_dashbord.Where(s => s.ID == ID).FirstOrDefault();

            db.Entry(Book).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();

            HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK, ID);
            return response;
        }

        public IHttpActionResult Put(tbl_dashbord bk)
        {
            var updatebook = db.tbl_dashbord.Where(x => x.ID == bk.ID).FirstOrDefault();
            if(updatebook !=null)
            {
                updatebook.ID = bk.ID;
                updatebook.BookName = bk.BookName;
                updatebook.Author = bk.Author;
                updatebook.IssureName = bk.IssureName;
                updatebook.DateOfIssue = bk.DateOfIssue;
                updatebook.DateOfReturn = bk.DateOfReturn;
                updatebook.Fine = bk.Fine;
                db.SaveChanges();

            }
            else
            {
                return NotFound();
            }
            return Ok();
        }



    

    }
}
