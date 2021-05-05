using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DiskInventory.Models;
using Microsoft.EntityFrameworkCore;

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

        //[HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.Action = "Edit";
            var borrower = context.Borrowers.Find(id);
            return View(borrower);
        }

        [HttpPost]
        public IActionResult Edit(Borrower borrower)
        {
            if(ModelState.IsValid)
            {
                if(borrower.BorrowerId == 0)
                {
                    //context.Borrowers.Add(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_ins_borrower @p0, @p1, @p2", 
                        parameters: new[] { borrower.FirstName, borrower.LastName, borrower.Phone.ToString()});
                }
                else
                {
                    //context.Borrowers.Update(borrower);
                    context.Database.ExecuteSqlRaw("execute sp_upd_borrower @p0, @p1, @p2, @p3", 
                        parameters: new[] { borrower.BorrowerId.ToString(), borrower.FirstName, borrower.LastName, borrower.Phone.ToString() });

                }
                context.SaveChanges();
                return RedirectToAction("Index");
            } else
            {
                ViewBag.Action = (borrower.BorrowerId == 0) ? "Add" : "Edit";
                return View(borrower);
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
            //context.Borrowers.Remove(borrower);
            context.Database.ExecuteSqlRaw("execute sp_del_borrower @p0",
                parameters: new[] { borrower.BorrowerId.ToString() });
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
