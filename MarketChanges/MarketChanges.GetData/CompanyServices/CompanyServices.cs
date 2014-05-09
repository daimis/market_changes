using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketChanges.GetDataServices;

namespace MarketChanges.GetData.CompanyServices
{
    public class CompanyServices : ICompanyServices
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string industryName;
        private string companySymbol;
        private string companyName;

        public string CompanySymbol
        {
            get { return companySymbol; }
            set
            {
                companySymbol = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CompanySymbol"));
            }
        }

        public string CompanyName
        {
            get { return companyName; }
            set
            {
                companyName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("CompanyName"));
            }
        }

        public CompanyServices(string iName, string sName, string sSymbol)
        {
            this.industryName = iName;
            this.companyName = sName;
            this.companySymbol = sSymbol;
        }

        public string IndustryName
        {
            get { return industryName; }
            set
            {
                industryName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("sectorName"));
            }
        }

    }
}
