namespace SytePortfolio
{
    public partial class ImgPortfolio
    {
        public int IdImg { get; set; }
        public int IdPortfolio { get; set; }
        public int IdUser { get; set; }
        public byte[] DataImg { get; set; }
        public string TypeImg { get; set; }
    }
}
