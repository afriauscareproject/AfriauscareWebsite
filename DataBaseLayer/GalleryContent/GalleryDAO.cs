using System;
using System.Collections.Generic;
using System.Linq;
using Afriauscare.BusinessLayer.Gallery;

namespace Afriauscare.DataBaseLayer
{
    public class GalleryDAO
    {
        public List<GalleryModel> getGalleryAll()
        {
            List<GalleryModel> listReturn = new List<GalleryModel>();

            using (var DataBase = new AfriAusEntities())
            {
                var galleryList = DataBase.Galleries.ToList();

                foreach (var item in galleryList)
                {
                    GalleryModel objGallery = new GalleryModel
                    {
                        GalleryId = item.GalleryId,
                        GalleryTitle = item.GalleryTitle,
                        GalleryDescription = item.GalleryDescription
                    };
                    listReturn.Add(objGallery);
                }
            }

            return listReturn;
        }

        public int CreateGallery(GalleryModel objModel)
        {
            var galleryId = 0;

            using (var DataBase = new AfriAusEntities())
            {
                Gallery objGallery = new Gallery()
                {
                    GalleryTitle = objModel.GalleryTitle,
                    GalleryDescription = objModel.GalleryDescription,
                    GalleryEventDate = objModel.GalleryEventDate
                };

                DataBase.Galleries.Add(objGallery);
                DataBase.SaveChanges();
                galleryId = objGallery.GalleryId;
            }

            return galleryId;
        }

    }
}
