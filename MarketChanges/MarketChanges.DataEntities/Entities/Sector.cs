using System;
using System.Collections.Generic;

namespace MarketChanges.DataEntities.Entities
{
    public class Sector : EntityBase<Sector>
    {
        public virtual string SectorName { get; set; }

        public virtual IList<Industry> Industries { get; set; }
    }
}
