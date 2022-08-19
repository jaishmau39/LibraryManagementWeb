using DataLibrary.Models;

namespace DataLibrary
{
    public interface ILibraryBranch
    {
        void Add(LibraryBranch newLibraryBranch);
        bool OpenOrClose(int BranchId);
        IEnumerable<string> GetOpenHours(int BranchId);
        IEnumerable<LibraryAsset> GetBranchAssets(int BranchId);

        LibraryBranch Get(int BranchId);
        IEnumerable<Patron> GetPatrons(int BranchId);
        IEnumerable<LibraryBranch> GetAll();

    }
}
