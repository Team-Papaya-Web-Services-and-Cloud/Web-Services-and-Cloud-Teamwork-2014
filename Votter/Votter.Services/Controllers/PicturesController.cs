﻿using System;
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
        private static Random random = new Random();
        private VotterData data;

        public PicturesController()
        {
            data = new VotterData();
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
            int randomCategory = GetRandomCategoryId();


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
            PictureModel picture = data.Pictures.All()
                .Where(x=>x.PictureId == id)
                .Select(x => 
                    new PictureModel()
                                { 
                                    Id = x.PictureId, 
                                    Link = x.Link, 
                                    CategoryId = x.CategoryId,
                                    ApplicationUserId = x.ApplicationUserId.ToString()
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
        public IHttpActionResult PostPicture(PictureModel p)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            this.data.Pictures
                     .Add(new Picture()
                     {
                         CategoryId = p.CategoryId,
                         ApplicationUserId = p.ApplicationUserId,
                         Link = p.Link
                     });
            
            this.data.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = p.Id }, p);
        }

        [HttpDelete]
        public IHttpActionResult DeletePicture(int id)
        {
            Picture picture = this.data.Pictures.Find(id);
            if (picture == null)
            {
                return NotFound();
            }

            this.data.Pictures.Delete(picture);
            this.data.SaveChanges();

            return Ok(string.Format("Picture {0} DELETED", id));
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