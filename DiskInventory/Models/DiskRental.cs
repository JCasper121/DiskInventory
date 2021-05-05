using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class DiskRental
    {
        public int RentalId { get; set; }

        [Required(ErrorMessage = "Please enter a date.")]
        public DateTime? BorrowedDate { get; set; }

        [Required(ErrorMessage = "Please enter a date.")]
        public DateTime? DueDate { get; set; }

        //[Required(ErrorMessage = "Please enter a date.")]
        public DateTime? ReturnDate { get; set; }

        [Required(ErrorMessage = "Please select a borrower.")]
        public int? BorrowerId { get; set; }

        [Required(ErrorMessage = "Please select a disk.")]
        public int? DiskId { get; set; }

        public virtual Borrower Borrower { get; set; }
        public virtual Disk Disk { get; set; }
    }
}
