using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BlogPost.Models
{
    public class Story
    {
        [Key]
        public int Id{ get; set; }
        
        [Required]
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string TitleImage { get; set; }
        public string StoryDetail { get; set; }
        public bool IsPublished { get; set; }
       
        ////master to be created navigational property
        //public string Category { get; set; }
        public string Tages { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public DateTime UpdatedDate { get; set; }
    }
}
