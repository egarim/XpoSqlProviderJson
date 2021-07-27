using DevExpress.Data.Filtering;
using System;
using System.Linq;

namespace XpoSqlProviderJson
{
    public class JsonQuery : ICustomFunctionOperatorFormattable
    {
        #region ICustomFunctionOperator Members
        // Evaluates the function on the client
        public object Evaluate(params object[] operands)
        {
            // Full text search is not available on the client and should not be used there
            throw new NotImplementedException();
        }
        public string Name
        {
            get { return nameof(JsonQuery); }
        }
        public Type ResultType(params Type[] operands)
        {
            return typeof(string);
        }
        #endregion
        #region ICustomFunctionOperatorFormattable Members
        // The function's expression to be evaluated on the server
        public string Format(Type providerType, params string[] operands)
        {
            // This example implements the function for MsSqlJson databases only
            if (providerType == typeof(MsSqlJson))
            {
                return $"JSON_QUERY({operands[0]}, {operands[1]})";
            }
            //return string.Format("JSON_VALUE({0}, {1}) ", operands[0], operands[1]);

            throw new NotSupportedException(string.Concat("This provider is not supported: ",
                providerType.Name));
        }
        #endregion
    }
}
