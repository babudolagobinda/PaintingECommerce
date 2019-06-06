using System;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ArtGalleryECommerce.UI.Models
{
    public class ItemGroupModel
    {
        public int GroupId { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Group Name is Mandatory"), MaxLength(40)]
        public string GroupName { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Group Desc is Mandatory"), MaxLength(400)]
        public string GroupDesc { get; set; }
        [DataType(DataType.Text)]
        [Required(ErrorMessage = "Group Image is Mandatory"), MaxLength(500)] 
        public string GroupImage { get; set; }
        public HttpPostedFile ImageFile { get; set; }
        public DateTimeOffset CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
        public int ModifiedBy { get; set; }
        public int IsActive { get; set; }
    }
}