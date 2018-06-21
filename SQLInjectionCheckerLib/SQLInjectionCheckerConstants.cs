using System;
namespace SQLInjectionCheckerLib
{
    public static class SQLInjectionBlackListConstants
    {
        public const string COMMENTS1 = "--";
        public const string COMMENTS2 = ";--";
        public const string SEMICOLON = ";";
        public const string COMMENTS3 = "/*";
        public const string COMMENTS4 = "*/";
        public const string PARAMETERIZED1 = "@@";
        public const string PARAMETERIZED2 = "@";
        public const string CHAR_DATATYPE = "char";
        public const string NCHAR_DATATYPE = "nchar";
        public const string VARCHAR_DATATYPE = "varchar";
        public const string NVARCHAR_DATATYPE = "nvarchar";
        public const string ALTER = "alter";
        public const string BEGIN = "begin";
        public const string CAST = "cast";
        public const string CREATE = "create";
        public const string CURSOR = "cursor";
        public const string DECLARE = "declare";
        public const string DELETE = "delete";
        public const string DROP = "drop";
        public const string END = "end";
        public const string EXECUTE = "execute";
        public const string FETCH = "fetch";
        public const string INSERT = "insert";
        public const string KILL = "kill";
        public const string SELECT = "select";
        public const string TRUNCATE = "truncate";
        public const string TABLE = "table";
        public const string UPDATE = "update";
        public const string REPLACE = "replace";
        public const string WHERE = "where";
    }
}
