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
using MarketChanges.GetDataServices;
using MarketChanges.GetData.SectorServices;
using MarketChanges.GetData.CompanyServices;
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

        public static ObservableCollection<IQuoteServices> Quotes { get; set; }

        public static void Start()
        {
            StartProc();
            //poll every 3600 seconds
            timer.Interval = new TimeSpan(0, 0, 3600);
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

            var cmp = repository
                .AsQueryOver(() => companyAlias)
                .Where(Restrictions.On(() => companyAlias.CompanySymbol).IsNotNull)
                .List();

            int limit = 0;
            foreach (Company c in cmp)
            {
                Quotes.Add(new MarketChanges.GetData.QueteServices.Quote(c.CompanySymbol));
                limit++;
                if (limit == 150 || c == cmp.Last<Company>())
                {
                    YahooMarketQuotes.Fetch(Quotes);
                    limit = 0;
                    Quotes = new ObservableCollection<IQuoteServices>();
                }
            }
        }
    }
}