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

        public GalleryModifyModel GetGalleryById(int GalleryId)
        {
            using (var DataBase = new AfriAusEntities())
            {
                var objGallery = DataBase.Galleries.Where(g => g.GalleryId == GalleryId).SingleOrDefault();

                GalleryModifyModel objGalleryReturn = new GalleryModifyModel
                {
                    GalleryId = objGallery.GalleryId,
                    GalleryTitle = objGallery.GalleryTitle,
                    GalleryDescription = objGallery.GalleryDescription,
                    GalleryEventDate = objGallery.GalleryEventDate
                };

                return objGalleryReturn;
            }
        
}

        public void ModifyGallery(GalleryModifyModel objGalleryModel)
        {
            using (var DataBase = new AfriAusEntities())
            {
                Gallery objGalleryUpdate = new Gallery()
                {
                    GalleryId = objGalleryModel.GalleryId,
                    GalleryTitle = objGalleryModel.GalleryTitle,
                    GalleryDescription = objGalleryModel.GalleryDescription,
                    GalleryEventDate = objGalleryModel.GalleryEventDate
                };

                DataBase.Galleries.Add(objGalleryUpdate);
                DataBase.Entry(objGalleryUpdate).State = System.Data.EntityState.Modified;
                DataBase.SaveChanges();
            }
        }

    }
}
