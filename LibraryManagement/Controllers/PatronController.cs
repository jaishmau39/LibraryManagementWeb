using DataLibrary;
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
    }
}
