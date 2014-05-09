using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using MarketChanges.GetDataServices;
using MarketChanges.GetData.CompanyServices;
using System.Collections.Generic;
using MarketChanges.DataContracts;
using MarketChanges.Data;
using MarketChanges.Data.DataContext;
using System.Transactions;
using MarketChanges.DataEntities.Entities;
using NHibernate.Criterion;

namespace MarketChanges.GetData
{
    public class YahooMarketCompanies
    {
        private const string BASE_URL = "https://query.yahooapis.com/v1/public/yql?q=select * from yahoo.finance.industry where id in (select industry.id from yahoo.finance.sectors)&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

        private static readonly ISessionFactoryProvider SessionFactoryProvider = new SessionFactoryProvider();

        public static void Fetch()
        {
            XDocument doc = XDocument.Load(BASE_URL);
            Parse(doc);
        }

        private static void Parse(XDocument doc)
        {
            var companies = doc.Descendants("industry");

            IRepository repository = new Repository(SessionFactoryProvider);

            IList<string> cmp = new List<string>();

            foreach (var c in companies)
            {
                IList<CompanyServices.CompanyServices> companiesNames = new List<CompanyServices.CompanyServices>();

                XAttribute com = c.Attribute("name");

                IEnumerable<XElement> cmpns = c.Elements("company");

                if (cmpns != null)
                {
                    foreach (var w in cmpns)
                    {
                        XAttribute company = w.Attribute("name");
                        XAttribute symbol = w.Attribute("symbol");
                        companiesNames.Add(new CompanyServices.CompanyServices(com.Value, company.Value, symbol.Value));
                        Console.WriteLine(company.Value);
                    }
                }

                Industry indAlias = null;

                IRepository rep = new Repository(SessionFactoryProvider);

                var list = rep
                    .AsQueryOver(() => indAlias)
                    .Where(Restrictions.On(() => indAlias.Id).IsNotNull)
                    .List();

                Industry indusID = new Industry();

                using (var transaction = new TransactionScope())
                {
                    foreach (var i in list)
                    {
                        if(i.IndustryName == com.Value)
                        {
                            indusID = i;
                        }
                    }

                    foreach (var cp in companiesNames)
                    {
                        if (!cmp.Contains(cp.CompanySymbol))
                        {
                            var company = new Company()
                            {
                                Industry = indusID,
                                CompanyName = cp.CompanyName,
                                CompanySymbol = cp.CompanySymbol
                            };
                            repository.Save(company);
                            cmp.Add(cp.CompanySymbol);
                        }
                    }

                    repository.Commit();
                    transaction.Complete();
                }
            }
        }

    }
}
