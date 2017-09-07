using RareVinyls.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace Rare_Vinyls.Controllers
{
    public class HomeController : Controller

    {
        public VinylsEntities5 db = new VinylsEntities5();

        public ActionResult Index(string vinylGenre, string vinylArtist, string vinylTitle)
        {
            //setting up a drop down list of all Genre
            var GenreList = new List<string>();
            var GenreQuery = from g in db.Vinyls orderby g.Genre select g.Genre;
            GenreList.AddRange(GenreQuery.Distinct());
            ViewBag.vinylGenre = new SelectList(GenreList);

            // get all vinyls
            var vinyls = from v in db.Vinyls select v;

            // setting up Artist search
            if (!string.IsNullOrEmpty(vinylGenre))
            {
                vinyls = vinyls.Where(s => s.Genre == vinylGenre);
            }
            if (!string.IsNullOrEmpty(vinylArtist))
            {
                vinyls = vinyls.Where(a => a.Artist.Contains(vinylArtist));
            }

            // setting up Title search
            if (!string.IsNullOrEmpty(vinylGenre))
            {
                vinyls = vinyls.Where(s => s.Genre == vinylGenre);
            }
            if (!string.IsNullOrEmpty(vinylTitle))
            {
                vinyls = vinyls.Where(t => t.Title.Contains(vinylTitle));
            }

            return View(vinyls);
        }


        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Add(/*[Bind(Include = "Picture, id, Title, Artist, ReleaseYear, Genre, Price, Owner_s_email, Additional_info")]*/Vinyl vinyl)
        {
            if (vinyl.Picture == null)
            {
                vinyl.Picture = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSENEvLDx-AshXMqesWzx3fZRLF_kgX-OanpfZTz-1o1P_H2Qdd";
            }
            if (ModelState.IsValid)
            {
                db.Vinyls.Add(vinyl);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vinyl vinyl = db.Vinyls.Find(id);

            if (vinyl == null)
            {
                return HttpNotFound();
            }
            return View(vinyl);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Vinyl vinyl = db.Vinyls.Find(id);

            if (vinyl == null)
            {
                return HttpNotFound();
            }
            return View(vinyl);
        }

        [HttpPost]
        public ActionResult Edit(Vinyl vinyl)
        {
            if (ModelState.IsValid)
            {
                db.Entry(vinyl).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vinyl);
        }

        public ActionResult Delete(int? id)
        {
            Vinyl vinyl = db.Vinyls.Find(id);

            if (vinyl == null)
            {
                return HttpNotFound();
            }
            return View(vinyl);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Vinyl vinyl = db.Vinyls.Find(id);
           // db.Entry(vinyl).State = EntityState.Modified;
            db.Vinyls.Remove(vinyl);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult About()
        {
            return View();
        }
    }
}