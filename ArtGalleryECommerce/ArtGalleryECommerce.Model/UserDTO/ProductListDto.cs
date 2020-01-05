using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.UserDTO
{
    public class ProductListDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDetails { get; set; }
        public string ItemDesc { get; set; }
        public string ItemImage { get; set; }
        public decimal Width { get; set; }
        public string WidthType { get; set; }
        public decimal Height { get; set; }
        public string HeightType { get; set; }
        public decimal Mrp { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
    public class PagingInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }
    }
    public class ProductListViewModel
    {
        public IEnumerable<ProductListDto> lstProductListDto { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
}
