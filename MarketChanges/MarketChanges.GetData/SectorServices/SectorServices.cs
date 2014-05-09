using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketChanges.GetDataServices;
using System.Windows;

namespace MarketChanges.GetData.SectorServices
{
    public class SectorServices : ISectorServices 
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string sectorName;
        private IList<string> industryName;

        public string SectorName
        {
            get { return sectorName; }
            set
            {
                sectorName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("sector"));
            }
        }

        public IList<string> IndustryName
        {
            get { return industryName; }
            set
            {
                industryName = value;
                if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("industry"));
            }
        }
    }

    public class SectorInformation
    {
        public List<SectorServices> SectorList { get; set; }

        public SectorInformation()
        {
            SectorList = new List<SectorServices>();
        }
    }
}
