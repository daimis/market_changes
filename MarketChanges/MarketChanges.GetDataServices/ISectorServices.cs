using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketChanges.GetDataServices
{
    public interface ISectorServices
    {
        string SectorName
        {
            get;
            set;
        }

        string IndustryName
        {
            get;
            set;
        }
    }
}
