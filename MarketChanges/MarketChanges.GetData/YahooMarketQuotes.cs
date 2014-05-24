using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using MarketChanges.DataContracts;
using MarketChanges.Data;
using MarketChanges.Data.DataContext;
using System.Transactions;
using MarketChanges.DataEntities.Entities;
using NHibernate.Criterion;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;

namespace MarketChanges.GetData
{
    public class YahooMarketQuotes
    {
        private const string BASE_URL = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20({0})&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

        private static readonly ISessionFactoryProvider SessionFactoryProvider = new SessionFactoryProvider();

        public static void Fetch(ObservableCollection<string> quotes)
        {
            string symbolList = String.Join("%2C", quotes.Select(w => "%22" + w + "%22").ToArray());
            string url = string.Format(BASE_URL, symbolList);

            XDocument doc = XDocument.Load(url);
            Parse(quotes, doc);
        }

        private static void Parse(ObservableCollection<string> quotes, XDocument doc)
        {
            XElement results = doc.Root.Element("results");

            IRepository repository = new Repository(SessionFactoryProvider);

            Company comAlias = null;

            IRepository rep = new Repository(SessionFactoryProvider);

            var list = rep
                .AsQueryOver(() => comAlias)
                .Where(Restrictions.On(() => comAlias.Id).IsNotNull)
                .List();

            Company comp = new Company();

            foreach (var quote in quotes)
            {
                XElement q = null;
                if (results.Elements("quote").Any(w => w.Attribute("symbol").Value.ToString() == quote))
                {
                    q = results.Elements("quote").First(w => w.Attribute("symbol").Value == quote);
                }

                foreach (var i in list)
                {
                    if (i.CompanySymbol == quote)
                    {
                        comp = i;
                    }
                }
                if (q != null && q.Element("ErrorIndicationreturnedforsymbolchangedinvalid").Value == "")
                {
                    using (var transaction = new TransactionScope())
                    {
                        var quoteData = new Quote()
                        {
                            Company = comp,
                            LastUpdate = DateTime.Now,
                            Ask = GetDecimal(q.Element("Ask").Value),
                            AskRealtime = GetDecimal(q.Element("AskRealtime").Value),
                            Bid = GetDecimal(q.Element("Bid").Value),
                            BidRealtime = GetDecimal(q.Element("BidRealtime").Value),
                            AverageDailyVolume = GetDecimal(q.Element("AverageDailyVolume").Value),
                            BookValue = GetDecimal(q.Element("BookValue").Value),
                            Change = GetDecimal(q.Element("Change").Value),
                            ChangeRealtime = GetDecimal(q.Element("ChangeRealtime").Value),
                            DividendShare = GetDecimal(q.Element("DividendShare").Value),
                            LastTradeDate = GetDateTime(q.Element("LastTradeDate") + " " + q.Element("LastTradeTime").Value),
                            EarningsShare = GetDecimal(q.Element("EarningsShare").Value),
                            EpsEstimateCurrentYear = GetDecimal(q.Element("EPSEstimateCurrentYear").Value),
                            EpsEstimateNextYear = GetDecimal(q.Element("EPSEstimateNextYear").Value),
                            EpsEstimateNextQuarter = GetDecimal(q.Element("EPSEstimateNextQuarter").Value),
                            DailyLow = GetDecimal(q.Element("DaysLow").Value),
                            DailyHigh = GetDecimal(q.Element("DaysHigh").Value),
                            YearlyLow = GetDecimal(q.Element("YearLow").Value),
                            YearlyHigh = GetDecimal(q.Element("YearHigh").Value),
                            MarketCapitalization = GetDecimal(q.Element("MarketCapitalization").Value),
                            Ebitda = GetDecimal(q.Element("EBITDA").Value),
                            ChangeFromYearLow = GetDecimal(q.Element("ChangeFromYearLow").Value),
                            PercentChangeFromYearLow = GetDecimal(q.Element("PercentChangeFromYearLow").Value),
                            ChangeFromYearHigh = GetDecimal(q.Element("ChangeFromYearHigh").Value),
                            LastTradePrice = GetDecimal(q.Element("LastTradePriceOnly").Value),
                            PercentChangeFromYearHigh = GetDecimal(q.Element("PercebtChangeFromYearHigh").Value),
                            FiftyDayMovingAverage = GetDecimal(q.Element("FiftydayMovingAverage").Value),
                            TwoHunderedDayMovingAverage = GetDecimal(q.Element("TwoHundreddayMovingAverage").Value),
                            ChangeFromTwoHundredDayMovingAverage = GetDecimal(q.Element("ChangeFromTwoHundreddayMovingAverage").Value),
                            PercentChangeFromTwoHundredDayMovingAverage = GetDecimal(q.Element("PercentChangeFromTwoHundreddayMovingAverage").Value),
                            PercentChangeFromFiftyDayMovingAverage = GetDecimal(q.Element("PercentChangeFromFiftydayMovingAverage").Value),
                            PreviousClose = GetDecimal(q.Element("PreviousClose").Value),
                            ChangeInPercent = GetDecimal(q.Element("ChangeinPercent").Value),
                            PriceSales = GetDecimal(q.Element("PriceSales").Value),
                            PriceBook = GetDecimal(q.Element("PriceBook").Value),
                            ExDividendDate = GetDateTime(q.Element("ExDividendDate").Value),
                            PeRatio = GetDecimal(q.Element("PERatio").Value),
                            DividendPayDate = GetDateTime(q.Element("DividendPayDate").Value),
                            PegRatio = GetDecimal(q.Element("PEGRatio").Value),
                            PriceEpsEstimateCurrentYear = GetDecimal(q.Element("PriceEPSEstimateCurrentYear").Value),
                            PriceEpsEstimateNextYear = GetDecimal(q.Element("PriceEPSEstimateNextYear").Value),
                            ShortRatio = GetDecimal(q.Element("ShortRatio").Value),
                            OneYearPriceTarget = GetDecimal(q.Element("OneyrTargetPrice").Value),
                            Volume = GetDecimal(q.Element("Volume").Value),
                            StockExchange = q.Element("StockExchange").Value
                        };
                        repository.Save(quoteData);

                        repository.Commit();
                        transaction.Complete();
                    }
                } else
                {
                    if (comp.Quotes.Count == 0)
                    {
                        rep.Delete<Company>(comp);
                        rep.Commit();
                    }
                }
            }
        }

        private static decimal? GetDecimal(string input)
        {
            if (input == null) return null;

            input = input.Replace("%", "");
            decimal value;

            if (Decimal.TryParse(input, NumberStyles.Any, new CultureInfo("en-US"), out value))
            {
                return value;
            }
            return null;
        }

        private static DateTime? GetDateTime(string input)
        {
            if (input == null) return null;

            DateTime value;

            if (DateTime.TryParse(input, out value)) return value;
            return null;
        }

        public string StockExchange
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? Volume
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PeRatio
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PercentChangeFromTwoHundredDayMovingAverage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime? DividendPayDate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? DividendYield
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? OneYearPriceTarget
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? ShortRatio
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PriceEpsEstimateNextYear
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PriceEpsEstimateCurrentYear
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PegRatio
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime? ExDividendDate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PriceBook
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PriceSales
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? ChangeInPercent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PreviousClose
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? Open
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Name
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PercentChangeFromFiftyDayMovingAverage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? ChangeFromTwoHundredDayMovingAverage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? TwoHunderedDayMovingAverage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? FiftyDayMovingAverage
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? LastTradePrice
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PercentChangeFromYearHigh
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? ChangeFromYearHigh
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? PercentChangeFromYearLow
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? ChangeFromYearLow
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? Ebitda
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? MarketCapitalization
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? YearlyHigh
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? YearlyLow
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? DailyHigh
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? DailyLow
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? EpsEstimateNextQuarter
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? EpsEstimateNextYear
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? EpsEstimateCurrentYear
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? EarningsShare
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime? LastTradeDate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? DividendShare
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? Change
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? ChangePercent
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? BookValue
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? Ask
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? Bid
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal? AverageDailyVolume
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Symbol
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        /*public static void Fetch(ObservableCollection<string> Quotes)
        {
            throw new NotImplementedException();
        }*/
    }
}
