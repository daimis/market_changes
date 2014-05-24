using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using MarketChanges.Data;
using MarketChanges.DataContracts;
using MarketChanges.DataEntities.Entities;
using MarketChanges.Data.DataContext;
using System.Collections.ObjectModel;
using System.Transactions;
using NHibernate.Criterion;
using MarketChanges.GetData;
using System.Xml.Linq;

namespace MarketChanges.Web
{
    public static class YahooAnalizer
    {
        private static readonly ISessionFactoryProvider SessionFactoryProvider = new SessionFactoryProvider();

        private static readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);

        private static IList<Company> cmp = new List<Company>();

        public static ObservableCollection<string> Quotes { get; set; }

        public static void Start()
        {
            StartProc();
            //poll every 1 hour
            timer.Interval = new TimeSpan(1, 0, 0);
            timer.Tick += (o, e) => StartProc();    
            timer.Start();
        }

        public static void Stop()
        {

        }

        private static void StartProc()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            Company companyAlias = null;

            Sector sectorAlias = null;

            if (repository
                .AsQueryOver(() => sectorAlias)
                .RowCount() == 0)
            {
                YahooMarketSectors.Fetch();
            }

            if (repository
                .AsQueryOver(() => companyAlias)
                .RowCount() == 0)
            {
                YahooMarketCompanies.Fetch();
            }

            var cmp = repository
                .AsQueryOver(() => companyAlias)
                .Where(Restrictions.On(() => companyAlias.CompanySymbol).IsNotNull)
                .List();

            int limit = 0;

            if (cmp.Count > 0)
            {
                Quotes = new ObservableCollection<string>();
                foreach (Company c in cmp)
                {
                    string symbol = c.CompanySymbol;
                    Quotes.Add(symbol);
                    limit++;
                    if (limit == 200 || c == cmp.Last<Company>())
                    {
                        YahooMarketQuotes.Fetch(Quotes);
                        limit = 0;
                        Quotes = new ObservableCollection<string>();
                    }
                }
            }
        }
    }
}