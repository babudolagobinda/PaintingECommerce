using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.AdminDTO
{
    public class ItemDetailsDto
    {
        public int ItemDetailsId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public decimal Width { get; set; }
        public string WidthType { get; set; }
        public decimal Height { get; set; }
        public string HeightType { get; set; }
        public decimal Mrp { get; set; }
        public decimal Discount { get; set; }
        public decimal Price { get; set; }
        public string CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int IsActive { get; set; }
    }
}
