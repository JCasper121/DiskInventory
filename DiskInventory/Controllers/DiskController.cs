using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace DiskInventory.Controllers
{
    public class DiskController : Controller
    {
        private disk_inventoryJCContext context { get; set; }

        public DiskController(disk_inventoryJCContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            return View(disks);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(dt => dt.Description).ToList();
            ViewBag.Action = "Add";
            return View("Edit", new Disk());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var disk = context.Disks.Find(id); 
            ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
            ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
            ViewBag.DiskTypes = context.DiskTypes.OrderBy(dt => dt.Description).ToList();

            return View(disk);   
        }

        [HttpPost]
        public IActionResult Edit(Disk disk)
        {
            if(ModelState.IsValid)
            {
                if(disk.DiskId == 0)
                {
                    //context.Disks.Add(disk);
                    context.Database.ExecuteSqlRaw("execute sp_ins_disk @p0, @p1, @p2, @p3, @p4",
                        parameters: new[] { disk.DiskName, disk.ReleaseDate.ToString(), disk.StatusId.ToString(), disk.GenreId.ToString(), disk.DiskTypeId.ToString() });
                }
                else
                {
                    context.Database.ExecuteSqlRaw("execute sp_upd_disk @p0, @p1, @p2, @p3, @p4, @p5",
                        parameters: new[] { disk.DiskId.ToString(), disk.DiskName, disk.ReleaseDate.ToString(), disk.StatusId.ToString(), disk.GenreId.ToString(), disk.DiskTypeId.ToString() });
                    //context.Disks.Update(disk);
                }
                context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Action = (disk.DiskId == 0 ? "Add" : "Edit");
                ViewBag.Genres = context.Genres.OrderBy(g => g.Description).ToList();
                ViewBag.Statuses = context.Statuses.OrderBy(s => s.Description).ToList();
                ViewBag.DiskTypes = context.DiskTypes.OrderBy(dt => dt.Description).ToList();
                return View(disk);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var disk = context.Disks.Find(id);
            return View(disk);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Disk disk)
        {
            //context.Disks.Remove(disk);
            context.Database.ExecuteSqlRaw("execute sp_del_disk @p0",
                parameters: new[] { disk.DiskId.ToString() });
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
