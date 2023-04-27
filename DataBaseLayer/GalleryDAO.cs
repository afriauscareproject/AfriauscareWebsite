using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
