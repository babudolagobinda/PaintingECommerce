using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.AdminDTO
{
    public class ItemCategoryDto
    {
        public int CategoryId { get; set; }
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDesc { get; set; }
        public string CategoryImage { get; set; }
        public string CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int IsActive { get; set; }
    }
}
