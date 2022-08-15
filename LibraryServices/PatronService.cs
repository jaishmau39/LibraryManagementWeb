using DataLibrary;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices
{
    public class PatronService : IPatron
    {
        private LibraryContext _context;

        public PatronService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(Patron new_Patron)
        {
            _context.Add(new_Patron);
            _context.SaveChanges();
        }

        public Patron Get(int Id)
        {
            return GetAll().
                FirstOrDefault(p=>p.ID==Id);
        }

        public IEnumerable<Patron> GetAll()
        {
            return _context.Patrons.Include(p => p.Library_Card).Include(p => p.Branch_Location);
        }

        public IEnumerable<CheckOutHistory> GetCheckOutHistory(int patron_Id)
        {
            var lib_cardId = Get(patron_Id).
                Library_Card.CardID;
            return _context.CheckOutHistories.Include(lb=>lb.LibraryCard).Include(lb=>lb.LibraryAsset).
                Where(lb => lb.LibraryCard.CardID== lib_cardId).OrderByDescending(lb=>lb.CheckOutDate);
        }

        public IEnumerable<LoanedAsset> GetCheckOuts(int patron_Id)
        {
            var lib_cardId = Get(patron_Id).
                Library_Card.CardID;
            return _context.LoanedAssets.Include(lb => lb.LibraryCard).Include(lb => lb.LibraryAsset).
                Where(lb => lb.LibraryCard.CardID == lib_cardId);
        }

        public IEnumerable<Hold> GetHolds(int patron_Id)
        {
            var lib_cardId = Get(patron_Id).
                Library_Card.CardID;
            return _context.Holds.Include(lb => lb.LibraryCard).Include(lb => lb.LibraryAsset).
                Where(lb => lb.LibraryCard.CardID == lib_cardId).OrderByDescending(lb => lb.Hold_Placed);
        }
    }
}
