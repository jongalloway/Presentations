using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Scaffolding.Models;

namespace Scaffolding.Controllers
{   
    public class AlbumController : Controller
    {
		private readonly IGenreRepository genreRepository;
		private readonly IAlbumRepository albumRepository;

		// If you are using Dependency Injection, you can delete the following constructor
        public AlbumController() : this(new GenreRepository(), new AlbumRepository())
        {
        }

        public AlbumController(IGenreRepository genreRepository, IAlbumRepository albumRepository)
        {
			this.genreRepository = genreRepository;
			this.albumRepository = albumRepository;
        }

        //
        // GET: /Album/

        public ViewResult Index()
        {
            return View(albumRepository.AllIncluding(album => album.Genre));
        }

        //
        // GET: /Album/Details/5

        public ViewResult Details(int id)
        {
            return View(albumRepository.Find(id));
        }

        //
        // GET: /Album/Create

        public ActionResult Create()
        {
			ViewBag.PossibleGenres = genreRepository.All;
            return View();
        } 

        //
        // POST: /Album/Create

        [HttpPost]
        public ActionResult Create(Album album)
        {
            if (ModelState.IsValid) {
                albumRepository.InsertOrUpdate(album);
                albumRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleGenres = genreRepository.All;
				return View();
			}
        }
        
        //
        // GET: /Album/Edit/5
 
        public ActionResult Edit(int id)
        {
			ViewBag.PossibleGenres = genreRepository.All;
             return View(albumRepository.Find(id));
        }

        //
        // POST: /Album/Edit/5

        [HttpPost]
        public ActionResult Edit(Album album)
        {
            if (ModelState.IsValid) {
                albumRepository.InsertOrUpdate(album);
                albumRepository.Save();
                return RedirectToAction("Index");
            } else {
				ViewBag.PossibleGenres = genreRepository.All;
				return View();
			}
        }

        //
        // GET: /Album/Delete/5
 
        public ActionResult Delete(int id)
        {
            return View(albumRepository.Find(id));
        }

        //
        // POST: /Album/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            albumRepository.Delete(id);
            albumRepository.Save();

            return RedirectToAction("Index");
        }
    }
}

