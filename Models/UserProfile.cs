using System;

namespace SytePortfolio
{
    public partial class UserProfile
    {
        public int IdUser { get; set; }
        public string NameUser { get; set; }
        public string SurnameUser { get; set; }
        public string MiddlenameUser { get; set; }
        public DateTime? AgeUser { get; set; }
        public string AdressUser { get; set; }
        public string AmailUser { get; set; }
        public string PhoneUser { get; set; }
        public int? MainImg { get; set; }
    }
}
