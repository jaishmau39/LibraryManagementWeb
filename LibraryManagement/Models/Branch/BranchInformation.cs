namespace LibraryManagement.Models.Branch
{
    public class BranchInformation
    {
        public int Id { get; set; }
        public string BranchName { get; set; }
        public string Address { get; set; }
        public bool isOpen { get; set; }
        public string Description { get; set; }
        public string Telephone { get; set; }
        public string OpenDate { get; set; }
        public int NoOfAssets { get; set; }
        public int NoOfPatrons { get; set; }
        public decimal AssetValue { get; set; }
        public IEnumerable<string> HoursOpen { get; set; }
        public string ImageUrl { get; set; }



    }
}
