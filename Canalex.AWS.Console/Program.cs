using System;
using System.Globalization;
using Canalex.AWS.BizRules;

namespace Canalex.AWS.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ConnectToDatabase();
        }

        public static void ConnectToDatabase()
        {
            var lenderBizRule = new LenderBizRule();

            System.Console.WriteLine(lenderBizRule.GetApplicationStatus("Soares"));

            System.Console.WriteLine(lenderBizRule.GetMyFavouriteLender("09/27/2017"));

            System.Console.WriteLine(lenderBizRule.GetMyFavouriteLender("09/28/2017"));

            var tomorrow = DateTime.Today.AddDays(1).Date.ToString(CultureInfo.InvariantCulture);
            System.Console.WriteLine(lenderBizRule.GetMyFavouriteLender(tomorrow));

            System.Console.WriteLine(lenderBizRule.GetDealsApprovedToday());

            System.Console.WriteLine(lenderBizRule.GetDealsStuckInSu());

            System.Console.WriteLine(lenderBizRule.GetDealsDeclined("RBC"));

            System.Console.WriteLine(lenderBizRule.GetDealsDeclined("TD"));
            System.Console.ReadKey();
        }
    }
}
