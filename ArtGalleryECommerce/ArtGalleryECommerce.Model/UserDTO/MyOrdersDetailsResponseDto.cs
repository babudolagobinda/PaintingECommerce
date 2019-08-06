using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.UserDTO
{
    public class MyOrdersDetailsResponseDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public decimal Mrp { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public decimal Width { get; set; }
        public string WidthType { get; set; }
        public decimal Height { get; set; }
        public string HeightType { get; set; }
        public string CurrencyType { get; set; }
        public int Quantity { get; set; }
        public int Status { get; set; }
        public int OrderId { get; set; }
        public string OrderNumber { get; set; }
        public string OrderDate { get; set; }
        public string PaymentDate { get; set; }
        public string PaymentType { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string EmailId { get; set; }
        public string MobileNo { get; set; }
        public int AddressId { get; set; }
        public string UserName { get; set; }
        public string UserMobileNo { get; set; }
        public string UserPincode { get; set; }
        public string UserAddress { get; set; }
        public string UserLocality { get; set; }
        public string UserCity { get; set; }
        public string UserState { get; set; }
        public string UserCountry { get; set; }
    }
}
