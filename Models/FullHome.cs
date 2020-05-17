using System.Collections.Generic;

namespace SytePortfolio
{
    public class FullHome
    {
        public IEnumerable<PortfolioHome> Portfolio { get; set; }
        public IEnumerable<BlogHome> Blog { get; set; }
    }
}
