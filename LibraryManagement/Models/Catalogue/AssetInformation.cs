using DataLibrary.Models;

namespace LibraryManagement.Models.Catalogue
{
    public class AssetInformation
    {
        public int Asset_ID { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public decimal Cost { get; set; }

        public int Year { get; set; }
        public string AuthororDirector { get; set; }
        public string DeweyIndex { get; set; }
        public string ISBN { get; set; }
        public string PatronName  { get; set; }
        public string Availability { get; set; }
        public string PresentLocation { get; set; }
        public LoanedAsset Checkout_Details { get; set; }
        public IEnumerable<CheckOutHistory> CheckOutHistories { get; set; }
        public IEnumerable<HoldPlacedHistory> HoldsPlaced { get; set; }

    }

    public class HoldPlacedHistory
    {
        public string PatronName { get; set; }
        public string HoldStatus { get; set; }
    }
}
