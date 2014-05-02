using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using MarketChanges.GetDataServices;
using MarketChanges.GetData.SectorServices;
using System.Xml;

namespace MarketChanges.GetData
{
    public class YahooMarketSectors
    {
        private const string BASE_URL = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.sectors&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

        public static void Fetch(ObservableCollection<ISectorServices> sectors)
        {
            XDocument doc = XDocument.Load(BASE_URL);
            Parse(sectors, doc);
        }

        private static void Parse(ObservableCollection<ISectorServices> sectors, XDocument doc)
        {
            var sctrs = doc.Descendants("sector");

            foreach (var s in sctrs)
            {
                XAttribute sec = s.Attribute("name");
                Console.WriteLine(sec.Value);
                IEnumerable<XElement> indus = s.Elements("industry");
                if (indus != null)
                {
                    foreach (var w in indus)
                    {
                        XAttribute indust = w.Attribute("name");
                        Console.WriteLine("     " + indust.Value);
                    }
                }
            }
            Console.ReadLine();
        }
    }
}
