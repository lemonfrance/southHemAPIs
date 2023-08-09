using System.ComponentModel.DataAnnotations;
namespace SHITPurchase.Dtos
{
    public class UserInputDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string Address { get; set; }
    }
}