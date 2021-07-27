using DevExpress.Data.Filtering;
using DevExpress.Xpo;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Data.SqlClient;
using System.Diagnostics;


namespace XpoSqlProviderJson
{
    public class Tests
    {
        IDataLayer dl;
        [SetUp]
        public void Setup()
        {
            //Hack https://docs.microsoft.com/en-us/sql/relational-databases/json/store-json-documents-in-sql-tables?view=sql-server-ver15

            //XpoProvider=MsSqlJson;Data Source=(local);User ID=username;Password=password;Initial Catalog=database;Persist Security Info=true
            MsSqlJson.Register();


            SqlConnection cnn;
            var xpocnx = "Data Source=localhost;User ID=sa;Password=JoseManuel16;Initial Catalog=Ajson;Persist Security Info=true";
            cnn = new SqlConnection(xpocnx);


            MsSqlJson msSqlJson = new MsSqlJson(cnn, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);

            //string conn = "XpoProvider=MSSqlServer;Data Source=MSI;User ID=sa;Password=JoseManuel16;Initial Catalog=Ajson;Persist Security Info=true";
            //IDataLayer dl = XpoDefault.GetDataLayer(conn, DevExpress.Xpo.DB.AutoCreateOption.DatabaseAndSchema);
            dl = new SimpleDataLayer(msSqlJson);
            using (Session session = new Session(dl))
            {
                System.Reflection.Assembly[] assemblies = new System.Reflection.Assembly[] { typeof(JsonStorage).Assembly };
                session.UpdateSchema(assemblies);
                session.CreateObjectTypeRecords(assemblies);
            }
            CreateTestData();

            CriteriaOperator.RegisterCustomFunction(new JsonSimpleEqual());
            CriteriaOperator.RegisterCustomFunction(new JsonValue());
            CriteriaOperator.RegisterCustomFunction(new JsonQuery());
        }

        private void CreateTestData()
        {
            PersonInfo JocheInfo = new PersonInfo() 
            {
                FirstName = "Joche", LastName = "Ojeda", CountryOfBirth = "El Salvador", CountryOfResidence = "Russian Federation" ,
                Address=new AddressInfo("Russian Federation","Saint Petersburg","Nevsky prospect")

            };
            PersonInfo JavierInfo = new PersonInfo() 
            { 
                FirstName = "Javier", LastName = "Columbie", CountryOfBirth = "Cuba", CountryOfResidence = "USA",
                Address=new AddressInfo("USA","Arizona","Arizona Street")
            };


            LookupInfo Countries = new LookupInfo("EL Salvador", "El Salvador", "Cuba", "Republica dominicana", "Chile", "Argentina", "Colombia");
            LookupInfo Languages = new LookupInfo("ES", "ES", "RU", "EN", "IT", "FR", "PT");

            UnitOfWork unitOfWork = new UnitOfWork(this.dl);


            XPCollection<JsonStorage> ObjectsToDelete = new XPCollection<JsonStorage>(unitOfWork);
            unitOfWork.Delete(ObjectsToDelete);
            unitOfWork.PurgeDeletedObjects();
            unitOfWork.CommitChanges();


            JsonStorage Joche = new JsonStorage(unitOfWork) { Id = "1", JsonData = JsonConvert.SerializeObject(JocheInfo, Formatting.Indented) };
            JsonStorage Javier = new JsonStorage(unitOfWork) { Id = "2", JsonData = JsonConvert.SerializeObject(JavierInfo, Formatting.Indented) };

            JsonStorage CountriesLookup = new JsonStorage(unitOfWork) { Id = "3", JsonData = JsonConvert.SerializeObject(Countries, Formatting.Indented) };
            JsonStorage LanguageLookup = new JsonStorage(unitOfWork) { Id = "4", JsonData = JsonConvert.SerializeObject(Languages, Formatting.Indented) };


            unitOfWork.CommitChanges();
        }

        [Test]
        public void SimpleEqualTest()
        {

          


            UnitOfWork unitOfWork = new UnitOfWork(this.dl);

            CriteriaOperator criteriaOperator = CriteriaOperator.Parse("JsonSimpleEqual(JsonData, '$.CountryOfResidence','EL Salvador')");
            Debug.WriteLine(criteriaOperator);
            XPCollection<JsonStorage> JsonObjects = new XPCollection<JsonStorage>(PersistentCriteriaEvaluationBehavior.InTransaction, unitOfWork, criteriaOperator);
       

         

            foreach (JsonStorage jsonStorage in JsonObjects)
            {
                Debug.WriteLine($"{jsonStorage.Id} - {jsonStorage.JsonData}");
            }

            Assert.AreEqual(1, JsonObjects.Count);
        }


        [Test]
        public void JsonValueTest()
        {

          


            UnitOfWork unitOfWork = new UnitOfWork(this.dl);

            CriteriaOperator criteriaOperator = CriteriaOperator.Parse("JsonValue(JsonData, '$.CountryOfResidence') = 'Russian Federation'");
            Debug.WriteLine(criteriaOperator);
            XPCollection<JsonStorage> JsonObjects = new XPCollection<JsonStorage>(PersistentCriteriaEvaluationBehavior.InTransaction, unitOfWork, criteriaOperator);




            foreach (JsonStorage jsonStorage in JsonObjects)
            {
                Debug.WriteLine($"{jsonStorage.Id} - {jsonStorage.JsonData}");
            }

            Assert.AreEqual(1, JsonObjects.Count);
        }
        [Test]
        public void JsonValueNestedObjectTest()
        {




            UnitOfWork unitOfWork = new UnitOfWork(this.dl);

            CriteriaOperator criteriaOperator = CriteriaOperator.Parse("JsonValue(JsonData, '$.Address.State') = 'Arizona'");
            Debug.WriteLine(criteriaOperator);
            XPCollection<JsonStorage> JsonObjects = new XPCollection<JsonStorage>(PersistentCriteriaEvaluationBehavior.InTransaction, unitOfWork, criteriaOperator);




            foreach (JsonStorage jsonStorage in JsonObjects)
            {
                Debug.WriteLine($"{jsonStorage.Id} - {jsonStorage.JsonData}");
            }

            Assert.AreEqual(1, JsonObjects.Count);
        }
        [Test]
        public void SimpleEqualNestedObjectTest()
        {

        


            UnitOfWork unitOfWork = new UnitOfWork(this.dl);

            CriteriaOperator criteriaOperator = CriteriaOperator.Parse("JsonSimpleEqual(JsonData, '$.Address.State','Arizona')");
            Debug.WriteLine(criteriaOperator);
            XPCollection<JsonStorage> JsonObjects = new XPCollection<JsonStorage>(PersistentCriteriaEvaluationBehavior.InTransaction, unitOfWork, criteriaOperator);




            foreach (JsonStorage jsonStorage in JsonObjects)
            {
                Debug.WriteLine($"{jsonStorage.Id} - {jsonStorage.JsonData}");
            }

            Assert.AreEqual(1, JsonObjects.Count);
        }
        [Test]
        public void JsonQuery()
        {

            //HACK https://docs.devexpress.com/XPO/5206/examples/how-to-implement-a-custom-criteria-language-function-operator


            UnitOfWork unitOfWork = new UnitOfWork(this.dl);

            CriteriaOperator criteriaOperator = CriteriaOperator.Parse("JsonSimpleEqual(JsonData, '$.SelectedValue','El Salvador')");
            Debug.WriteLine(criteriaOperator);
           


            XPView view = new XPView(unitOfWork, typeof(JsonStorage), "Id", criteriaOperator);
            // Using MyGetMonth in an XPView column's expression.
            view.AddProperty("Values", "JsonQuery(JsonData,'$.Values')");

            foreach (ViewRecord prop in view)
            {
                Debug.WriteLine(prop["Id"]);
                Debug.WriteLine("Values: " + prop["Values"]);
            }




          
            //Assert.AreEqual(1, JsonObjects.Count);
        }
    }
}