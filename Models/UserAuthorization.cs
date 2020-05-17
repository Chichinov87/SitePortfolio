namespace SytePortfolio
{
    public partial class UserAuthorization
    {
        public int IdUser { get; set; }
        public int? StatusUser { get; set; }
        public string LoginUser { get; set; }
        public string HashPassword { get; set; }
        public string CodeWord { get; set; }
    }
}
