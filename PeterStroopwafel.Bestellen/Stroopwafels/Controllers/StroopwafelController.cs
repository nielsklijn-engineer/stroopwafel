using Microsoft.AspNetCore.Mvc;
using Ordering;
using Ordering.Commands;
using Ordering.Queries;
using Ordering.Services;
using Stroopwafels.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;

namespace Stroopwafels.Controllers
{
    public class StroopwafelController : Controller
    {
        private readonly QuotesQueryHandler _quotesQueryHandler;
        private readonly ICommandHandler<OrderCommand> _orderCommandHandler;

        public StroopwafelController(ICommandHandler<OrderCommand> orderCommandHandler, QuotesQueryHandler quotesQueryHandler)
        {
            _quotesQueryHandler = quotesQueryHandler;
            _orderCommandHandler = orderCommandHandler;
        }

        public ActionResult Index()
        {
            var viewModel = new OrderDetailsViewModel();

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult GetQuotes(OrderDetailsViewModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return Index();
            }

            var orderDetails = GetOrderDetails(formModel.OrderRows);
            var quotes = GetQuotesFor(orderDetails);

            var viewModel = new QuoteViewModel();
            foreach (var quote in quotes)
            {
                viewModel.Quotes.Add(new Models.Quote
                {
                    SupplierName = quote.Supplier.Name,
                    TotalAmount = quote.TotalPricePresentation
                });
            }

            viewModel.OrderRows = formModel.OrderRows;
            viewModel.SelectedSupplier = quotes.OrderBy(q => q.TotalPrice).First().Supplier.Name;

            return View(viewModel);
        }

        private IList<Ordering.Quote> GetQuotesFor(IList<KeyValuePair<StroopwafelType, int>> orderDetails)
        {
            var query = new QuotesQuery(orderDetails);
            var orders = _quotesQueryHandler.Handle(query);

            return orders;
        }

        private static IList<KeyValuePair<StroopwafelType, int>> GetOrderDetails(IEnumerable<OrderRow> orderRows)
        {
            return orderRows
                .Select(orderRow => new KeyValuePair<StroopwafelType, int>(orderRow.Type, orderRow.Amount))
                .ToList();
        }

        public ActionResult Order(QuoteViewModel formModel)
        {
            if (!ModelState.IsValid)
            {
                return Index();
            }

            var orderDetails = GetOrderDetails(formModel.OrderRows);

            var command = new OrderCommand(orderDetails, formModel.SelectedSupplier);
            _orderCommandHandler.Handle(command);

            return View();

        }
    }
}
