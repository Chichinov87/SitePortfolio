using System.Collections.Generic;

namespace SytePortfolio
{
    public class FullServices
    {
        public IEnumerable<UserServices> Services { get; set; }
        public IEnumerable<UserProfile> User { get; set; }
    }
}
