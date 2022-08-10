using DataLibrary.Models;
namespace DataLibrary
{
    public interface ICheckOut
    {
        IEnumerable<LoanedAsset> GetAll(); 
        LoanedAsset GetById(int loanedAsset_ID);
        void Add(LoanedAsset new_loanedAsset); 
        void CheckOut_Asset(int asset_ID, int libraryCard_ID);
        void CheckIn_Asset(int asset_ID);
        string GetCurrentCheckOutPatron(int asset_ID);

        IEnumerable<CheckOutHistory> GetCheckOutHistory(int ID);
        void PlaceHold(int asset_ID, int libraryCard_ID);    
        string GetCurrentHoldPatron(int ID);
        DateTime GetCurrentHoldTime(int ID);
        IEnumerable<Hold> GetCurrentHolds(int ID);

        LoanedAsset LatestCheckout_Asset(int asset_ID);

        void MarkLost(int asset_ID);
        void MarkFound(int asset_ID);

        bool IsCheckedOut(int ID);

    }
}
