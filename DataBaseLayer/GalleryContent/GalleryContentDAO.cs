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
                    GalleryContentImage = objModel.GalleryContentImage,
                    GalleryContentImageName = objModel.GalleryFileName
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

        public List<GalleryContentModel> getImagesFromGallery(int galleryId)
        {
            List<GalleryContentModel> listGallery = new List<GalleryContentModel>();

            using (var Database = new AfriAusEntities())
            {
                var list = Database.GalleryContents.Where(g => g.GalleryContentIsActive == true && g.GalleryId == galleryId).ToList();
                var galleryTitle = Database.Galleries.Where(g => g.GalleryId == galleryId).FirstOrDefault();

                foreach (var item in list)
                {
                    GalleryContentModel objGalleryContent = new GalleryContentModel()
                    {
                        GalleryContentIndex = item.GalleryContentIndex,
                        GalleryContentImage = item.GalleryContentImage,
                        GalleryContentPath = galleryTitle.GalleryTitle
                    };
                    listGallery.Add(objGalleryContent);
                }

                return listGallery;
            }

        }

        public List<GalleryContentModel> getImagesFromGalleryAll(int galleryId)
        {
            List<GalleryContentModel> listGallery = new List<GalleryContentModel>();

            using (var Database = new AfriAusEntities())
            {
                var list = Database.GalleryContents.Where(g => g.GalleryContentIsActive == true && g.GalleryId == galleryId).ToList();
                var galleryTitle = Database.Galleries.Where(g => g.GalleryId == galleryId).FirstOrDefault();

                foreach (var item in list)
                {
                    GalleryContentModel objGalleryContent = new GalleryContentModel()
                    {
                        GalleryId = (int)item.GalleryId,
                        GalleryContentId = item.GalleryContentID,
                        GalleryName = galleryTitle.GalleryTitle,
                        GalleryContentIndex = item.GalleryContentIndex,
                        GalleryContentImage = item.GalleryContentImage,
                        GalleryFileName = item.GalleryContentImageName
                    };
                    listGallery.Add(objGalleryContent);
                }

                return listGallery;
            }

        }

        public void DeleteGalleryContent(int galleryContentId)
        {
            using (var DataBase = new AfriAusEntities())
            {
                if (DataBase.GalleryContents.Any(c => c.GalleryContentID == galleryContentId))
                {
                    GalleryContent recordToDelete = new GalleryContent() { GalleryContentID = galleryContentId };
                    DataBase.Entry(recordToDelete).State = System.Data.EntityState.Deleted;
                    DataBase.SaveChanges();
                }
            }
        }

        public void AssignImagesIndexes(int galleryId)
        {
            using (var Database = new AfriAusEntities())
            {
                var list = Database.GalleryContents.Where(g => g.GalleryId == galleryId).ToList();
                int index = 1;

                foreach (var item in list)
                {
                    item.GalleryContentIndex = index;
                    index = index + 1;
                }
                Database.SaveChanges();
            }
        }
    }
}
