using BlogPost.DataAccess.Data;
using BlogPost.DataAccess.Repository.IRepository;
using BlogPost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogPost.DataAccess.Repository
{
    public class StoryRepository : Repository<Story>, IStoryRepository
    {
        private readonly ApplicationDbContext _db;

        public StoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Story story)
        {
            var objFromDb = _db.Stories.FirstOrDefault(s => s.Id == story.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = story.Title;
                objFromDb.SubTitle = story.SubTitle;
                objFromDb.UpdatedDate = DateTime.Now;
            }
        }
    }
}
