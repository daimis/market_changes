using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketChanges.Data;
using MarketChanges.DataContracts;
using MarketChanges.DataEntities.Entities;
using MarketChanges.Data.DataContext;
using System.Collections.ObjectModel;
using System.Transactions;
using MarketChanges.GetDataServices;
using MarketChanges.GetData.SectorServices;
using NHibernate.Criterion;
using MarketChanges.GetData;
using System.Windows.Threading;
using Test;
using System.Xml.Linq;


namespace Test
{
    class Program
    {
        private const string BASE_URL = "https://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20geo.countries%20where%20place%3D%22North%20America%22&diagnostics=true&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

        private static readonly ISessionFactoryProvider SessionFactoryProvider = new SessionFactoryProvider();

        public static ObservableCollection<ISectorServices> Sectors { get; set; }

        static void Main(string[] args)
        {
            Sectors = new ObservableCollection<ISectorServices>();

            YahooMarketSectors.Fetch(Sectors);

            System.Console.ReadLine();
        }

        private static void CreateSector()
        {


            IRepository repository = new Repository(SessionFactoryProvider);

            var sector = new Sector
            {
                SectorName = "TestFirstName"
            };

            repository.Save(sector);

        }

    }
}
