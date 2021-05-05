using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
#nullable disable

namespace DiskInventory.Models
{
    public partial class Borrower
    {
        public Borrower()
        {
            DiskRentals = new HashSet<DiskRental>();
        }
        public int BorrowerId { get; set; }

        [Required(ErrorMessage = "First name is required")]
        //[StringLength(60, MinimumLength =3)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        //[StringLength(60, MinimumLength = 3)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        //[StringLength(10, 12)]
        public string Phone { get; set; }

        public virtual ICollection<DiskRental> DiskRentals { get; set; }
    }
}
