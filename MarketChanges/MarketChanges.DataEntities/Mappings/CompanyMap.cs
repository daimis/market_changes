using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketChanges.DataEntities.Entities;

namespace MarketChanges.DataEntities.Mappings
{
    class CompanyMap : EntityMapBase<Company>
    {
        public CompanyMap()
        {
            Map(m => m.CompanyName).Length(40).Not.Nullable();

            Map(m => m.CompanySymbol).Length(20).Not.Nullable();

            References(m => m.Industry);

            HasMany(m => m.Quotes);
        }
    }
}
