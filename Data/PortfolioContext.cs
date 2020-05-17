using Microsoft.EntityFrameworkCore;

namespace SytePortfolio
{
    public partial class PortfolioContext : DbContext
    {
        public PortfolioContext()
        {
        }

        public PortfolioContext(DbContextOptions<PortfolioContext> options)
            : base(options)
        {
        }

        public virtual DbSet<BlogUser> BlogUser { get; set; }
        public virtual DbSet<Education> Education { get; set; }
        public virtual DbSet<ImgBlog> ImgBlog { get; set; }
        public virtual DbSet<ImgPortfolio> ImgPortfolio { get; set; }
        public virtual DbSet<ImgUser> ImgUser { get; set; }
        public virtual DbSet<UserAuthorization> UserAuthorization { get; set; }
        public virtual DbSet<UserBiography> UserBiography { get; set; }
        public virtual DbSet<UserPortfolio> UserPortfolio { get; set; }
        public virtual DbSet<UserProfile> UserProfile { get; set; }
        public virtual DbSet<UserServices> UserServices { get; set; }
        public virtual DbSet<UserWorkplace> UserWorkplace { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=DESKTOP-L8FVL5O\\SQLEXPRESS;Database=Portfolio;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlogUser>(entity =>
            {
                entity.HasKey(e => e.IdBlog);

                entity.Property(e => e.IdBlog).HasColumnName("id_blog");

                entity.Property(e => e.DateBlog)
                    .HasColumnName("date_blog")
                    .HasColumnType("datetime");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.MainImg).HasColumnName("main_img");

                entity.Property(e => e.NameBlog).HasColumnName("name_blog");
    

                entity.Property(e => e.SubtypeBlog)
                    .HasColumnName("subtype_blog")
                    .HasMaxLength(50);

                entity.Property(e => e.TextBlog)
                    .HasColumnName("text_blog")
                    .HasColumnType("text");

                entity.Property(e => e.TypeBlog)
                    .HasColumnName("type_blog")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Education>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("education");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.EducationalInstitution)
                    .HasColumnName("educational_institution")
                    .HasDefaultValueSql("('Учебное заведение')");

                entity.Property(e => e.NameSpecialization).HasColumnName("name_specialization");

                entity.Property(e => e.StatusEducation).HasColumnName("status_education");
            });

            modelBuilder.Entity<ImgBlog>(entity =>
            {
                entity.HasKey(e => e.IdImg);

                entity.Property(e => e.IdImg).HasColumnName("id_img");

                entity.Property(e => e.DataImg)
                    .IsRequired()
                    .HasColumnName("data_img");

                entity.Property(e => e.IdBlog).HasColumnName("id_blog");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.TypeImg)
                    .IsRequired()
                    .HasColumnName("type_img")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ImgPortfolio>(entity =>
            {
                entity.HasKey(e => e.IdImg);

                entity.Property(e => e.IdImg).HasColumnName("id_img");

                entity.Property(e => e.DataImg)
                    .IsRequired()
                    .HasColumnName("data_img");

                entity.Property(e => e.IdPortfolio).HasColumnName("id_portfolio");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.TypeImg)
                    .IsRequired()
                    .HasColumnName("type_img")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<ImgUser>(entity =>
            {
                entity.HasKey(e => e.IdImg);

                entity.Property(e => e.IdImg).HasColumnName("id_img");

                entity.Property(e => e.DataImg).HasColumnName("data_img");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TypeImg)
                    .HasColumnName("type_img")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserAuthorization>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.CodeWord)
                    .HasColumnName("code_word")
                    .HasMaxLength(50);

                entity.Property(e => e.HashPassword)
                    .IsRequired()
                    .HasColumnName("hash_password");

                entity.Property(e => e.LoginUser)
                    .IsRequired()
                    .HasColumnName("login_user")
                    .HasMaxLength(50);

                entity.Property(e => e.StatusUser).HasColumnName("status_user");
            });

            modelBuilder.Entity<UserBiography>(entity =>
            {
                entity.HasKey(e => e.IdUser)
                    .HasName("PK_UserBiography_1");

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.NameHistoryBiography).HasColumnName("name_historyBiography");

                entity.Property(e => e.StatusUser)
                    .HasColumnName("status_user")
                    .HasColumnType("text");

                entity.Property(e => e.UserHistory)
                    .HasColumnName("user_history")
                    .HasColumnType("text")
                    .HasDefaultValueSql("('Немного обо мне')");
            });

            modelBuilder.Entity<UserPortfolio>(entity =>
            {
                entity.HasKey(e => e.IdPortfolio);

                entity.Property(e => e.IdPortfolio).HasColumnName("id_portfolio");

                entity.Property(e => e.DatePortfolio)
                    .HasColumnName("date_portfolio")
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DescriptionPortfolio)
                    .HasColumnName("description_portfolio");


                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.ImageMimeType)
                    .HasColumnName("image_mime_type")
                    .HasMaxLength(50);

                entity.Property(e => e.ImagePortfolio).HasColumnName("image_portfolio");

                entity.Property(e => e.LinkPortfolio).HasColumnName("link_portfolio");

                entity.Property(e => e.MainImg).HasColumnName("main_img");

                entity.Property(e => e.NamePortfolio)
                    .HasColumnName("name_portfolio");

                entity.Property(e => e.PricePortfolio)
                    .HasColumnName("price_portfolio")
                    .HasColumnType("money");

                entity.Property(e => e.StatusPortfolio)
                    .HasColumnName("status_portfolio")
                    .HasMaxLength(50);

                entity.Property(e => e.TypePortfolio)
                    .IsRequired()
                    .HasColumnName("type_portfolio")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserProfile>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.AdressUser)
                    .HasColumnName("adress_user")
                    .HasColumnType("text");

                entity.Property(e => e.AgeUser)
                    .HasColumnName("age_user")
                    .HasColumnType("date");

                entity.Property(e => e.AmailUser)
                    .HasColumnName("amail_user")
                    .HasMaxLength(50);

                entity.Property(e => e.MainImg).HasColumnName("main_img");

                entity.Property(e => e.MiddlenameUser)
                    .HasColumnName("middlename_user")
                    .HasMaxLength(50);

                entity.Property(e => e.NameUser)
                    .HasColumnName("name_user")
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('Name')");

                entity.Property(e => e.PhoneUser)
                    .HasColumnName("phone_user")
                    .HasMaxLength(50);

                entity.Property(e => e.SurnameUser)
                    .HasColumnName("surname_user")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UserServices>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.DescriptionServices)
                    .HasColumnName("description_services")
                    .HasColumnType("text")
                    .HasDefaultValueSql("('Описание услуги')");

                entity.Property(e => e.NameServices).HasColumnName("name_services");
            });

            modelBuilder.Entity<UserWorkplace>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.Property(e => e.IdUser).HasColumnName("id_user");

                entity.Property(e => e.DescriptionOrganization)
                    .HasColumnName("description_organization")
                    .HasColumnType("text");

                entity.Property(e => e.DescriptionWorkplace)
                    .HasColumnName("description_workplace")
                    .HasColumnType("text");

                entity.Property(e => e.KeySkill)
                    .HasColumnName("key_skill")
                    .HasColumnType("text")
                    .HasDefaultValueSql("('Молодец')");

                entity.Property(e => e.NameOrganization).HasColumnName("name_organization");

                entity.Property(e => e.UserPosition).HasColumnName("user_position");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
