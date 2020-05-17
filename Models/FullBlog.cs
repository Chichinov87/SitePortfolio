using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SytePortfolio
{
    public class FullBlog
    {
        public IEnumerable<ImgBlog> Image { get; set; }
        public IEnumerable<BlogUser> Blog { get; set; }
        public BlogUser OneBlog { get; set; }
    }
}
