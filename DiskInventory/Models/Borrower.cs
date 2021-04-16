using System;
using System.Collections.Generic;

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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long Phone { get; set; }

        public virtual ICollection<DiskRental> DiskRentals { get; set; }
    }
}
