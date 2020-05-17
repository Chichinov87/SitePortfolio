namespace SytePortfolio
{
    public partial class ImgUser
    {
        public int IdImg { get; set; }
        public int? IdUser { get; set; }
        public byte[] DataImg { get; set; }
        public string TypeImg { get; set; }
    }
}
