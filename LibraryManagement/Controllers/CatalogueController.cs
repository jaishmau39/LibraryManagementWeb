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

    }
}
