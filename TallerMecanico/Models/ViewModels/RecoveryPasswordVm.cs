using System.ComponentModel.DataAnnotations;

namespace TallerMecanico.Models.ViewModels
{
    public class RecoveryPasswordVm
    {
        public string token { get; set; }

        [Required]
        public string Password { get; set; }

        [Compare("Password")]
        [Required]
        public string Password2 { get; set; }
    }
}
