using DevExpress.Xpo;
using NUnit.Framework;
using System.Data.SqlClient;

namespace XpoSqlProviderJson
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {

            //XpoProvider=MsSqlJson;Data Source=(local);User ID=username;Password=password;Initial Catalog=database;Persist Security Info=true
            MsSqlJson.Register();


            SqlConnection cnn;
            var xpocnx = "Data Source=localhost;User ID=sa;Password=JoseManuel16;Initial Catalog=TestJson;Persist Security Info=true";
            cnn = new SqlConnection(xpocnx);


            MsSqlJson msSqlJson = new MsSqlJson(cnn, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);

            string conn = "XpoProvider=MSSqlServer;Data Source=MSI;User ID=sa;Password=JoseManuel16;Initial Catalog=Ajson;Persist Security Info=true";
            //IDataLayer dl = XpoDefault.GetDataLayer(conn, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            IDataLayer dl = new SimpleDataLayer(msSqlJson);
            using (Session session = new Session(dl))
            {
                System.Reflection.Assembly[] assemblies = new System.Reflection.Assembly[] {
      
               typeof(Person).Assembly
           };
                session.UpdateSchema(assemblies);
                session.CreateObjectTypeRecords(assemblies);
            }

        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}