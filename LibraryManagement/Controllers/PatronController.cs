using DataLibrary;
using DataLibrary.Models;
using LibraryManagement.Models.Patron;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagement.Controllers
{
    public class PatronController:Controller
    {
        private IPatron _patron;
        public PatronController(IPatron patron)
        {
            _patron = patron;
        }
        public IActionResult Index()
        {
            var AllPatrons = _patron.GetAll();
            var AllPatrons_Model = AllPatrons.Select(pt => new PatronInfo
            {
                Id=pt.ID,
                FirstName=pt.FirstName,
                LastName=pt.LastName,
                Library_CardId=pt.Library_Card.CardID,
                Overdue_Fees=pt.Library_Card.Overdue_Fee,
                LibraryBranch=pt.Branch_Location.Branch_Name
            }).ToList();
            var patronList_model = new PatronList()
            {
                Patrons_List = AllPatrons_Model
            };
            return View(patronList_model);
        }

        public IActionResult PatronInformation(int Id) {

            var patron_info = _patron.Get(Id);
            var patronInfo_Model = new PatronInfo
                {
                FirstName = patron_info.FirstName,
                LastName = patron_info.LastName,
                Library_CardId = patron_info.Library_Card.CardID,
                Overdue_Fees = patron_info.Library_Card.Overdue_Fee,
                LibraryBranch = patron_info.Branch_Location.Branch_Name,
                Address =patron_info.Address,
                Holds = _patron.GetHolds(Id),
                Telephone = patron_info.TelephoneNumber,
                StartDate = patron_info.Library_Card.Created_Date,
                CheckOut_History = _patron.GetCheckOutHistory(Id),
                CheckedOut_Assets = _patron.GetCheckOuts(Id).ToList()?? new List<LoanedAsset>()
            };
            return View(patronInfo_Model);
        
        }
    }
}
