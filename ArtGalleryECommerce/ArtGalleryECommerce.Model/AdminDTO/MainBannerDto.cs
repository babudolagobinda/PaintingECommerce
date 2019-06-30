using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.AdminDTO
{
    public class MainBannerDto
    {
        public int BannerId { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public string BannerName { get; set; }
        public string BannerDesc { get; set; }
        public string BannerImage { get; set; }
        public string CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int IsActive { get; set; }
    }
}
