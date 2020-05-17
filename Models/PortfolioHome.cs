using System;

namespace SytePortfolio
{
    public class PortfolioHome
    {
        public int IdPortfolio { get; set; }
        public string NamePortfolio { get; set; }
        public string TypePortfolio { get; set; }
        public string DescriptionPortfolio { get; set; }
        public DateTime? DatePortfolio { get; set; }
        public int IdUserPortfolio { get; set; }
        public string NameUserPortfolio { get; set; }
        public byte[] ImagePortfolio { get; set; }
    }
}
