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
        VotterData data;

        public PicturesController()
        {
            data = new VotterData();
        }

        public IQueryable<PictureModel> GetPictures()
        {
            return this.data.Pictures
                            .All()
                            .Select(x => new PictureModel() { Id = x.PictureId, Link = x.Link, CategoryId = x.CategoryId });
        }

        [HttpGet]
        [ResponseType(typeof(PictureModel))]
        public IHttpActionResult GetPicture(int id)
        {
            PictureModel picture = data.Pictures.All()
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
        [ResponseType(typeof(Picture))]
        public IHttpActionResult PostPicture(Picture picture)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.data.Pictures.Add(picture);
            this.data.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = picture.PictureId }, picture);
        }

        // DELETE: api/Pictures/5
        [ResponseType(typeof(Picture))]
        public IHttpActionResult DeletePicture(int id)
        {
            Picture picture = this.data.Pictures.Find(id);
            if (picture == null)
            {
                return NotFound();
            }

            this.data.Pictures.Delete(picture);
            this.data.SaveChanges();

            return Ok(picture);
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
    }
}