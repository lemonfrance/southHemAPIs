using System.ComponentModel.DataAnnotations;
namespace SHITPurchase.Dtos
{
    public class OrderInputDto
    {
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}