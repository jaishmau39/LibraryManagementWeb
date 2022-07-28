using DataLibrary;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryServices
{
    public class LibraryAssetService : ILibraryAsset
    {
        private LibraryContext _context;
        public LibraryAssetService(LibraryContext context)
        {
            _context = context;
        }
        public void AddAsset(LibraryAsset asset)
        {
            _context.Add(asset);
            _context.SaveChanges();
        }

        public IEnumerable<LibraryAsset> GetAll()
        {
            return _context.LibraryAssets
                .Include(asset => asset.Availability)
                .Include(asset => asset.Last_Location);
        }

        public string GetAuthororDirector(int id)
        {
            var book = _context.LibraryAssets.OfType<Book>().Where(asset => asset.Asset_ID == id).Any();
            var video = _context.LibraryAssets.OfType<Video>().Where(asset => asset.Asset_ID == id).Any();

            return book ? _context.Books.FirstOrDefault(books => books.Asset_ID == id).Author :
                _context.Videos.FirstOrDefault(videos => videos.Asset_ID == id).Director ?? "Unknown";

        }

        public LibraryBranch GetBranchLocation(int id)
        {
            return _context.LibraryAssets.FirstOrDefault(asset => asset.Asset_ID == id).Last_Location;
        }

        public LibraryAsset GetById(int id)
        {
            return _context.LibraryAssets
                .Include(asset => asset.Availability)
                .Include(asset => asset.Last_Location)
                .FirstOrDefault(asset => asset.Asset_ID == id);
        }

        public string GetDeweyIndex(int id)
        {
            if (_context.Books.Any(book => book.Asset_ID == id))
            {
                return _context.Books.FirstOrDefault(book => book.Asset_ID == id).DeweyIndex;
            }

            else return "";
        }

        public string GetISBN(int id)
        {
            if(_context.Books.Any(book => book.Asset_ID == id))
            {
                return _context.Books.FirstOrDefault(book => book.Asset_ID == id).ISBN;
            }

            else return "";
        }

        public string GetTitle(int id)
        {
            return _context.LibraryAssets.FirstOrDefault(asset => asset.Asset_ID == id).Title;
        }

        public string GetType(int id)
        {
            var book = _context.LibraryAssets.OfType<Book>().Where(asset => asset.Asset_ID == id);
            return book.Any() ? "Book" : "Video";
        }
    }
}