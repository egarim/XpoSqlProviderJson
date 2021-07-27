using DevExpress.Data.Filtering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XpoSqlProviderJson
{
    //HACK https://docs.devexpress.com/XPO/3246/examples/how-to-implement-a-full-text-search
    public class JsonSimpleEqual : ICustomFunctionOperatorFormattable
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
            get { return nameof(JsonSimpleEqual); }
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
                return $"JSON_VALUE({operands[0]}, {operands[1]}) = {operands[2]}";
            }
                //return string.Format("JSON_VALUE({0}, {1}) ", operands[0], operands[1]);

            throw new NotSupportedException(string.Concat("This provider is not supported: ",
                providerType.Name));
        }
        #endregion
    }



    //HACK https://docs.devexpress.com/XPO/3246/examples/how-to-implement-a-full-text-search
    public class JsonValue : ICustomFunctionOperatorFormattable
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
            get { return nameof(JsonValue); }
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
                return $"JSON_VALUE({operands[0]}, {operands[1]})";
            }
            //return string.Format("JSON_VALUE({0}, {1}) ", operands[0], operands[1]);

            throw new NotSupportedException(string.Concat("This provider is not supported: ",
                providerType.Name));
        }
        #endregion
    }

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
