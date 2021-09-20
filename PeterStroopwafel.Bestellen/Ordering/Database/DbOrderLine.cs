using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Database {
    public class DbOrderLine {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Key { get; set; }
        public int Quantity { get; set; }
        public StroopwafelType StroopwafelType { get; set; }
        
        public string Supplier { get; set; }
        
        public decimal SalesPrice { get; set;}
        public decimal PurchasePrice { get; set; }
        public DbOrder DbOrder { get; set; }
    }
}