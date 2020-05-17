using Microsoft.AspNetCore.Http;
using System;

namespace SytePortfolio
{
    public partial class UserPortfolioForms
{
        public int IdPortfolio { get; set; }
    public int IdUser { get; set; }
    public string TypePortfolio { get; set; }
    public string NamePortfolio { get; set; }
    public string DescriptionPortfolio { get; set; }
    public string StatusPortfolio { get; set; }
    public decimal? PricePortfolio { get; set; }
    public IFormFile ImagePortfolio { get; set; }
    public string ImageMimeType { get; set; }
    public string LinkPortfolio { get; set; }
    public DateTime? DatePortfolio { get; set; }
}
}
