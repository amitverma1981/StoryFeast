using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Models.ViewModels
{
    public class StoryViewModel
    {
        public int StoryId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string htmlStory { get; set; }
        public string FileName { get; set; }
    
    }
}
