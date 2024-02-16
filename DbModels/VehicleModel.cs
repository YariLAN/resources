using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbModels
{
    public class VehicleModel
    {
        public int Id { get; set; }
        public int OwnerID { get; set; }
        public int Model { get; set; }

        public string Number { get; set; }
        public float[] OldPosition { get; set; }
        public float Rotation { get; set; } = 0;
        public int ColorOne { get; set; } = 0;
        public int ColorTwo { get; set; } = 0;
        public double Price { get; set; }

        public VehicleModel() { }

        public UserModel UserModel { get; set; }
    }

    public class VehicleModelConfiguration : IEntityTypeConfiguration<VehicleModel>
    {
        public void Configure(EntityTypeBuilder<VehicleModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x => x.OwnerID).IsRequired();

            builder.Property(x => x.Number).HasMaxLength(5);

            builder
                .HasOne(x => x.UserModel)
                .WithMany(x => x.Vehicles);
        }
    }
}
