using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;

namespace DiskInventory.Controllers
{
    public class BorrowerController : Controller
    {
        private disk_inventoryJCContext context { get; set; }

        public BorrowerController(disk_inventoryJCContext ctx)
        {
            context = ctx;
        }
        public IActionResult Index()
        {
            var borrowers = context.Borrowers.OrderBy(b => b.LastName).ThenBy(b => b.FirstName).ToList();
            return View(borrowers);
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Action = "Add";
            return View("Edit", new Borrower());
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        [HttpPost]
        public RedirectToActionResult Edit(Borrower borrower)
        {
            if(ModelState.IsValid)
            {
                if(borrower.BorrowerId == 0)
                {
                    context.Borrowers.Add(borrower);
                }
                else
                {
                    context.Borrowers.Update(borrower);
                }
                context.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                ViewBag.Action = (borrower.BorrowerId == 0 ? "Add" : "Edit");
                return RedirectToAction("Edit", borrower);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        [HttpPost]
        public RedirectToActionResult Delete(Borrower borrower)
        {
            context.Borrowers.Remove(borrower);
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
