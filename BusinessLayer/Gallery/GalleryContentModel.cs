using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afriauscare.BusinessLayer.Gallery
{
    public class GalleryContentModel
    {
        public int GalleryContentId { set; get; }

        public string GalleryContentPath { set; get; }

        public int GalleryContentIndex { set; get; }

        public bool GalleryContentIsActive { set; get; }

        public byte[] GalleryContentImage { set; get; }

        public string GalleryName { set; get; }

        public string GalleryFileName { set; get; }

        public int GalleryId { set; get; }

    }
}
