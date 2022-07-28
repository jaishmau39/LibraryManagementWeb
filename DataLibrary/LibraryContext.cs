using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLibrary
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions options) : base(options) { }
        
            public DbSet<Patron> Patrons { get; set; }
            public DbSet<AssetStatus> AssetStatuses { get; set; }
            public DbSet<LibraryBranch> LibraryBranches { get; set; }
            public DbSet<LibraryAsset> LibraryAssets { get; set; }
            public DbSet<Book> Books { get; set; }
            public DbSet<LibraryCard> LibraryCards { get; set; }
            public DbSet<LoanedAsset> LoanedAssets { get; set; }
            public DbSet<CheckOutHistory> CheckOutHistories { get; set; }
            public DbSet<BranchTimes> BranchTimings { get; set; }
            public DbSet<Hold> Holds { get; set; }
            public DbSet<Video> Videos { get; set; }

    }
    
}