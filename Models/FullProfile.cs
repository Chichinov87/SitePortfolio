using System.Collections.Generic;

namespace SytePortfolio
{
    public class FullProfile
    {
        public IEnumerable<FullProfile>User { get; set; }

        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string SurnameUser { get; set; }
        public string CityUser { get; set; }
        public int? MainImg { get; set; }
        public int IdImg { get; set; }
        public byte[] ImgUser { get; set; }
    }
}
