using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;
using Canalex.AWS.Domain;
using System.Data.SqlClient;

namespace Canalex.AWS.Data
{
    public interface IApplicationRepository
    {
        IList<Application> GetAll();

        IList<Application> GetMostCreatedDealsLender(DateTime date);
    }
    public class ApplicationRepository: IApplicationRepository
    {
        public IList<Application> GetAll()
        {
            var result = new List<Application>();

            using (var conn = new SqlConnection(SQLConnection.connetionString))
            {
                using (var cmd = new SqlCommand("SELECT * FROM Deals", conn))
                {
                    conn.Open();

                    using (var r = cmd.ExecuteReader())
                    {
                        foreach (DbDataRecord s in r)
                        {
                            var application = new Application();

                            application.Id = s.GetInt32(0);
                            application.FirstName = s.GetString(1);
                            application.LastName= s.GetString(2);
                            application.Lender= s.GetString(3);
                            application.Status= s.GetString(4);
                            application.Date= s.GetDateTime(5);

                            result.Add(application);
                        }
                    }
                }
            }

            return result;
        }

        public IList<Application> GetMostCreatedDealsLender(DateTime date)
        {
            var applications = GetAll();

            var query = from e in applications
                where e.Date.Day == date.Day && e.Date.Month == date.Month && e.Date.Year == date.Year
                group e by e.Lender
                into r
                select new {Lender = r.Key, NumberOfDealsToday = r.Count()};

            var result = query.ToList().OrderByDescending(x => x.NumberOfDealsToday).FirstOrDefault();

            return applications.Where(e => e.Lender == result.Lender &&
            e.Date.Day == date.Day && e.Date.Month == date.Month && e.Date.Year == date.Year
            ).ToList();
        }
    }
}
