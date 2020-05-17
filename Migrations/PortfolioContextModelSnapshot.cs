using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using System;

namespace SytePortfolio.Migrations
{
    [DbContext(typeof(PortfolioContext))]
    partial class PortfolioContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SytePortfolio.BlogUser", b =>
                {
                    b.Property<int>("IdBlog")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_blog")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DateBlog")
                        .HasColumnName("date_blog")
                        .HasColumnType("datetime");

                    b.Property<int>("IdUser")
                        .HasColumnName("id_user")
                        .HasColumnType("int");

                    b.Property<int>("MainImg")
                        .HasColumnName("main_img")
                        .HasColumnType("int");

                    b.Property<string>("NameBlog")
                        .HasColumnName("name_blog")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("SubtypeBlog")
                        .HasColumnName("subtype_blog")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TextBlog")
                        .HasColumnName("text_blog")
                        .HasColumnType("text");

                    b.Property<string>("TypeBlog")
                        .HasColumnName("type_blog")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("IdBlog");

                    b.ToTable("BlogUser");
                });

            modelBuilder.Entity("SytePortfolio.Education", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("EducationalInstitution")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("educational_institution")
                        .HasColumnType("nvarchar(max)")
                        .HasDefaultValueSql("('Учебное заведение')");

                    b.Property<string>("NameSpecialization")
                        .HasColumnName("name_specialization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusEducation")
                        .HasColumnName("status_education")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.ToTable("education");
                });

            modelBuilder.Entity("SytePortfolio.ImgBlog", b =>
                {
                    b.Property<int>("IdImg")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_img")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("DataImg")
                        .IsRequired()
                        .HasColumnName("data_img")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("IdBlog")
                        .HasColumnName("id_blog")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnName("id_user")
                        .HasColumnType("int");

                    b.Property<string>("TypeImg")
                        .IsRequired()
                        .HasColumnName("type_img")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("IdImg");

                    b.ToTable("ImgBlog");
                });

            modelBuilder.Entity("SytePortfolio.ImgPortfolio", b =>
                {
                    b.Property<int>("IdImg")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_img")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("DataImg")
                        .IsRequired()
                        .HasColumnName("data_img")
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("IdPortfolio")
                        .HasColumnName("id_portfolio")
                        .HasColumnType("int");

                    b.Property<int>("IdUser")
                        .HasColumnName("id_user")
                        .HasColumnType("int");

                    b.Property<string>("TypeImg")
                        .IsRequired()
                        .HasColumnName("type_img")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("IdImg");

                    b.ToTable("ImgPortfolio");
                });

            modelBuilder.Entity("SytePortfolio.ImgUser", b =>
                {
                    b.Property<int>("IdImg")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_img")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte[]>("DataImg")
                        .HasColumnName("data_img")
                        .HasColumnType("varbinary(max)");

                    b.Property<int?>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user")
                        .HasColumnType("int")
                        .HasDefaultValueSql("((0))");

                    b.Property<string>("TypeImg")
                        .HasColumnName("type_img")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("IdImg");

                    b.ToTable("ImgUser");
                });

            modelBuilder.Entity("SytePortfolio.UserAuthorization", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CodeWord")
                        .HasColumnName("code_word")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("HashPassword")
                        .IsRequired()
                        .HasColumnName("hash_password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LoginUser")
                        .IsRequired()
                        .HasColumnName("login_user")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int?>("StatusUser")
                        .HasColumnName("status_user")
                        .HasColumnType("int");

                    b.HasKey("IdUser");

                    b.ToTable("UserAuthorization");
                });

            modelBuilder.Entity("SytePortfolio.UserBiography", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("NameHistoryBiography")
                        .HasColumnName("name_historyBiography")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("StatusUser")
                        .HasColumnName("status_user")
                        .HasColumnType("text");

                    b.Property<string>("UserHistory")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("user_history")
                        .HasColumnType("text")
                        .HasDefaultValueSql("('Немного обо мне')");

                    b.HasKey("IdUser")
                        .HasName("PK_UserBiography_1");

                    b.ToTable("UserBiography");
                });

            modelBuilder.Entity("SytePortfolio.UserPortfolio", b =>
                {
                    b.Property<int>("IdPortfolio")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_portfolio")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime?>("DatePortfolio")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("date_portfolio")
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("(getdate())");

                    b.Property<string>("DescriptionPortfolio")
                        .IsRequired()
                        .HasColumnName("description_portfolio")
                        .HasColumnType("text");

                    b.Property<int>("IdUser")
                        .HasColumnName("id_user")
                        .HasColumnType("int");

                    b.Property<string>("ImageMimeType")
                        .HasColumnName("image_mime_type")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<byte[]>("ImagePortfolio")
                        .HasColumnName("image_portfolio")
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("LinkPortfolio")
                        .HasColumnName("link_portfolio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MainImg")
                        .HasColumnName("main_img")
                        .HasColumnType("int");

                    b.Property<string>("NamePortfolio")
                        .IsRequired()
                        .HasColumnName("name_portfolio")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<decimal?>("PricePortfolio")
                        .HasColumnName("price_portfolio")
                        .HasColumnType("money");

                    b.Property<string>("StatusPortfolio")
                        .HasColumnName("status_portfolio")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("TypePortfolio")
                        .IsRequired()
                        .HasColumnName("type_portfolio")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("IdPortfolio");

                    b.ToTable("UserPortfolio");
                });

            modelBuilder.Entity("SytePortfolio.UserProfile", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AdressUser")
                        .HasColumnName("adress_user")
                        .HasColumnType("text");

                    b.Property<DateTime?>("AgeUser")
                        .HasColumnName("age_user")
                        .HasColumnType("date");

                    b.Property<string>("AmailUser")
                        .HasColumnName("amail_user")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<int>("MainImg")
                        .HasColumnName("main_img")
                        .HasColumnType("int");

                    b.Property<string>("MiddlenameUser")
                        .HasColumnName("middlename_user")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("NameUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("name_user")
                        .HasColumnType("nvarchar(50)")
                        .HasDefaultValueSql("('Name')")
                        .HasMaxLength(50);

                    b.Property<string>("PhoneUser")
                        .HasColumnName("phone_user")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.Property<string>("SurnameUser")
                        .HasColumnName("surname_user")
                        .HasColumnType("nvarchar(50)")
                        .HasMaxLength(50);

                    b.HasKey("IdUser");

                    b.ToTable("UserProfile");
                });

            modelBuilder.Entity("SytePortfolio.UserServices", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DescriptionServices")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("description_services")
                        .HasColumnType("text")
                        .HasDefaultValueSql("('Описание услуги')");

                    b.Property<string>("NameServices")
                        .HasColumnName("name_services")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.ToTable("UserServices");
                });

            modelBuilder.Entity("SytePortfolio.UserWorkplace", b =>
                {
                    b.Property<int>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("id_user")
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DescriptionOrganization")
                        .HasColumnName("description_organization")
                        .HasColumnType("text");

                    b.Property<string>("DescriptionWorkplace")
                        .HasColumnName("description_workplace")
                        .HasColumnType("text");

                    b.Property<string>("KeySkill")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("key_skill")
                        .HasColumnType("text")
                        .HasDefaultValueSql("('Молодец')");

                    b.Property<string>("NameOrganization")
                        .HasColumnName("name_organization")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserPosition")
                        .HasColumnName("user_position")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdUser");

                    b.ToTable("UserWorkplace");
                });
#pragma warning restore 612, 618
        }
    }
}
