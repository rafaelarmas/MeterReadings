using System.ComponentModel.DataAnnotations;

namespace MeterReadings.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public int AccountNumber { get; set; }
        [StringLength(255)]
        public string FirstName { get; set; }
        [StringLength(255)]
        public string LastName { get; set; }
    }
}
