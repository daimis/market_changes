using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MaasOne;
using MaasOne.Base;
using MaasOne.Finance.YahooFinance;

namespace MarketChanges.DataEntities
{
    class Download
    {
        public void Download()
        {
            //Parameters
            IEnumerable<YahooManaged.Finance.Sector> sectors = new YahooManaged.Finance.Sector[] { YahooManaged.Finance.Sector.Basic_Materials };

            //Download Sectors
            YahooManaged.Finance.API.MarketDownload dl = new YahooManaged.Finance.API.MarketDownload();
            YahooManaged.Finance.API.SectorResponse respSectors = dl.DownloadSectors(sectors);

            //Response/Result
            if (respSectors.Connection.State == YahooManaged.Base.ConnectionState.Success)
            {
                foreach (YahooManaged.Finance.SectorData sector in respSectors.Result)
                {
                    YahooManaged.Finance.Sector sectorID = sector.ID;
                    string sectorName = sector.Name;
                    List<YahooManaged.Finance.IndustryData> industries = sector.Industries;
                    int industryCount = industries.Count;

                    //Download Industries
                    YahooManaged.Finance.API.IndustryResponse respIndustries = dl.DownloadIndustries(industries);

                    //Response/Result
                    if (respIndustries.Connection.State == YahooManaged.Base.ConnectionState.Success)
                    {
                        foreach (YahooManaged.Finance.IndustryData industry in respIndustries.Result)
                        {
                            int industryID = industry.ID;
                            string industryName = industry.Name;
                            List<YahooManaged.Finance.CompanyInfoData> companies = industry.Companies;
                            int companyCount = companies.Count;

                            foreach (YahooManaged.Finance.CompanyInfoData company in companies)
                            {
                                string companyID = company.ID;
                                string companyName = company.Name;
                                int employees = company.FullTimeEmployees;
                                System.DateTime start = company.StartDate;
                                string industryNameByCompany = company.IndustryName;

                            }
                        }
                    }

                }
            }

        }
    }
}
