namespace Votter.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    public class Vote
    {
        [Key]
        public int VoteId { get; set; }

        public Guid ApplicationUserId { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
 
        public int UpVotePictureId { get; set; }

        public int DownVotePictureId { get; set; }
    }
}