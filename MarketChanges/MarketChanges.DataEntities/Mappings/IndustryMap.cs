using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketChanges.DataEntities.Entities;

namespace MarketChanges.DataEntities.Mappings
{
    class IndustryMap : EntityMapBase<Industry>
    {
        public IndustryMap()
        {
            Map(m => m.IndustryName).Length(40).Not.Nullable();

            //HasMany(m => m.Companies);
        }
    }
}
