using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using MarketChanges.GetDataServices;

namespace MarketChanges.GetData
{
    public class YahooMarketCompanies
    {
        private const string BASE_URL = "https://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.industry where id in (select industry.id from yahoo.finance.sectors)&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

        public static void Fetch(ObservableCollection<ICompanyServices> companies)
        {
            string symbolList = String.Join("%2C", companies.Select(w => "%22" + w.CompanyName + "%22").ToArray());
            string url = string.Format(BASE_URL, symbolList);

            XDocument doc = XDocument.Load(url);
            Parse(companies, doc);
        }

        private static void Parse(ObservableCollection<ICompanyServices> companies, XDocument doc)
        {
            XElement results = doc.Root.Element("results");

            foreach (ICompanyServices company in companies)
            {
                XElement c = results.Elements("company").First(w => w.Attribute("symbol").Value == company.CompanySymbol);

                company.CompanyName = c.Element("CompanyName").Value;
                company.CompanySymbol = c.Element("CompanySymbol").Value;

            }
        }
    }
}
