using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Ordering;
using Ordering.Commands;
using Ordering.Queries;

namespace PeterStroopwafel.Website.Components {
    public class OrderViewModel {
        private readonly CustomerQuotesQueryHandler _customerQuotesQuery;

        public OrderViewModel(CustomerQuotesQueryHandler customerQuotesQuery) {
            _customerQuotesQuery = customerQuotesQuery;
        }
        
        
        public string CustomerName { get; set; }
        
        public DateTime WishDate { get; set; } = DateTime.Now.AddDays(5);


        public IList<OrderRow> OrderRows { get; set; } = new List<OrderRow> {
            new OrderRow {
                Amount = 0,
                Type = StroopwafelType.Gewoon
            },
            new OrderRow {
                Amount = 0,
                Type = StroopwafelType.Suikervrij
            },
            new OrderRow {
                Amount = 0,
                Type = StroopwafelType.Super
            }
        };
        
        
        public List<KeyValuePair<StroopwafelType, int>> GetOrderLines() {
            var lines = new List<KeyValuePair<StroopwafelType, int>>();

            foreach (var row in OrderRows) {
                lines.Add(new KeyValuePair<StroopwafelType, int>(row.Type,row.Amount));
            }

            return lines;
        }

        public CustomerQuote GetCustomerQuote() {
            return _customerQuotesQuery.Handle(new QuotesQuery(GetOrderLines()));
        }

        public CustomerOrderCommand GetCustomerOrderCommand() {
            return new CustomerOrderCommand(CustomerName, WishDate, GetOrderLines());
        }

        public IList<string> GetMessages() {

            var messages = new List<string>();
            if (string.IsNullOrWhiteSpace(CustomerName)) {
                messages.Add("Vul a.u.b u naam in.");
            }
            
            
            if (WishDate <= DateTime.Now) {
                messages.Add("Gebruik een ophaal datum welke in de toekomst ligt.");
            }
            
            if (OrderRows.Sum(x=>x.Amount) == 0 ) {
                messages.Add("Bestel 1 of meer producten.");
            }

            return messages;

        }

        public bool IsValid => !GetMessages().Any();
    }

    public class OrderRow {
        [Required]
        public int Amount { get; set; }

        public StroopwafelType Type { get; set; }

        public string DisplayName => Type.ToString();

    }
}