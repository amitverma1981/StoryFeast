using BlogPost.DataAccess.Data;
using BlogPost.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Story = new StoryRepository(_db);
            StoryClap = new StoryClapRepository(_db);
            SP_Call = new SP_Call(_db);
        }

        public IStoryRepository Story { get; set; }

        public IStoryClapRepository StoryClap { get; set; }
        public ISP_Call SP_Call { get; private set; }
       
        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
