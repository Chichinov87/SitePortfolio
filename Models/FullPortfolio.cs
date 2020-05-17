using System.Collections.Generic;

namespace SytePortfolio
{
    public class FullPortfolio
    {
        public IEnumerable<ImgPortfolio> Image { get; set; }
        public IEnumerable<UserPortfolio> User { get; set; }
        public UserPortfolio OnePortfolio { get; set; }
        public UserProfile OneProfile { get; set; }
    }
}
