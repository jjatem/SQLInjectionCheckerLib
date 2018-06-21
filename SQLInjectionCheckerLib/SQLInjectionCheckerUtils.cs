using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SQLInjectionCheckerLib
{
    public static class SQLInjectionCheckerUtils
    {
        /// <summary>
        /// Helper to extract all constats values from a static constants class
        /// </summary>
        /// <returns>The all public constant values.</returns>
        /// <param name="type">Type.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static List<T> GetAllPublicConstantValues<T>(this Type type)
        {
            return type
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(fi => fi.IsLiteral && !fi.IsInitOnly && fi.FieldType == typeof(T))
                .Select(x => (T)x.GetRawConstantValue())
                .ToList();
        }

        /// <summary>
        /// Gets the blacklist constant keywords as a string word list
        /// </summary>
        /// <returns>The sql injection keywords black list.</returns>
        public static List<string> GetSqlInjectionKeywordsBlackList()
        {
            List<string> results = new List<string>();

            results = typeof(SQLInjectionBlackListConstants).GetAllPublicConstantValues<string>();

            return results;
        }

        /// <summary>
        /// Checks any string value for SQL Injection.
        /// </summary>
        /// <returns><c>true</c>, if for SQLI njection was checked, <c>false</c> otherwise.</returns>
        /// <param name="userInput">User input.</param>
        public static bool CheckForSQLInjection(this string userInput)
        {
            bool isSQLInjection = false;

            string[] sqlBlackList = GetSqlInjectionKeywordsBlackList().ToArray();
            string CheckString = userInput.Replace("'", "''");

            for (int i = 0; i <= sqlBlackList.Length - 1; i++)
            {
                if ((CheckString.IndexOf(sqlBlackList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                { 
                    isSQLInjection = true; 
                }
            }

            return isSQLInjection;
        }


        /// <summary>
        /// Checks all public properties of any object type for sql injection attacks.
        /// </summary>
        /// <returns><c>true</c>, if all public properties for sql injection was checked, <c>false</c> otherwise.</returns>
        /// <param name="Obj">Object.</param>
        /// <param name="ErrorMessage">Error message.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public static bool CheckAllPublicPropertiesForSqlInjection<T>(this T Obj, out string ErrorMessage)
        {
            bool PassedChecks = true;
            ErrorMessage = string.Empty;

            if (Obj != null)
            {
                var properties = (from prop in Obj.GetType()
                                  .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                  where
                                    prop.PropertyType == typeof(string) &&
                                    prop.CanWrite &&
                                    prop.CanRead &&
                                    !prop.Name.ToUpper().Contains("SQL")
                                  select prop).ToList();

                foreach(PropertyInfo prop in properties)
                {
                    string propValue = prop.GetValue(Obj, null).ToString();

                    if (propValue.CheckForSQLInjection())
                    {
                        ErrorMessage = $@"Invalid User Input Provided for Member [{prop.Name}]. A possible Sql Injection Attack Detected on the following input
                        [""{propValue}""]";
                        return false; //exit with failed validation
                    }
                }
            }

            return PassedChecks;
        }
    }
}
