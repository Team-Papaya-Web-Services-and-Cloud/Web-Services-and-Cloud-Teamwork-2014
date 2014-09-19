using System;

namespace Votter.Services.Controllers
{
    using System.Linq;
    using System.Web.Http;
    using Votter.Data;
    using Votter.Models;
    using Votter.Services.Models;

    public class PicturesController : ApiController
    {
        private static Random random = new Random();
        private VotterData data;

        public PicturesController()
        {
            this.data = new VotterData();
        }

        public IQueryable<PictureModel> GetPictures()
        {
            return this.data.Pictures
                       .All()
                       .Select(x => new PictureModel()
                              {
                                  Id = x.PictureId,
                                  Link = x.Link,
                                  CategoryId = x.CategoryId,
                                  ApplicationUserId = x.ApplicationUserId.ToString()
                              });
        }

        public IQueryable<PictureModel> GetRandomPictureFromRandomCategory()
        {
            int randomCategory = this.GetRandomCategoryId();

            return this.data.Pictures
                       .All()
                       .Where(p => p.CategoryId == randomCategory)
                       .Select(x => new PictureModel()
                              {
                                  Id = x.PictureId,
                                  Link = x.Link,
                                  CategoryId = x.CategoryId,
                                  ApplicationUserId = x.ApplicationUserId
                              })
                       .Take(2);
        }

        [HttpGet]
        public IQueryable<PictureModel> GetTwoRandomPicsFromCategory(int categoryID)
        {
            return this.data.Pictures
                       .All()
                       .Where(p => p.CategoryId == categoryID)
                       .Select(x => new PictureModel()
                              {
                                  Id = x.PictureId,
                                  Link = x.Link,
                                  CategoryId = x.CategoryId,
                                  ApplicationUserId = x.ApplicationUserId
                              })
                       .Take(2);
        }

        [HttpGet]
        public IHttpActionResult GetPicture(int id)
        {
            PictureModel picture = this.data.Pictures.All()
                                       .Where(x => x.PictureId == id)
            .Select(x => new PictureModel(){ Id = x.PictureId, Link = x.Link, CategoryId = x.CategoryId, ApplicationUserId = x.ApplicationUserId.ToString() })
                                       .FirstOrDefault();

            if (picture == null)
            {
                return this.NotFound();
            }

            return this.Ok(picture);
        }

        // PUT: api/Pictures/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutPicture(PictureModel picture)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    } 
        //    this.data.Pictures.Entry(picture).State = EntityState.Modified;

        //    try
        //    {
        //        this.data.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!PictureExists(picture.Id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw new ArgumentException("The picture Id wasn't valid");
        //        }
        //    }
        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        [HttpPost]
        public IHttpActionResult PostPicture(PictureModel p)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            this.data.Pictures
            .Add(new Picture()
                {
                    CategoryId = p.CategoryId,
                    ApplicationUserId = p.ApplicationUserId,
                    Link = p.Link
                });
            
            this.data.SaveChanges();
            return this.CreatedAtRoute("DefaultApi", new { id = p.Id }, p);
        }

        [HttpDelete]
        public IHttpActionResult DeletePicture(int id)
        {
            Picture picture = this.data.Pictures.Find(id);
            if (picture == null)
            {
                return this.NotFound();
            }

            this.data.Pictures.Delete(picture);
            this.data.SaveChanges();

            return this.Ok(string.Format("Picture {0} DELETED", id));
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.data.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PictureExists(int id)
        {
            return this.data.Pictures.All().Count(e => e.PictureId == id) > 0;
        }

        private int GetRandomCategoryId()
        {
            var categoryIds = this.data.Categories.All().Select(c => c.CategoryId);
            var skip = (int)(random.NextDouble() * categoryIds.Count());
            return this.data.Categories.All().OrderBy(c => c.CategoryId).Skip(skip).Take(1).First().CategoryId; 
        }
    }
}