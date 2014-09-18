using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Votter.Data;
using Votter.Models;
using Votter.Data.Repositories;
using Votter.Data.Contracts;
using Votter.Services.Models;

namespace Votter.Services.Controllers
{
    public class PicturesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        IGenericRepository<Picture> data;

        public PicturesController()
        {
            data = new GenericRepository<Picture>();
        }

        // GET: api/Pictures
        public IQueryable<PictureModel> GetPictures()
        {
            return db.Pictures.Select(x => new PictureModel() { Id = x.PictureId, Link = x.Link, CategoryId = x.CategoryId });
        }

        // GET: api/Pictures/5
        [HttpGet]
        [ResponseType(typeof(PictureModel))]
        public IHttpActionResult GetPicture(int id)
        {
            PictureModel picture = db.Pictures
                .Where(x=>x.PictureId == id)
                .Select(x => 
                    new PictureModel()
                                { 
                                    Id = x.PictureId, Link = x.Link, CategoryId = x.CategoryId 
                                })
                .FirstOrDefault();

            if (picture == null)
            {
                return NotFound();
            }

            return Ok(picture);
        }

        // PUT: api/Pictures/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPicture(PictureModel picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Entry(picture).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PictureExists(picture.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw new ArgumentException("The picture Id wasn't valid");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }



        // POST: api/Pictures
        [ResponseType(typeof(Picture))]
        public IHttpActionResult PostPicture(Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Pictures.Add(picture);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = picture.PictureId }, picture);
        }

        // DELETE: api/Pictures/5
        [ResponseType(typeof(Picture))]
        public IHttpActionResult DeletePicture(int id)
        {
            Picture picture = db.Pictures.Find(id);
            if (picture == null)
            {
                return NotFound();
            }

            db.Pictures.Remove(picture);
            db.SaveChanges();

            return Ok(picture);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PictureExists(int id)
        {
            return db.Pictures.Count(e => e.PictureId == id) > 0;
        }
    }
}