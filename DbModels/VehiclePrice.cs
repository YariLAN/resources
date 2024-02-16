using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DbModels
{
    public class VehiclePrice
    {
        public int Id { get; set; }

        public int Model { get; set; }

        public double Price { get; set; }
    }

    public class VehiclePriceConfiguration : IEntityTypeConfiguration<VehiclePrice>
    {
        public void Configure(EntityTypeBuilder<VehiclePrice> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder.Property(x =>x.Model).IsRequired();

            builder.Property(x => x.Price).IsRequired();
        }
    }
}
