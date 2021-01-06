using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.Models.ViewModels
{
    public class DashBoardViewModel
    {
        public int StoryId { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string StoryFirstImage { get; set; }
        public string StoryFirstPara { get; set; }

        public string CreatedBy { get; set; }

        public string CreatedOn { get; set; }

        public string UserImage { get; set; }

        public string UpdatedOn { get; set; }


    }
}
