using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpoSqlProviderJson
{
    public class LookupInfo
    {
        public string SelectedValue { get; set; }
        public List<string> Values { get; set; }
        public LookupInfo(string selectedValue,params string[] values)
        {
            SelectedValue = selectedValue;
            Values = new List<string>();
            foreach (string item in values)
            {
                Values.Add(item);
            }
           
        }
    }
}
