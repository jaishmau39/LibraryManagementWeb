using DataLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryServices
{
    public class ReadableDates
    {
        public static List<string> DisplayTimes(IEnumerable<BranchTimes> branchTimings)
        {
            var hours = new List<string>();
            foreach (var t in branchTimings)
            {
                var day = displayDay(t.Day_of_Week);
                var OpenTime = displayTime(t.OpeningTime);
                var CloseTime = displayTime(t.ClosingTime);

                var time = $"{day} {OpenTime} to {CloseTime}";
                hours.Add(time);
            }
            return hours;
        }
        public static string displayDay(int DayNumber)
        {
            return Enum.GetName(typeof(DayOfWeek), DayNumber);
        }
        public static string displayTime(int OperationTime)
        {
            return TimeSpan.FromHours(OperationTime).ToString("hh':'mm");
        }
    }
}
