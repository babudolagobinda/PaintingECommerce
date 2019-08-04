using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArtGalleryECommerce.Model.UserDTO
{
    public class UserReviewDto
    {
        public int RatingId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public int ItemId { get; set; }
        public string Review { get; set; }
        public string Rating { get; set; }
        public string CreatedDate { get; set; }
        public int IsActive { get; set; }
    }
}
