using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afriauscare.BusinessLayer.Gallery
{
    public class GalleryModel
    {
        public int GalleryId { set; get; }

        [DisplayName("Gallery Title")]
        public string GalleryTitle { set; get; }

        [DisplayName("Gallery Description")]
        public string GalleryDescription { set; get; }
    }
}
