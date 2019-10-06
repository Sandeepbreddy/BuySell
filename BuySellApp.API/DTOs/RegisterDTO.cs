using System.ComponentModel.DataAnnotations;

namespace BuySellApp.API.DTOs {
    public class RegisterDTO {
        [Required (ErrorMessage = "Please enter Username")]
        public string username { get; set; }

        [Required]
        [MinLength (8, ErrorMessage = "Please enter atleast minimum 8 characters")]
        public string password { get; set; }
    }
}