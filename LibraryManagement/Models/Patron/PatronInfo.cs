using DataLibrary.Models;

namespace LibraryManagement.Models.Patron
{
    public class PatronInfo
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName { get { return FirstName + " " + LastName; } }
        public int Library_CardId { get; set; }
        public decimal Overdue_Fees { get; set; }
        public string LibraryBranch { get; set; }
        public string Telephone { get; set; }
        public DateTime StartDate { get; set; }
        public string Address { get; set; }
        public IEnumerable<LoanedAsset> CheckedOut_Assets { get; set; }
        public IEnumerable<CheckOutHistory> CheckOut_History { get; set; }
        public IEnumerable<Hold> Holds { get; set; }
    }
}
