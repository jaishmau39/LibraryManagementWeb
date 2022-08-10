namespace LibraryManagement.Models.CheckOutAsset
{
    public class CheckOutModel
    {
        public string LibraryCardID { get; set; }
        public string Title { get; set; }
        public int AssetID { get; set; }
        public string ImageUrl { get; set; }
        public int Hold_Count { get; set; }
        public bool IsCheckedOut { get; set; }
    }
}
