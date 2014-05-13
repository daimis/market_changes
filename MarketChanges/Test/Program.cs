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

namespace Test
{
    class Program
    {
        private static readonly ISessionFactoryProvider SessionFactoryProvider = new SessionFactoryProvider();

        private static readonly DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Background);

        private static IList<Company> cmp = new List<Company>();

        public static ObservableCollection<IQuoteServices> Quotes { get; set; }

        static void Main(string[] args)
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            Quotes = new ObservableCollection<IQuoteServices>();

            //YahooMarketSectors.Fetch();
            //YahooMarketCompanies.Fetch();

            //Some example tickers
            //get the data

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

            
            //poll every 3600 seconds
            //timer.Interval = new TimeSpan(0, 0, 3600);
            //timer.Tick += (o, e) => YahooMarketQuotes.Fetch(Quotes);                  
            //timer.Start();

            Console.WriteLine("Baige");
            Console.ReadLine();
        }

        private static bool HasElements()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            Sector sectorAlias = null;

            var list = repository
                .AsQueryOver(() => sectorAlias)
                .Where(Restrictions.On(() => sectorAlias.Id).IsNotNull)
                .List();

            if (list.Count > 0)
            {
                return true;
            }
            return false;
        }

        private static void CompaniesSymbolList(IList<Company> companies)
        {
            

        }

    }
}
