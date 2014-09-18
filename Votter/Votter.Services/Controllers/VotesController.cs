namespace Votter.Services.Controllers
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Linq;
    using System.Net;
    using System.Web.Http;
    using System.Web.Http.Description;
    using Votter.Data;
    using Votter.Models;

    public class VotesController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Votes
        public IQueryable<Vote> GetVotes()
        {
            return this.db.Votes;
        }

        // GET: api/Votes/5
        [ResponseType(typeof(Vote))]
        public IHttpActionResult GetVote(int id)
        {
            Vote vote = this.db.Votes.Find(id);
            if (vote == null)
            {
                return this.NotFound();
            }

            return this.Ok(vote);
        }

        // PUT: api/Votes/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVote(int id, Vote vote)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (id != vote.VoteId)
            {
                return this.BadRequest();
            }

            this.db.Entry(vote).State = EntityState.Modified;

            try
            {
                this.db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!this.VoteExists(id))
                {
                    return this.NotFound();
                }
                else
                {
                    throw;
                }
            }

            return this.StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Votes
        [ResponseType(typeof(Vote))]
        public IHttpActionResult PostVote(Vote vote)
        {
            if (!this.ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            this.db.Votes.Add(vote);
            this.db.SaveChanges();

            return this.CreatedAtRoute("DefaultApi", new { id = vote.VoteId }, vote);
        }

        // DELETE: api/Votes/5
        [ResponseType(typeof(Vote))]
        public IHttpActionResult DeleteVote(int id)
        {
            Vote vote = this.db.Votes.Find(id);
            if (vote == null)
            {
                return this.NotFound();
            }

            this.db.Votes.Remove(vote);
            this.db.SaveChanges();

            return this.Ok(vote);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoteExists(int id)
        {
            return this.db.Votes.Count(e => e.VoteId == id) > 0;
        }
    }
}