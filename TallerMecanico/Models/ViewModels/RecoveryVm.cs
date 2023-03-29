using System.ComponentModel.DataAnnotations;

namespace TallerMecanico.Models.ViewModels
{
    public class RecoveryVm
    {
        [EmailAddress]
        [Required]
        public string Email { get; set; }
    }
}
