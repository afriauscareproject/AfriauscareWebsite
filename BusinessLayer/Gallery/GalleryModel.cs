using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web;
using Afriauscare.BusinessLayer.Shared;

namespace Afriauscare.BusinessLayer.Gallery
{
    public class GalleryModel
    {
        public int GalleryId { set; get; }

        [DisplayName("Gallery Title")]
        [Required(ErrorMessage = "Gallery Title is required.")]
        public string GalleryTitle { set; get; }

        [DisplayName("Gallery Description")]
        [Required(ErrorMessage = "Gallery Description is required.")]
        public string GalleryDescription { set; get; }

        [DisplayName("Event Date")]
        [Required(ErrorMessage = "Gallery Event Date is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}", ApplyFormatInEditMode = true)]
        public DateTime GalleryEventDate { set; get; }

        [DisplayName("Select your files")]
        [ImageListValidation(ErrorMessage = "Images are required.")]
        public HttpPostedFileBase[] imageList { set; get; }

        public byte[] DefaultImage { set; get; }
    }
}
