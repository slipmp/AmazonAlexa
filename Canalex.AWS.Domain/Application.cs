using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Canalex.AWS.Domain
{
    public class Application
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Lender { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }

        public override string ToString()
        {
            return
                $"{nameof(Id)}: {Id}, {nameof(FirstName)}: {FirstName}, {nameof(LastName)}: {LastName}, {nameof(Lender)}: {Lender}, {nameof(Status)}: {Status}, {nameof(Date)}: {Date}";
        }
    }
}
