using DataLibrary;
using LibraryManagement.Models.Branch;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class BranchController : Controller
    {
        private ILibraryBranch _branch;
        public BranchController(ILibraryBranch branch)
        {
            _branch = branch;
        }

        public IActionResult Index()
        {
            var branches = _branch.GetAll().Select(b => new BranchInformation
            {
                Id = b.ID,
                BranchName = b.Branch_Name,
                NoOfAssets = _branch.GetBranchAssets(b.ID).Count(),
                NoOfPatrons = _branch.GetPatrons(b.ID).Count(),
                isOpen = _branch.OpenOrClose(b.ID)
            }
            );
            var model = new BranchIndex()
            {
                Branches = branches,
            };
            return View(model);
        }
        public IActionResult MoreInformation(int Id)
        {
            var branch = _branch.Get(Id);
            var model = new BranchInformation
            {
                Id = branch.ID,
                ImageUrl = branch.ImageUrl,
                BranchName = branch.Branch_Name,
                Address = branch.Address,
                Telephone = branch.Telephone_Number,
                OpenDate  = branch.Date_Founded.ToString("yyy-MM-dd"),
                HoursOpen = _branch.GetOpenHours(Id),
                NoOfAssets  = _branch.GetBranchAssets(Id).Count(),
                NoOfPatrons = _branch.GetPatrons(Id).Count(),
                AssetValue = _branch.GetBranchAssets(Id).Sum(v=>v.Cost),

            };
            return View(model);
        }
    }
}
