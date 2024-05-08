using DbModels;
using GTANetworkAPI;

namespace Mappers
{
    public class BlipsDbMapper
    {
        public static (TypesBlips, Blips) ToMap(string Name, int TypeId, Vector3 position)
        {
            return (new TypesBlips { Id = TypeId, Name = Name }, 
                    new Blips { TypeId = TypeId, Position = new double[] { position.X, position.Y, position.Z } });
        }

        public static (TypesBlips, Blips) ToMap(string Name, Vector3 position)
        {
            return (new TypesBlips { Name = Name },
                    new Blips { Position = new double[] { position.X, position.Y, position.Z } });
        }
    }
}
