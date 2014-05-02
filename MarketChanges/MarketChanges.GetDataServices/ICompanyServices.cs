using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketChanges.GetDataServices
{
    public interface ICompanyServices
    {
        string CompanySymbol
        {
            get;
            set;
        }

        string CompanyName
        {
            get;
            set;
        }
    }
}
