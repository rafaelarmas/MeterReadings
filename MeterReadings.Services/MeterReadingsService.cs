using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using MeterReadings.Data.Context;
using MeterReadings.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace MeterReadings.Services
{
    public class MeterReadingsService : IMeterReadingsService
    {
        private readonly MeterReadingContext _context;

        public MeterReadingsService()
        {
            _context = new MeterReadingContext();
        }

        public async Task<MeterReadingUploadResponse> ProcessFile(IFormFile file)
        {
            var result = new MeterReadingUploadResponse();
            using (var reader = new StreamReader(file.OpenReadStream()))
            {
                while (reader.Peek() >= 0)
                {
                    var line = await reader.ReadLineAsync();
                    var success = await AddMeterReading(line);
                    if (success)
                        result.NumberOfSuccessfulReadings++;
                    else
                        result.NumberOfFailedReadings++;

                }
            }
            return result;
        }

        private async Task<bool> AddMeterReading(string lineText)
        {
            try
            {
                var meterReadingDetails = lineText.Split(',');

                if (meterReadingDetails.Length != 3) throw new Exception($"Invalid line was found: {lineText}");

                var accountNumber = meterReadingDetails[0];

                if (!int.TryParse(accountNumber, out int accountNumberInteger)) throw new Exception($"Account number is invalid: {accountNumber}");

                var account = await _context.Accounts.SingleOrDefaultAsync(x => x.AccountNumber == accountNumberInteger);

                if (account == null) throw new Exception($"Account was not found: {accountNumberInteger}");

                if (!DateTime.TryParse(meterReadingDetails[1], out DateTime readingDateTime)) throw new Exception($"Meter reading date is invalid: {meterReadingDetails[1]}");

                if (!int.TryParse(meterReadingDetails[2], out int readingValue)) throw new Exception($"Meter reading value is invalid: {meterReadingDetails[2]}");

                if (_context.MeterReadings.Any(x => x.Value == readingValue && x.Account.AccountNumber == accountNumberInteger && x.DateTime == readingDateTime))
                    throw new Exception($"Duplicate details were found: {lineText}");

                var meterReading = new MeterReading
                {
                    Account = account,
                    DateTime = readingDateTime,
                    Value = readingValue
                };

                _context.MeterReadings.Add(meterReading);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception e)
            {
                // TODO: Add to logs.
                Console.WriteLine(e);
                return false;
            }
        }
    }
}
