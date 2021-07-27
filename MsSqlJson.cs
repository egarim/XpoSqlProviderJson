using DevExpress.Xpo.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpoSqlProviderJson
{
    public class MsSqlJson: MSSqlConnectionProvider
    {
        /// <summary>
        /// <para>Initializes a new instance of the <see cref="MsSqlJson"/> class with the specified settings.</para>
        /// </summary>
        /// <param name="connection">An object implementing the <see cref="System.Data.IDbConnection"/> interface that represents an open connection to a data source.</param>
        /// <param name="autoCreateOption">An <see cref="DevExpress.Xpo.DB.AutoCreateOption"/> value that specifies which operations are performed when storage is accessed for the first time. This parameter is used to initialize the <see cref="DevExpress.Xpo.DB.IDataStore.AutoCreateOption"/> property.</param>
        public MsSqlJson(IDbConnection connection, AutoCreateOption autoCreateOption) : base(connection, autoCreateOption)
        {

        }
        protected override object ReformatReadValue(object value, ReformatReadValueArgs args)
        {

            //if (value != null)
            //{

            //    Type valueType = value.GetType();

            //    if (valueType == typeof(SqlGeography) || valueType == typeof(SqlGeometry))

            //        return value.ToString();

            //}

            return base.ReformatReadValue(value, args);

        }
        protected override string GetSqlCreateColumnTypeForString(DBTable table, DBColumn column)
        {
            if (column.Name== "JsonData")
            {
                string Value = $"nvarchar(max) CONSTRAINT [Content should be formatted as JSON] CHECK ( ISJSON({column.Name})>0 )";
                return Value;
            }

            return base.GetSqlCreateColumnTypeForString(table, column);
        }

        public new static void Register()
        {

            DataStoreBase.RegisterDataStoreProvider(nameof(MsSqlJson), CreateProviderFromString);
            //RegisterDataStoreProvider("System.Data.SqlClient.SqlConnection", new DataStoreCreationFromConnectionDelegate(CreateProviderFromConnection));
            //RegisterDataStoreProvider("Microsoft.Data.SqlClient.SqlConnection", new DataStoreCreationFromConnectionDelegate(CreateProviderFromConnection));
            //RegisterFactory(new MSSqlProviderFactory());
        }
    }
}
