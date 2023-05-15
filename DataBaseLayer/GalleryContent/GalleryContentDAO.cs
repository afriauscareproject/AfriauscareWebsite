using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afriauscare.BusinessLayer.Gallery;

namespace Afriauscare.DataBaseLayer
{
    public class GalleryContentDAO
    {
        public void CreateGalleryContent(GalleryContentModel objModel, int galleryId)
        {
            using(var Database = new AfriAusEntities())
            {
                GalleryContent objGallery = new GalleryContent()
                {
                    GalleryId = galleryId,
                    GalleryContentPath = objModel.GalleryContentPath,
                    GalleryContentIndex = objModel.GalleryContentIndex,
                    GalleryContentIsActive = objModel.GalleryContentIsActive,
                    GalleryContentImage = objModel.GalleryContentImage
                };
                Database.GalleryContents.Add(objGallery);
                Database.SaveChanges();
            }
        }
    }
}
