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

        public List<GalleryContentModel> getImagesAll()
        {
            List<GalleryContentModel> listGallery = new List<GalleryContentModel>();

            using (var Database = new AfriAusEntities())
            {
                var list = Database.GalleryContents.Where(g => g.GalleryContentIsActive == true).ToList();

                foreach (var item in list)
                {
                    GalleryContentModel objGalleryContent = new GalleryContentModel()
                    {
                        GalleryContentIndex = item.GalleryContentIndex,
                        GalleryContentImage = item.GalleryContentImage
                    };
                    listGallery.Add(objGalleryContent);
                }
            }

            return listGallery;
        }

        public GalleryContentModel getFirstImageFromGallery(int galleryId)
        {
            using (var Database = new AfriAusEntities())
            {
                var list = Database.GalleryContents.Where(g => g.GalleryContentIsActive == true && g.GalleryId == galleryId && g.GalleryContentIndex == 1).FirstOrDefault();

                GalleryContentModel objGalleryContent = new GalleryContentModel()
                {
                    GalleryContentImage = list.GalleryContentImage
                };

                return objGalleryContent;
            }

        }
    }
}
