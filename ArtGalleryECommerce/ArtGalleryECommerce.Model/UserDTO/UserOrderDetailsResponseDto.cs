﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.UserDTO
{
    public class UserOrderDetailsResponseDto
    {
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public int AddressId { get; set; }
        public decimal Mrp { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public string CurrencyType { get; set; }
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentType { get; set; }
        public string OrderDate { get; set; }
        public string PaymentDate { get; set; }
        public int Status { get; set; }
    }
}