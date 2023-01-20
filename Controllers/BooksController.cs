using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LibrarySystem;

namespace LibrarySystem.Controllers
{
    public class BooksController : Controller
    {
        private library_system db = new library_system();

        // GET: Books
        public ActionResult Index()
        {
            var books = db.Books.Include(b => b.Author).Include(b => b.Category).Include(b => b.SubCategory);
            return View(books.ToList());
        }

        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "AuthorName");
            ViewBag.MainCategoryId = new SelectList(db.Categories, "Id", "CategoryName");
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategory1");
            return View();
        }
        public ActionResult GetSubCategory(int CategoryId)
        {
            var subList=db.SubCategories.Where(s => s.CategoryId == CategoryId).ToList();
            ViewBag.SubCategoryId= new SelectList(subList, "Id", "SubCategory1");
            return PartialView("SubCategoryView", subList);
        }

        // POST: Books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Description,AuthorId,MainCategoryId,SubCategoryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Books.Add(book);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "AuthorName", book.AuthorId);
            ViewBag.MainCategoryId = new SelectList(db.Categories, "Id", "CategoryName", book.MainCategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategory1", book.SubCategoryId);
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "AuthorName", book.AuthorId);
            ViewBag.MainCategoryId = new SelectList(db.Categories, "Id", "CategoryName", book.MainCategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategory1", book.SubCategoryId);
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Description,AuthorId,MainCategoryId,SubCategoryId")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.AuthorId = new SelectList(db.Authors, "Id", "AuthorName", book.AuthorId);
            ViewBag.MainCategoryId = new SelectList(db.Categories, "Id", "CategoryName", book.MainCategoryId);
            ViewBag.SubCategoryId = new SelectList(db.SubCategories, "Id", "SubCategory1", book.SubCategoryId);
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
