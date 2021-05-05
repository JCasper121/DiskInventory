using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;


namespace DiskInventory.Controllers
{
    public class RentalController : Controller
    {
        private disk_inventoryJCContext context { get; set; }

        public RentalController(disk_inventoryJCContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var rentals = context.DiskRentals.Include(r => r.Disk).Include(r => r.Borrower).OrderBy(r => r.BorrowedDate).ThenBy(r => r.DueDate).ToList();
            //var rentals = context.DiskRentals.ToList();

            return View(rentals);
        }
        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            ViewBag.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.LastName).ToList();
            DiskRental rental = new DiskRental();
            return View("Edit", rental);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            if(id == 0)
            {
                ViewBag.Action = "Add";
            }else
            {
                ViewBag.Action = "Edit";

            }
            ViewBag.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
            ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.LastName).ToList();
            var rental = context.DiskRentals.Find(id);
            return View(rental);
        }

        [HttpPost]
        public IActionResult Edit(DiskRental rental)
        {
            if(ModelState.IsValid)
            {
                if(rental.RentalId == 0)
                {
                    context.DiskRentals.Add(rental);
                } else
                {
                    context.DiskRentals.Update(rental);

                }
            }else
            {
                if(rental.RentalId == 0)
                {
                    ViewBag.Action = "Add";
                } else
                {
                    ViewBag.Action = "Edit";
                }
                ViewBag.Disks = context.Disks.OrderBy(d => d.DiskName).ToList();
                ViewBag.Borrowers = context.Borrowers.OrderBy(b => b.LastName).ToList();
                return View(rental);
            }
            context.SaveChanges();
            return RedirectToAction("Index", "Rental");

        }
    }
}
