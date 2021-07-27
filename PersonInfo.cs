using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpoSqlProviderJson
{
    public class PersonInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CountryOfBirth { get; set; }
        public string CountryOfResidence { get; set; }
        public AddressInfo Address { get; set; }

    }
}
