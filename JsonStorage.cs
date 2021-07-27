using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpoSqlProviderJson
{
    public class JsonStorage : XPObject
    {
        public JsonStorage(Session session) : base(session)
        { }



        string id;
        string jsonData;



        
        [Size(SizeAttribute.DefaultStringMappingFieldSize)]
        public string Id
        {
            get => id;
            set => SetPropertyValue(nameof(Id), ref id, value);
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
