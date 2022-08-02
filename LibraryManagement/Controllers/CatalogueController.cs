using DataLibrary;
using LibraryManagement.Models.Catalogue;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryManagement.Controllers
{
    public class CatalogueController : Controller
    {
        //make this public if needed
        private ILibraryAsset _assets;

        public CatalogueController(ILibraryAsset assets)
        {
          _assets = assets;
        }
        
        public IActionResult Index()
        {
            var allAssets = _assets.GetAll();

            var IndexResult = allAssets.Select(result => new CatalogueIndex
            {
                Id = result.Asset_ID, ImageUrl = result.ImageUrl, Title = result.Title, Type = _assets.GetType(result.Asset_ID), 
                DirectorOrAuthor = _assets.GetAuthororDirector(result.Asset_ID), DeweyIndex = _assets.GetDeweyIndex(result.Asset_ID)

            });

            var CatalogueList = new CatalogueIndexList() { AssetList = IndexResult };

            return View(CatalogueList);

        }

        public IActionResult MoreInformation(int Id)
        {
            var Asset = _assets.GetById(Id);

            var assetInformation = new AssetInformation
            {
                Asset_ID = Id,
                ImageUrl = Asset.ImageUrl,
                Title = Asset.Title,
                Cost = Asset.Cost,
                Year = Asset.Year,
                AuthororDirector = _assets.GetAuthororDirector(Id),
                DeweyIndex = _assets.GetDeweyIndex(Id),
                ISBN = _assets.GetISBN(Id),
                Availability = Asset.Availability.Status,
                PresentLocation = _assets.GetBranchLocation(Id).Branch_Name,

            };
            return View(assetInformation);

        }

    }
}
