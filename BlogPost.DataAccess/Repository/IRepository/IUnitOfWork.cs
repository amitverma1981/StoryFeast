using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IStoryRepository Story { get; set; }
        IStoryClapRepository StoryClap { get; set; }
        ISP_Call SP_Call { get; }
        void Save();
    }
}
