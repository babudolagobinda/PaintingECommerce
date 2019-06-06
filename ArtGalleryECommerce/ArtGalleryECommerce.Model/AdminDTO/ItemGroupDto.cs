using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.AdminDTO
{
    public class ItemGroupDto
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public string GroupDesc { get; set; }
        public string GroupImage { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int IsActive { get; set; }

    }
}
