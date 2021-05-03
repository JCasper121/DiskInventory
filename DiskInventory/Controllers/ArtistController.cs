using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskInventory.Controllers
{
    public class ArtistController : Controller
    {
        private disk_inventoryJCContext context { get; set; }

        public ArtistController(disk_inventoryJCContext ctx)
        {
            context = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var artists = context.Artists.OrderBy(a => a.LastName).ThenBy(a => a.FirstName).ToList();
            return View(artists);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            
            return View("Edit", new Artist());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
            Artist artist = context.Artists.Find(id);
            return View(artist);
        }

        [HttpPost]
        public IActionResult Edit(Artist artist)
        {
            if(ModelState.IsValid)
            {
                if (artist.ArtistId == 0)
                {
                    //context.Artists.Add(artist);
                    context.Database.ExecuteSqlRaw("execute sp_ins_artist @p0, @p1, @p2",
                        parameters: new[] { artist.FirstName, artist.LastName, artist.ArtistTypeId.ToString() });
                }
                else
                {
                    //context.Artists.Update(artist);
                    context.Database.ExecuteSqlRaw("execute sp_upd_artist @p0, @p1, @p2, @p3",
                        parameters: new[] { artist.ArtistId.ToString(), artist.FirstName, artist.LastName, artist.ArtistTypeId.ToString() });

                }
                context.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                ViewBag.Action = (artist.ArtistId == 0 ? "Add" : "Edit");
                ViewBag.ArtistTypes = context.ArtistTypes.OrderBy(t => t.Description).ToList();
                return View(artist);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var artist = context.Artists.Find(id);
            return View(artist);
        }

        [HttpPost] 
        public RedirectToActionResult Delete(Artist artist)
        {
            //context.Artists.Remove(artist);
            context.Database.ExecuteSqlRaw("execute sp_del_artist @p0",
                parameters: new[] { artist.ArtistId.ToString() });
            context.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
