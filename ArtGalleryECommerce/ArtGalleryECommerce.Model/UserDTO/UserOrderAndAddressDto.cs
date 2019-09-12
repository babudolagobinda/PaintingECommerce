using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.UserDTO
{
    public class UserOrderAndAddressDto
    {
        public int AddressId { get; set; }
        public string OrderNumber { get; set; }
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentType { get; set; }
        public int Status { get; set; }
        public string Name { get; set; }
        public string MobileNo { get; set; }
        public string Pincode { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public int IsActive { get; set; }
    }
}
