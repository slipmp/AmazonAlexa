using System;
using System.Linq;
using Canalex.AWS.Data;

namespace Canalex.AWS.BizRules
{
    public interface ILenderBizRule
    {
        string GetDealsApprovedToday();
        string GetMyFavouriteLender(string date);
        string GetApplicationStatus(string lastName);
        string GetDealsDeclined(string lender);
    }

    public class LenderBizRule: ILenderBizRule
    {
        public string GetDealsApprovedToday()
        {
            var applicationRepository = new ApplicationRepository();
            var applications = applicationRepository.GetAll();

            var applicationsToday = applications.Where(e =>
                e.Date.Day == DateTime.Today.Day && e.Date.Month == DateTime.Today.Month &&
                e.Date.Year == DateTime.Today.Year &&
                e.Status.ToUpper() == "AP"
            ).ToList();

            var result = $"There are {applicationsToday.Count} deals approved today. ";

            var listOfLenders = from e in applicationsToday
                group e by e.Lender
                into r
                select new {Lender = r.Key, NumberOfDealsToday = r.Count()};

            foreach (var lenderDeals in listOfLenders)
            {
                result += $"{lenderDeals.NumberOfDealsToday} deals with {lenderDeals.Lender}. ";
            }

            return result;
        }

        public string GetDealsDeclined(string lender)
        {
            var applicationRepository = new ApplicationRepository();
            var applications = applicationRepository.GetAll();

            var applicationsToday = applications.Where(e =>
                e.Status.ToUpper() == "DE" && e.Lender.ToUpper() == lender.ToUpper()
            ).ToList();

            var result =
                $"There are {applicationsToday.Count} deal{(applicationsToday.Count > 1 ? "s" : "")} declined for {lender}. ";
            
            return result;
        }

        public string GetMyFavouriteLender(string date)
        {
            DateTime dateTime;
            DateTime.TryParse(date, out dateTime);
            if (dateTime == DateTime.MinValue)
                return "I did not understand the given date";

            if (dateTime > DateTime.Today)
                return "I am not able to predict the future...yet!";

            var verbToBe = dateTime.Date.Date == DateTime.Today ? "is" : "was";

            var applicationRepository = new ApplicationRepository();
            var applications = applicationRepository.GetMostCreatedDealsLender(dateTime);

            // ReSharper disable once PossibleNullReferenceException
            var lender = applications.FirstOrDefault().Lender;

            return $"My favourite lender on {date} {verbToBe} {lender} with {applications.Count} total applications";
        }

        public string GetApplicationStatus(string lastName)
        {
            return $"The application status for {lastName} is not approved";
        }
        public string GetDealsStuckInSu()
        {
            var applicationRepository = new ApplicationRepository();
            var applications = applicationRepository.GetAll();

            var applicationsInSu = applications.Where(e =>e.Status.ToUpper() == "SU"
            ).ToList();

            var result = $"There are {applicationsInSu.Count} deals in SU";

            return result;
        }
    }
}
