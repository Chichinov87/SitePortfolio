using System;

namespace SytePortfolio
{
    public class BlogHome
    {
        public int IdBlog { get; set; }
        public string NameBlog { get; set; }
        public string TypeBlog { get; set; }
        public string DescriptionBlog { get; set; }
        public DateTime? DateBlog { get; set; }
        public int IdUserBlog { get; set; }
        public string NameUserBlog { get; set; }
        public byte[] ImageBlog { get; set; }
    }
}
