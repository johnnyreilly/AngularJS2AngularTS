using System.ComponentModel.DataAnnotations;

namespace PoorClaresAngular.Models
{
    public class PrayerRequest
    {
        [Required, EmailAddress]
        public string email { get; set; }

        [Required]
        public string prayFor { get; set; }
    }
}