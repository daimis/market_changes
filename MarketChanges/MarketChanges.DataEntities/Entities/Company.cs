using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketChanges.DataEntities.Entities
{
    public class Company : EntityBase<Company>
    {
        public virtual string CompanyName { get; set; }

        public virtual string CompanySymbol { get; set; }

        public virtual Industry Industry { get; set; }

        public virtual IList<Quote> Quotes { get; set; }
    }
}
