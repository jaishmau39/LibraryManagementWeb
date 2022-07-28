using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibrary
{
    public interface ILibraryAsset
    {
        IEnumerable<LibraryAsset> GetAll();
        LibraryAsset GetById(int id);

        string GetAuthororDirector(int id);
        string GetDeweyIndex(int id);
        string GetISBN(int id);
        string GetTitle(int id);
        string GetType(int id);

        void AddAsset(LibraryAsset asset);
        LibraryBranch GetBranchLocation(int id);
        //void RemoveAsset(LibraryAsset asset);


    }
}
