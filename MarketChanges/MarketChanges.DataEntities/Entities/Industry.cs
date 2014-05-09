using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketChanges.DataEntities.Entities
{
    public class Industry : EntityBase<Industry>
    {
        public virtual string IndustryName { get; set; }

        public virtual Sector Sector { get; set; }

        public virtual IList<Company> Companies { get; set; }
    }
}
