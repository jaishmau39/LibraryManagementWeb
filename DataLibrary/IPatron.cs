using DataLibrary.Models;

namespace DataLibrary
{
    public interface IPatron
    {
        Patron Get(int Id);
        IEnumerable<Patron> GetAll();
        void Add(Patron new_Patron);
        IEnumerable<CheckOutHistory> GetCheckOutHistory(int patron_Id);
        IEnumerable<Hold> GetHolds(int patron_Id);
        IEnumerable<LoanedAsset> GetCheckOuts(int patron_Id);
    }
}
