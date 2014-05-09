using System;
using System.Transactions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MarketChanges.GetDataServices;
using MarketChanges.GetData.SectorServices;
using MarketChanges.DataContracts;
using MarketChanges.Data;
using MarketChanges.Data.DataContext;
using MarketChanges.DataEntities.Entities;
using System.Xml;

namespace MarketChanges.GetData
{
    public class YahooMarketSectors
    {
        private const string BASE_URL = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.sectors&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

        private static readonly ISessionFactoryProvider SessionFactoryProvider = new SessionFactoryProvider();

        public static void Fetch()
        {
            XDocument doc = XDocument.Load(BASE_URL);
            Parse(doc);
        }

        private static void Parse(XDocument doc)
        {
            var sctrs = doc.Descendants("sector");

            IRepository repository = new Repository(SessionFactoryProvider);

            foreach (var s in sctrs)
            {
                IList<string> industriesNames = new List<string>();
                XAttribute sec = s.Attribute("name");
                IEnumerable<XElement> indus = s.Elements("industry");
                if (indus != null)
                {
                    foreach (var w in indus)
                    {
                        XAttribute indust = w.Attribute("name");
                        industriesNames.Add(indust.Value);
                    }
                }

                using (var transaction = new TransactionScope())
                {
                    var sector = new Sector()
                    {
                        SectorName = sec.Value,
                    };

                    repository.Save(sector);

                    foreach (string name in industriesNames)
                    {
                        var industry = new Industry()
                        {
                            Sector = sector,
                            IndustryName = name
                        };
                        repository.Save(industry);
                    }

                    repository.Commit();
                    transaction.Complete();
                }
            }
        }

        public static void GetYahooIndustriesId(ObservableCollection<int> id)
        {
            XDocument doc = XDocument.Load(BASE_URL);

            var sctrs = doc.Descendants("sector");
            foreach (var s in sctrs)
            {
                XAttribute sec = s.Attribute("name");
                IEnumerable<XElement> indus = s.Elements("industry");
                if (indus != null)
                {
                    foreach (var w in indus)
                    {
                        XAttribute indust = w.Attribute("id");
                        int cnv = Convert.ToInt16(indust.Value);
                        id.Add(cnv);
                    }
                }
            }
        }
    }
}
