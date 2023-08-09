using System.ComponentModel.DataAnnotations;

namespace SHITPurchase.Models
{
    public class User
    {
        [Key]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
    }
}