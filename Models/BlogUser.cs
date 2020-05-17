using System;

namespace SytePortfolio
{
    public partial class BlogUser
    {
        public int IdBlog { get; set; }
        public int IdUser { get; set; }
        public string NameBlog { get; set; }
        public string TextBlog { get; set; }
        public string TypeBlog { get; set; }
        public string SubtypeBlog { get; set; }
        public DateTime?  DateBlog { get; set; }
        public int? MainImg { get; set; }
    }
}
