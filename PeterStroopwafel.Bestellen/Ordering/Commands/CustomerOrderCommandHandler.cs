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

        public void Handle(CustomerOrderCommand command)
        {
            var customerQuote = _customerQuotesQueryHandler.Handle(new QuotesQuery(command.OrderLines.ToList(),command.WishDate));
            
            foreach (var orderCommand in customerQuote.GetOrderCommands())
            {
                _orderCommandHandler.Handle(orderCommand);
            }
            
            var dbOrder = new DbOrder {
                CustomerName = command.CustomerName,
                WishDate = command.WishDate,
                TotalSalesPrice = customerQuote.TotalPrice
            };

            var orderLines = new List<DbOrderLine>();


            foreach (var orderLinesPerSupplier in customerQuote.SupplierQuotes)
            {
                var supplierName = orderLinesPerSupplier.Supplier.Name;
                foreach (var orderLine in orderLinesPerSupplier.OrderLines)
                {
                    orderLines.Add(new DbOrderLine {
                        Quantity = orderLine.Amount,
                        StroopwafelType = orderLine.Stroopwafel.Type,
                        Supplier = supplierName,
                    });
                }
            }

            dbOrder.OrderLines = orderLines;
            _orderContext.Add(dbOrder);
            _orderContext.SaveChanges();
        }
    }
}