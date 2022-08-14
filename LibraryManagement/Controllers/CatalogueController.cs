using DataLibrary;
using LibraryManagement.Models.Catalogue;
using LibraryManagement.Models.CheckOutAsset;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LibraryManagement.Controllers
{
    public class CatalogueController : Controller
    {
        private ILibraryAsset _assets;
        private ICheckOut _checkOuts;

        public CatalogueController(ILibraryAsset assets, ICheckOut checkOuts)
        {
          _assets = assets;
          _checkOuts = checkOuts;
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
            var currentHolds = _checkOuts.GetCurrentHolds(Id).Select(
                h=>new HoldPlacedHistory
                {
                    PatronName = _checkOuts.GetCurrentHoldPatron(h.ID),
                    HoldStatus = _checkOuts.GetCurrentHoldTime(h.ID).ToString("d")
                }
                );

            var assetInformation = new AssetInformation
            {
                Asset_ID = Id,
                ImageUrl = Asset.ImageUrl,
                Title = Asset.Title,
                Cost = Asset.Cost,
                Year = Asset.Year,
                Type = _assets.GetType(Id),
                AuthororDirector = _assets.GetAuthororDirector(Id),
                DeweyIndex = _assets.GetDeweyIndex(Id),
                ISBN = _assets.GetISBN(Id),
                Availability = Asset.Availability.Status,
                PresentLocation = _assets.GetBranchLocation(Id).Branch_Name,
                CheckOutHistories = _checkOuts.GetCheckOutHistory(Id),
                Checkout_Details = _checkOuts.LatestCheckout_Asset(Id),
                PatronName = _checkOuts.GetCurrentCheckOutPatron(Id),
                HoldsPlaced = currentHolds

            };
            return View(assetInformation);

        }

        public IActionResult CheckOut(int Id)
        {
            var asset = _assets.GetById(Id);

            var model = new CheckOutModel
            {
                AssetID = Id,
                Title = asset.Title,
                ImageUrl = asset.ImageUrl,
                LibraryCardID = "",
                IsCheckedOut = _checkOuts.IsCheckedOut(Id)
            };
            return View(model);
        }

        public IActionResult CheckIn(int Id)
        {
   
            _checkOuts.CheckIn_Asset(Id);
            return RedirectToAction("MoreInformation", new { Id = Id });
            //return RedirectToAction("Home");
        }

        public IActionResult Hold(int Id)
        {
            var asset = _assets.GetById(Id);

            var model = new CheckOutModel
            {
                AssetID = Id,
                Title = asset.Title,
                ImageUrl = asset.ImageUrl,
                LibraryCardID = "",
                IsCheckedOut = _checkOuts.IsCheckedOut(Id),
                Hold_Count = _checkOuts.GetCurrentHolds(Id).Count()
            };
            return View(model);
        }

        // 
        [HttpPost]
        public IActionResult PlaceCheckOut(int AssetID, int LibraryCardID)
        {
            _checkOuts.CheckOut_Asset(AssetID, LibraryCardID);
            return RedirectToAction("MoreInformation", new { Id = AssetID });
        }
        [HttpPost]
        public IActionResult PlaceHold(int AssetID, int LibraryCardID)
        {
            _checkOuts.PlaceHold(AssetID, LibraryCardID);
            return RedirectToAction("MoreInformation", new { Id = AssetID });
        }

        [HttpPost]
        public IActionResult MarkLost(int Id)
        {
            _checkOuts.MarkLost(Id);
            return RedirectToAction("MoreInformation", new { Id = Id });
        }
        [HttpPost]
        public IActionResult MarkFound(int Id)
        {
            _checkOuts.MarkFound(Id);
            return RedirectToAction("MoreInformation", new { Id = Id });
        }
    }
}
