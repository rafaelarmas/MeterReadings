using System;
using System.IO;
using System.Linq;
using MeterReadings.Data.Entities;

namespace MeterReadings.Data.Context
{
    public static class MeterReadingContextExtensions
    {
        public static void SeedData(this MeterReadingContext context)
        {
            //var account = new Account{ AccountNumber = "1", FirstName = "Bob", Lastname = "Smith" };
            // Only add seed data if there are no existing accounts.
            if (context.Accounts.Any()) return;

            var accountText = File.ReadLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "test_Accounts.csv"));

            foreach (var accountString in accountText)
            {
                if (!IsAccountTextValid(accountString)) continue;

                var accountDetails = accountString.Split(',');
                var account = new Account
                {
                    AccountNumber = int.Parse(accountDetails[0]),
                    FirstName = accountDetails[1],
                    LastName = accountDetails[2]
                        
                };
                context.Accounts.Add(account);
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Validate data in CSV file.
        /// </summary>
        /// <param name="accountString">A single line from CSV file.</param>
        /// <returns></returns>
        private static bool IsAccountTextValid(string accountString)
        {
            var accountDetails = accountString.Split(',');

            // TODO: Add to log file when errors are found.
            if (accountDetails.Length != 3)
                return false;

            if (!int.TryParse(accountDetails[0], out int accountId))
                return false;

            return true;
        }
    }
}
