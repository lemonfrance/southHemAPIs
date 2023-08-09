using System.ComponentModel.DataAnnotations;

namespace SHITPurchase.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string UserName { get; set; }
        [Required]
        public int ProductID { get; set; }
        [Required]
        public int Quantity { get; set; }
    }
}