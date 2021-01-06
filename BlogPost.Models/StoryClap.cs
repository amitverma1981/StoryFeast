using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogPost.Models
{
    public class StoryClap
    {
        [Key]
        public int Id { get; set; }

        //[Required]
        //public string ApplicationUserId { get; set; }
        //[ForeignKey("ApplicationUserId")]
        //public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int StoryId { get; set; }
        [ForeignKey("StoryId")]
        public Story Story { get; set; }

        public int Claps { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }
}
