using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbModels
{
    public class LoginModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public UserModel UserModel { get; set; }
    }

    public class LoginModelConfiguration : IEntityTypeConfiguration<LoginModel>
    {
        public void Configure(EntityTypeBuilder<LoginModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasMaxLength(50).IsRequired();

            builder.Property(x => x.Password).HasMaxLength(50).IsRequired();

            builder
                .HasOne(x => x.UserModel)
                .WithOne(x => x.LoginModel)
                .HasForeignKey<UserModel>(x => x.LoginModelId);
        }
    }
}
