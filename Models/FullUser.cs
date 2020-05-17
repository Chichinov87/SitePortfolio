using System.Collections.Generic;

namespace SytePortfolio
{
    public class FullUser
    {
        public IEnumerable<ImgUser> Image { get; set; }
        public IEnumerable<UserProfile> User { get; set; }
        public UserProfile OneUser { get; set; }
        public Education EducationUser { get; set; }
        public UserServices ServicesUser { get; set; }
        public UserWorkplace WorkplaceUser { get; set; }
        public UserBiography BiographyUser { get; set; }
        public ImgUser ImgUser { get; set; }
    }
}
