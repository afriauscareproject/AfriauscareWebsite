using System;
using System.Collections.Generic;
using System.Linq;
using Afriauscare.BusinessLayer.Gallery;

namespace Afriauscare.DataBaseLayer
{
    public class GalleryDAO
    {
        /// <summary>
        /// Method that returns all the records from the Gallery table.
        /// </summary>
        /// <returns>List<GalleryModel></returns>
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

        /// <summary>
        /// Method that creates a gallery record in the database and returns the gallery ID.
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns>Gallery ID as integer</returns>
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

        /// <summary>
        /// Method that obtains the gallery record based on its ID. Note that this method is used for Modify Gallery only as it uses its own model
        /// </summary>
        /// <param name="GalleryId"></param>
        /// <returns>GalleryModifyModel</returns>
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

        /// <summary>
        /// Method that updates a Gallery record.
        /// </summary>
        /// <param name="objGalleryModel"></param>
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

        /// <summary>
        /// Method that deletes a gallery record.
        /// </summary>
        /// <param name="galleryId"></param>
        public void DeleteGallery(int galleryId)
        {
            using (var DataBase = new AfriAusEntities())
            {
                Gallery recordToDelete = new Gallery() { GalleryId = galleryId };
                DataBase.Entry(recordToDelete).State = System.Data.EntityState.Deleted;
                DataBase.SaveChanges();
            }
        }
    }
}
