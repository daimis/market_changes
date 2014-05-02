using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketChanges.DataEntities.Entities;

namespace MarketChanges.DataEntities.Mappings
{
    class SectorMap : EntityMapBase<Sector>
    {
        public SectorMap()
        {
            Map(m => m.SectorName).Length(40).Not.Nullable();

            HasMany(m => m.Industries);
        }
    }
}
