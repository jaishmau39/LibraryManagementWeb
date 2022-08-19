using DataLibrary;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices
{
    public class LibraryBranchService : ILibraryBranch
    {
        private LibraryContext _context;

        public LibraryBranchService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(LibraryBranch newLibraryBranch)
        {
            _context.Add(newLibraryBranch);
            _context.SaveChanges();
        }

        public LibraryBranch Get(int BranchId)
        {
            return GetAll().FirstOrDefault(lb => lb.ID == BranchId);
        }

        public IEnumerable<LibraryBranch> GetAll()
        {
            return _context.LibraryBranches.
                Include(lb => lb.Branch_Assets).Include(lb => lb.Branch_Patrons);
        }

        public IEnumerable<LibraryAsset> GetBranchAssets(int BranchId)
        {
            return _context.LibraryBranches.Include(l => l.Branch_Assets).
                FirstOrDefault(l => l.ID == BranchId).Branch_Assets;
        }

        public IEnumerable<string> GetOpenHours(int BranchId)
        {
            var open_hours = _context.BranchTimings.Where(oh=>oh.LibraryBranch.ID == BranchId);
            return ReadableDates.DisplayTimes(open_hours);
        }

        public IEnumerable<Patron> GetPatrons(int BranchId)
        {
            return _context.LibraryBranches.Include(p => p.Branch_Patrons)
                .FirstOrDefault(p => p.ID == BranchId).Branch_Patrons;
        }

        public bool OpenOrClose(int BranchId)
        {
            var CurrentHour = DateTime.Now.Hour;
            var CurrentDayofWeek = (int)DateTime.Now.DayOfWeek+1;
            var open_hours = _context.BranchTimings.Where(oh => oh.LibraryBranch.ID == BranchId);

            var hours_day = open_hours.FirstOrDefault(f =>f.Day_of_Week==CurrentDayofWeek);
            return CurrentHour<hours_day.ClosingTime && CurrentHour>hours_day.OpeningTime;  
        }
    }
}
