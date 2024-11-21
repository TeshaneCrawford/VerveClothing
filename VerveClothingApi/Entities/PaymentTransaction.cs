using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace VerveClothingApi.Entities
{
    public class PaymentTransaction
    {
        public int TransactionId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
        [Required]
        public string PaymentMethod { get; set; }
        [Required]
        public string Status { get; set; }
        public DateTime TransactionDate { get; set; }
    }
}
