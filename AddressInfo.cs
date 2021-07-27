using System;
using System.Linq;

namespace XpoSqlProviderJson
{
    public class AddressInfo
    {

        public AddressInfo()
        {

        }
        public string Country { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public AddressInfo(string country, string state, string street)
        {
            Country = country;
            State = state;
            Street = street;
        }
    }
}
