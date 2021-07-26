using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpoSqlProviderJson
{
    public class Person : XPObject
    {
        public Person(Session session) : base(session)
        { }



        string jsonData;
        string name;

        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Name
        {
            get => name;
            set => SetPropertyValue(nameof(Name), ref name, value);
        }

        //[DbType("json")]
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string JsonData
        {
            get => jsonData;
            set => SetPropertyValue(nameof(JsonData), ref jsonData, value);
        }
    }
}
