namespace SytePortfolio
{
    public partial class ImgBlog
    {
        public int IdImg { get; set; }
        public int IdBlog { get; set; }
        public int IdUser { get; set; }
        public byte[] DataImg { get; set; }
        public string TypeImg { get; set; }
    }
}
