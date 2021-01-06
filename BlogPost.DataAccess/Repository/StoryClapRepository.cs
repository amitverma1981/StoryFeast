using BlogPost.DataAccess.Data;
using BlogPost.DataAccess.Repository.IRepository;
using BlogPost.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BlogPost.DataAccess.Repository
{
    public class StoryClapRepository : Repository<StoryClap>, IStoryClapRepository
    {
        private readonly ApplicationDbContext _db;

        public StoryClapRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(StoryClap storyclap)
        {
            var objFromDb = _db.StoryClaps.FirstOrDefault(s => s.Id == storyclap.Id);
            if (objFromDb != null)
            {
                objFromDb.UpdatedDate = DateTime.Now;
            }
        }
    }
}
