using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;

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
                    ViewBag.Action = "Add";
                    context.Disks.Add(disk);
                }
                else
                {
                    context.Disks.Update(disk);
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
            context.Disks.Remove(disk);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
