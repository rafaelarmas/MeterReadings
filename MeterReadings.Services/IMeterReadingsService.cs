using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace MeterReadings.Services
{
    public interface IMeterReadingsService
    {
        Task<MeterReadingUploadResponse> ProcessFile(IFormFile file);
    }
}
