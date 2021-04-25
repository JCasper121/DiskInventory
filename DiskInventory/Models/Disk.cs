using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace DiskInventory.Models
{
    public partial class Disk
    {
        public Disk()
        {
            DiskArtists = new HashSet<DiskArtist>();
            DiskRentals = new HashSet<DiskRental>();
        }

        public int DiskId { get; set; }

        [Required(ErrorMessage = "Disk name required")]
        public string DiskName { get; set; }

        [Required(ErrorMessage = "Release date required")]
        public DateTime ReleaseDate { get; set; }
        public int? StatusId { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        public int? GenreId { get; set; }
        public int? DiskTypeId { get; set; }

        public virtual DiskType DiskType { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<DiskArtist> DiskArtists { get; set; }
        public virtual ICollection<DiskRental> DiskRentals { get; set; }
    }
}
