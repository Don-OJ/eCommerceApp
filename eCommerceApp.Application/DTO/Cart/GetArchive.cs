﻿namespace eCommerceApp.Application.DTO.Cart
{
    public class GetArchive
    {
        public string? ProductName { get; set; }
        public int QuantityOrdered { get; set; }
        public string? CustomerName { get; set; }
        public string? CustomerEmail { get; set; }
        public decimal AmountPaid { get; set; }
        public DateTime DatePurchased { get; set; }
    }
}
