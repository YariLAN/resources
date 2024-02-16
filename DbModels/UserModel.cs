using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DbModels
{
    public class UserModel
    {
        public int Id { get; set; }

        public int LoginModelId { get; set; }

        public string? Name { get; set; }

        public double[] OldPosition { get; set; } = new double[] { -1537, -942, 11 };

        public double Money { get; set; } = 0.0;

        public bool IsAdmin { get; set; } = false;

        public LoginModel LoginModel { get; set; }

        public IEnumerable<VehicleModel> Vehicles { get; set;}
    }

    public class UserModelConfiguration : IEntityTypeConfiguration<UserModel>
    {
        public void Configure(EntityTypeBuilder<UserModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.Name).HasMaxLength(50);

            builder
                .HasMany(x => x.Vehicles)
                .WithOne(x => x.UserModel)
                .HasForeignKey(x => x.OwnerID)
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasOne(x => x.LoginModel)
                .WithOne(x => x.UserModel);
        }
    }
}
