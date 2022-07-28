namespace LibraryManagement.Models.Catalogue
{
    public class CatalogueIndex
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }

        public string Type { get; set; }
        public string DirectorOrAuthor { get; set; }
        public string DeweyIndex { get; set; }
        public string NumberOfCopies { get; set; }

    }
}
