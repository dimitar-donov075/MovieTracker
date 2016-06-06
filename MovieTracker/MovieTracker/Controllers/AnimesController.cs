using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MovieTracker.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;

namespace MovieTracker.Controllers
{
    [Authorize]
    public class AnimesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /Animes/
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var animesByUserId = db.Animes.Where(c => c.ApplicationUserId.Id == id);
            return View(animesByUserId.ToList());
        }

        // GET: /Animes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return View(anime);
        }

        // GET: /Animes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Animes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Title,Genre,Season,Episode")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                var id = User.Identity.GetUserId();
                anime.ApplicationUserId = db.Users.Find(id);
                db.Animes.Add(anime);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(anime);
        }

        // GET: /Animes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return View(anime);
        }

        // POST: /Animes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Title,Genre,Season,Episode")] Anime anime)
        {
            if (ModelState.IsValid)
            {
                db.Entry(anime).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(anime);
        }

        // GET: /Animes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Anime anime = db.Animes.Find(id);
            if (anime == null)
            {
                return HttpNotFound();
            }
            return View(anime);
        }

        // POST: /Animes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Anime anime = db.Animes.Find(id);
            db.Animes.Remove(anime);
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
