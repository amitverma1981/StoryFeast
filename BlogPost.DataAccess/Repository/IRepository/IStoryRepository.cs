using BlogPost.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.DataAccess.Repository.IRepository
{
    public interface IStoryRepository : IRepository<Story>
    {
        void Update(Story story);
    }
}
