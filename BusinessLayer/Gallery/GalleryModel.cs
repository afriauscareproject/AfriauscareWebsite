using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Afriauscare.BusinessLayer.Shared;

namespace Afriauscare.BusinessLayer.Gallery
{
    /// <summary>
    /// Gallery model class which is used for gallery content with main information
    /// </summary>
    public class GalleryModel
    {
        public int GalleryId { set; get; }

        [DisplayName("Gallery Title")]
        [Required(ErrorMessage = "Gallery Title is required.")]
        [StringLength(50, ErrorMessage = "The maximum lenght for Gallery Title is 50 characters.")]
        public string GalleryTitle { set; get; }

        [DisplayName("Gallery Description")]
        [Required(ErrorMessage = "Gallery Description is required.")]
        [StringLength(100, ErrorMessage = "The maximum lenght for First Name is 100 characters.")]
        public string GalleryDescription { set; get; }

        [DisplayName("Event Date")]
        [Required(ErrorMessage = "Gallery Event Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime GalleryEventDate { set; get; }

        [DisplayName("Select your files")]
        [ImageListValidation(ErrorMessage = "Images are required.")]
        public HttpPostedFileBase[] imageList { set; get; }

        public byte[] DefaultImage { set; get; }
    }
}
