using DataLibrary;
using DataLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class CheckOutService : ICheckOut
    {
        private LibraryContext _context;

        public CheckOutService(LibraryContext context)
        {
            _context = context;
        }

        public void Add(LoanedAsset new_loanedAsset)
        {
            _context.Add(new_loanedAsset);
            _context.SaveChanges();
        }

        public void CheckIn_Asset(int asset_ID, int libraryCard_ID)
        {
            var now = DateTime.Now;
            var asset = _context.LibraryAssets.FirstOrDefault(a => a.Asset_ID == asset_ID);

            RemoveExistingCheckouts(asset_ID);
            CloseCheckoutHistory(asset_ID, now);

            //Look for holds on the asset.
            var asset_holds = _context.Holds.Include(la => la.LibraryAsset).Include(lc => lc.LibraryCard).
                Where(h => h.LibraryAsset.Asset_ID == asset_ID);

            //if holds exist, check out asset to earliest hold
            if (asset_holds.Any())
            {
                CheckOutToEarliestHold(asset_ID, asset_holds);
            }

            //else, update item status to "Available"
            UpdateAssetAvailability(asset_ID, "Available");

            _context.SaveChanges();
        }

        private void CheckOutToEarliestHold(int asset_ID, IQueryable<Hold> asset_holds)
        {
            var earliest_hold = asset_holds.OrderBy(h => h.Hold_Placed).FirstOrDefault();

            var libraryCard = earliest_hold.LibraryCard;

            _context.Remove(earliest_hold);
            _context.SaveChanges();
            CheckOut_Asset(asset_ID, libraryCard.CardID);
        }

        public void CheckOut_Asset(int asset_ID, int libraryCard_ID)
        {
            var now = DateTime.Now;
            
            if(IsCheckedOut(asset_ID))
            {
                return;
            }

            var asset = _context.LibraryAssets.FirstOrDefault(a => a.Asset_ID == asset_ID);

            UpdateAssetAvailability(asset_ID,"Checked Out");
            var Patron_libCard = _context.LibraryCards.Include(la=>la.LoanedAssets).
                FirstOrDefault(a => a.CardID == libraryCard_ID);

            //Create a new checkout

            var checkout = new LoanedAsset
            {
                LibraryAsset = asset,
                LibraryCard = Patron_libCard,
                CheckoutDate = now,
                ReturnDate = GetAssetReturnTime(now)
            };

            _context.Add(checkout);

            var checkoutHistory = new CheckOutHistory
            {
                LibraryAsset = asset,
                LibraryCard = Patron_libCard,
                CheckOutDate = now
            };

            _context.Add(checkoutHistory);
            _context.SaveChanges();
        }

        private DateTime GetAssetReturnTime(DateTime now)
        {
            return now.AddDays(30);
        }

        private bool IsCheckedOut(int asset_ID)
        {
            return _context.LoanedAssets.Where(a => a.LibraryAsset.Asset_ID == asset_ID).Any();
        }

        public IEnumerable<LoanedAsset> GetAll()
        {
            return _context.LoanedAssets;
        }

        public LoanedAsset GetById(int loanedAsset_ID)
        {
            return GetAll().FirstOrDefault(loanedAsset => loanedAsset.ID == loanedAsset_ID);
        }

        public IEnumerable<CheckOutHistory> GetCheckOutHistory(int ID)
        {
            return _context.CheckOutHistories.Include(checkOutHistory => checkOutHistory.LibraryAsset).
                Include(checkOutHistory=>checkOutHistory.LibraryCard).
                Where(checkOutHistory => checkOutHistory.LibraryAsset.Asset_ID == ID);
        }

        public string GetCurrentHoldPatron(int hold_ID)
        {
            var hold = _context.Holds.Include(h => h.LibraryAsset).Include(c => c.LibraryCard)
                .FirstOrDefault(h => h.ID == hold_ID);
            var libraryCard_ID = hold?.LibraryCard.CardID;

            var patron = _context.Patrons.Include(p => p.Library_Card).FirstOrDefault(p => p.Library_Card.CardID == libraryCard_ID);
            return patron?.FirstName+ " " + patron?.LastName;

        }

        public IEnumerable<Hold> GetCurrentHolds(int ID)
        {
            return _context.Holds.Include(currentHolds => currentHolds.LibraryAsset).
                Where(currentHolds=>currentHolds.LibraryAsset.Asset_ID==ID);
        }

        public LoanedAsset LatestCheckout_Asset(int asset_ID)
        {
            return _context.LoanedAssets.Where(latestCheckout => latestCheckout.LibraryAsset.Asset_ID == asset_ID).
                OrderByDescending(latestCheckouts=>latestCheckouts.CheckoutDate)
                .FirstOrDefault();
        }

        public DateTime GetCurrentHoldTime(int ID)
        {
            return _context.Holds.Include(h => h.LibraryAsset).Include(c => c.LibraryCard)
                .FirstOrDefault(h => h.ID == ID).Hold_Placed;
        }

        public void MarkFound(int asset_ID)
        {
            
            UpdateAssetAvailability(asset_ID, "Available");

            var now = DateTime.Now;

            // Remove existing checkouts on the item
            RemoveExistingCheckouts(asset_ID);

            //Close checkout history and checkin item
            CloseCheckoutHistory(asset_ID, now);
            _context.SaveChanges();

        }

        private void UpdateAssetAvailability(int asset_ID, string asset_status)
        {
            var asset = _context.LibraryAssets.FirstOrDefault(asset => asset.Asset_ID == asset_ID);
            _context.Update(asset);

            asset.Availability = _context.AssetStatuses.FirstOrDefault(status => status.Status == asset_status);
        }

        private void CloseCheckoutHistory(int asset_ID, DateTime now)
        {
           var item_checkouthistory = _context.CheckOutHistories.FirstOrDefault(rh => rh.LibraryAsset.Asset_ID == asset_ID &&
           rh.CheckInDate == null);

            if (item_checkouthistory != null)
            {
                _context.Update(item_checkouthistory);
                item_checkouthistory.CheckInDate = now;
            }
        }

        private void RemoveExistingCheckouts(int asset_ID)
        {
            var Item_checkout = _context.LoanedAssets.FirstOrDefault(rc => rc.LibraryAsset.Asset_ID == asset_ID);

            if (Item_checkout != null)
            {
                _context.Remove(Item_checkout);
            }
        }

        public void MarkLost(int asset_ID)
        {
            UpdateAssetAvailability(asset_ID, "Lost"); ;
            _context.SaveChanges();

        }

        public void PlaceHold(int asset_ID, int libraryCard_ID)
        {
            var now = DateTime.Now;
            var asset = _context.LibraryAssets.FirstOrDefault(asset => asset.Asset_ID == asset_ID);
            var libraryCard = _context.LibraryCards.
                FirstOrDefault(a => a.CardID == libraryCard_ID);

            if (asset.Availability.Status == "Available")
            {
                UpdateAssetAvailability(asset_ID,"On Hold");
            }

            var placeHold = new Hold
            {
                LibraryAsset = asset,
                LibraryCard = libraryCard,
                Hold_Placed = now,
            };

            _context.Add(placeHold);
            _context.SaveChanges();
        }

        public string GetCurrentCheckOutPatron(int asset_ID)
        {
            var checkout = GetCheckOutbyAsset(asset_ID);
            if(checkout == null)
            {
                return "";
            };

            var CardID = checkout.LibraryCard.CardID;
            var patron = _context.Patrons.
                Include(p=>p.Library_Card)
                .FirstOrDefault(p=>p.Library_Card.CardID==CardID);

            return patron.FirstName + " " + patron.LastName;
        }

        private LoanedAsset GetCheckOutbyAsset(int asset_ID)
        {
            return _context.LoanedAssets.
                Include(la=>la.LibraryAsset).Include(lc=>lc.LibraryCard).
                FirstOrDefault(p => p.LibraryAsset.Asset_ID == asset_ID);
        }
    }
}
