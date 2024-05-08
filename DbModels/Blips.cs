using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Numerics;

namespace DbModels
{
    public class Blips
    {
        public int Id { get; set; }

        public int TypeId { get; set; }

        public double[] Position { get; set; }

        public TypesBlips TypeBlip { get; set; }
    }

    public class BlipsConfiguration : IEntityTypeConfiguration<Blips>
    {
        public void Configure(EntityTypeBuilder<Blips> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.TypeBlip)
                .WithMany(x => x.Blips);
        }
    }
}
