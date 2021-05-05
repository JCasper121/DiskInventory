using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DiskInventory.Models
{
    public partial class disk_inventoryJCContext : DbContext
    {
        public disk_inventoryJCContext()
        {
        }

        public disk_inventoryJCContext(DbContextOptions<disk_inventoryJCContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Artist> Artists { get; set; }
        public virtual DbSet<ArtistType> ArtistTypes { get; set; }
        public virtual DbSet<Borrower> Borrowers { get; set; }
        public virtual DbSet<Disk> Disks { get; set; }
        public virtual DbSet<DiskArtist> DiskArtists { get; set; }
        public virtual DbSet<DiskRental> DiskRentals { get; set; }
        public virtual DbSet<DiskType> DiskTypes { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Status> Statuses { get; set; }
        public virtual DbSet<ViewIndividualArtist> ViewIndividualArtists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.\\SQLDEV01;Database=disk_inventoryJC;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Artist>(entity =>
            {
                entity.ToTable("Artist");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artist_type_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("last_name");

                entity.HasOne(d => d.ArtistType)
                    .WithMany(p => p.Artists)
                    .HasForeignKey(d => d.ArtistTypeId)
                    .HasConstraintName("FK__Artist__artist_t__2B3F6F97");
            });

            modelBuilder.Entity<ArtistType>(entity =>
            {
                entity.ToTable("ArtistType");

                entity.Property(e => e.ArtistTypeId).HasColumnName("artist_type_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Borrower>(entity =>
            {
                entity.ToTable("Borrower");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("last_name");

                entity.Property(e => e.Phone).HasColumnName("phone");
            });

            modelBuilder.Entity<Disk>(entity =>
            {
                entity.ToTable("Disk");

                entity.Property(e => e.DiskId).HasColumnName("disk_id");

                entity.Property(e => e.DiskName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("disk_name");

                entity.Property(e => e.DiskTypeId).HasColumnName("disk_type_id");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.ReleaseDate)
                    .HasColumnType("date")
                    .HasColumnName("release_date");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.HasOne(d => d.DiskType)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.DiskTypeId)
                    .HasConstraintName("FK__Disk__disk_type___300424B4");

                entity.HasOne(d => d.Genre)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.GenreId)
                    .HasConstraintName("FK__Disk__genre_id__2F10007B");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.Disks)
                    .HasForeignKey(d => d.StatusId)
                    .HasConstraintName("FK__Disk__status_id__2E1BDC42");
            });

            modelBuilder.Entity<DiskArtist>(entity =>
            {
                entity.ToTable("DiskArtist");

                entity.Property(e => e.DiskArtistId).HasColumnName("disk_artist_id");

                entity.Property(e => e.ArtistId).HasColumnName("artist_id");

                entity.Property(e => e.DiskId).HasColumnName("disk_id");

                entity.HasOne(d => d.Artist)
                    .WithMany(p => p.DiskArtists)
                    .HasForeignKey(d => d.ArtistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiskArtis__artis__35BCFE0A");

                entity.HasOne(d => d.Disk)
                    .WithMany(p => p.DiskArtists)
                    .HasForeignKey(d => d.DiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiskArtis__disk___34C8D9D1");
            });

            modelBuilder.Entity<DiskRental>(entity =>
            {
                entity.HasKey(e => e.RentalId)
                    .HasName("PK__DiskRent__67DB611BF8C4FD03");

                entity.ToTable("DiskRental");

                entity.Property(e => e.RentalId).HasColumnName("rental_id");

                entity.Property(e => e.BorrowedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("borrowed_date");

                entity.Property(e => e.BorrowerId).HasColumnName("borrower_id");

                entity.Property(e => e.DiskId).HasColumnName("disk_id");

                entity.Property(e => e.DueDate)
                    .HasColumnType("datetime")
                    .HasColumnName("due_date");

                entity.Property(e => e.ReturnDate)
                    .HasColumnType("datetime")
                    .HasColumnName("return_date");

                entity.HasOne(d => d.Borrower)
                    .WithMany(p => p.DiskRentals)
                    .HasForeignKey(d => d.BorrowerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiskRenta__borro__38996AB5");

                entity.HasOne(d => d.Disk)
                    .WithMany(p => p.DiskRentals)
                    .HasForeignKey(d => d.DiskId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__DiskRenta__disk___398D8EEE");
            });

            modelBuilder.Entity<DiskType>(entity =>
            {
                entity.ToTable("DiskType");

                entity.Property(e => e.DiskTypeId).HasColumnName("disk_type_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Genre>(entity =>
            {
                entity.ToTable("Genre");

                entity.Property(e => e.GenreId).HasColumnName("genre_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.ToTable("Status");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60)
                    .HasColumnName("description");
            });

            modelBuilder.Entity<ViewIndividualArtist>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("View_Individual_Artist");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("first_name");

                entity.Property(e => e.LastName)
                    .HasMaxLength(60)
                    .IsUnicode(false)
                    .HasColumnName("last_name");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
