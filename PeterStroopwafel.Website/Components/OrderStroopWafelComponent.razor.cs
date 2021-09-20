using System;
using System.Linq;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Ordering.Commands;
using Ordering.Queries;

namespace PeterStroopwafel.Website.Components {
    public partial class OrderStroopWafelComponent {

        [Inject] public CustomerQuotesQueryHandler CustomerQuotesQueryHandler { get; set; }
        [Inject] public ICommandHandler<CustomerOrderCommand> CustomerOrderCommandHandler { get; set; }
        

        public OrderViewModel OrderViewModel { get; set; }


        public bool ShowOrderForm => ShowOrderSuccess == false;
        public bool ShowOrderSuccess { get; set; }


        public void OnOrder() {
            CustomerOrderCommandHandler.Handle(OrderViewModel.GetCustomerOrderCommand());
            ShowOrderSuccess = true;
        }
        
        protected override void OnInitialized() {

            OrderViewModel = new OrderViewModel(CustomerQuotesQueryHandler);
            base.OnInitialized();
        }
    }
}