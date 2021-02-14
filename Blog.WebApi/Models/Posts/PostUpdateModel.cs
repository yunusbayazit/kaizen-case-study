using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blog.WebApi.Models.Posts
{
    public class PostUpdateModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Content { get; set; }
    }
}
