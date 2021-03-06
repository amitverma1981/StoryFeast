﻿using BlogPost.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlogPost.DataAccess.Repository.IRepository
{
    public interface IStoryClapRepository : IRepository<StoryClap>
    {
        void Update(StoryClap storyclap);
    }
}
