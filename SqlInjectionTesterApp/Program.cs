using System;
using SQLInjectionCheckerLib;

namespace SqlInjectionTesterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Checking account POCO class for SQL injection....");

            var Acct = new Account();
            Acct.AccountId = 1234567;
            Acct.AccountNumber = "508527SAQ";
            Acct.AccountName = "Willys Insurance &;TRUNCATE TABLE account;Sons incorporated";
            Acct.AccountType = "A/*SELECT * FROM SALARY*/";
            Acct.MarketCode = "CO";
            Acct.SelectSql = "SELECT * FROM mbsi.account WHERE account_number_full={0};";

            string ErrMsg = string.Empty;
            if (SQLInjectionCheckerUtils.CheckAllPublicPropertiesForSqlInjection<Account>(Acct, out ErrMsg))
            {
                Console.WriteLine("SQL Injection Attack Detected");
                Console.WriteLine(ErrMsg);
            }

            Console.WriteLine("Press Any Key to Exit...");
            var keystroke = Console.Read();


        }
    }

    public class Account
    {
        public int AccountId { get; set;}
        public string AccountNumber { get; set; }
        public string AccountName { get; set; }
        public string AccountType { get; set; }
        public string MarketCode { get; set; }
        public string SelectSql { get; set; }
    }
}
