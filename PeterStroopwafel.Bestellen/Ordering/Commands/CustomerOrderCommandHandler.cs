using System;
using System.Collections.Generic;
using System.Linq;
using Ordering.Database;
using Ordering.Queries;

namespace Ordering.Commands {
    public class CustomerOrderCommandHandler : ICommandHandler<CustomerOrderCommand> {
        private readonly OrderContext _orderContext;
        private readonly CustomerQuotesQueryHandler _customerQuotesQueryHandler;
        private readonly ICommandHandler<OrderCommand> _orderCommandHandler;

        public CustomerOrderCommandHandler(
            OrderContext orderContext, CustomerQuotesQueryHandler customerQuotesQueryHandler,
            ICommandHandler<OrderCommand> orderCommandHandler) {
            _orderContext = orderContext;
            _customerQuotesQueryHandler = customerQuotesQueryHandler;
            _orderCommandHandler = orderCommandHandler;
        }

        public void Handle(CustomerOrderCommand command) {
            var selectQuote = _customerQuotesQueryHandler.Handle(new QuotesQuery(command.OrderLines));
            var supplierName = selectQuote.PreferredQuote.Supplier.Name;

            _orderCommandHandler.Handle(new OrderCommand(command.OrderLines, supplierName));

            var dbOrder = new DbOrder {
                CustomerName = command.CustomerName,
                WishDate = command.WishDate,
                TotalSalesPrice = selectQuote.TotalPrice
            };

            var orderLines = new List<DbOrderLine>();

            foreach (var orderLine in command.OrderLines) {
                orderLines.Add(new DbOrderLine {
                    Quantity = orderLine.Value,
                    StroopwafelType = orderLine.Key,
                    Supplier = supplierName,
                });
            }

            dbOrder.OrderLines = orderLines;
            _orderContext.Add(dbOrder);
            _orderContext.SaveChanges();
        }
    }
}