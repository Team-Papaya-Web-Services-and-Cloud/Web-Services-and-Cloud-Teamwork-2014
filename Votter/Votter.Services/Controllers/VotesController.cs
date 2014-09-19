namespace Votter.Services.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Http;
    using Votter.Models;

    [Authorize]
    public class VotesController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Create(int upVotePictureId, int downVotePictureId)
        {
            Vote vote = null;

            // TODO Validation
            // Authorized

            return Ok(vote);
        }
    }
}