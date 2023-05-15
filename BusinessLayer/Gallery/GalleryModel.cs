using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web;

namespace Afriauscare.BusinessLayer.Gallery
{
    public class GalleryModel
    {
        public int GalleryId { set; get; }

        [DisplayName("Gallery Title")]
        public string GalleryTitle { set; get; }

        [DisplayName("Gallery Description")]
        public string GalleryDescription { set; get; }

        [DisplayName("Event Date")]
        public DateTime GalleryEventDate { set; get; }

        [DisplayName("Select your files")]
        public HttpPostedFileBase[] imageList { set; get; }
    }
}
