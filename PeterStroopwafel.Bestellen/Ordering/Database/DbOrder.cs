using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ordering.Database {
    public class DbOrder {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        
        public int Key { get; set; }
        public string CustomerName { get; set; }
        public DateTime WishDate { get; set; }

        public DateTime Created { get; set; } = DateTime.Now;
        
        public decimal TotalSalesPrice { get; set; }

        public ICollection<DbOrderLine> OrderLines { get; set; }
    }
}