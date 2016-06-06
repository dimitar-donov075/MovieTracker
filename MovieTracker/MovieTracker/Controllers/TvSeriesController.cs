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
    public class TvSeriesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: /TvSeries/
        public ActionResult Index()
        {
            var id = User.Identity.GetUserId();
            var seriesByUserId = db.TvSeries.Where(c => c.ApplicationUserId.Id == id);
            return View(seriesByUserId.ToList());
        }

        // GET: /TvSeries/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.TvSeries.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // GET: /TvSeries/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /TvSeries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="Id,Title,Genre,AirDate,Season,Episode")] Series series)
        {
            if (ModelState.IsValid)
            {
                var id = User.Identity.GetUserId();
                series.ApplicationUserId = db.Users.Find(id);
                db.TvSeries.Add(series);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(series);
        }

        // GET: /TvSeries/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.TvSeries.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // POST: /TvSeries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="Id,Title,Genre,AirDate,Season,Episode")] Series series)
        {
            if (ModelState.IsValid)
            {
                db.Entry(series).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(series);
        }

        // GET: /TvSeries/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Series series = db.TvSeries.Find(id);
            if (series == null)
            {
                return HttpNotFound();
            }
            return View(series);
        }

        // POST: /TvSeries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Series series = db.TvSeries.Find(id);
            db.TvSeries.Remove(series);
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
