using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using MarketChanges.GetDataServices;

namespace MarketChanges.GetData
{
    public class YahooMarketQuotes
    {
        private const string BASE_URL = "http://query.yahooapis.com/v1/public/yql?q=select%20*%20from%20yahoo.finance.quotes%20where%20symbol%20in%20({0})&env=store%3A%2F%2Fdatatables.org%2Falltableswithkeys";

        public static void Fetch(ObservableCollection<IQuoteServices> quotes)
        {
            string symbolList = String.Join("%2C", quotes.Select(w => "%22" + w.Symbol + "%22").ToArray());
            string url = string.Format(BASE_URL, symbolList);

            XDocument doc = XDocument.Load(url);
            Parse(quotes, doc);
        }

        private static void Parse(ObservableCollection<IQuoteServices> quotes, XDocument doc)
        {
            XElement results = doc.Root.Element("results");

            foreach (IQuoteServices quote in quotes)
            {
                XElement q = results.Elements("quote").First(w => w.Attribute("symbol").Value == quote.Symbol);

                quote.Ask = GetDecimal(q.Element("Ask").Value);
                quote.Bid = GetDecimal(q.Element("Bid").Value);
                quote.AverageDailyVolume = GetDecimal(q.Element("AverageDailyVolume").Value);
                quote.BookValue = GetDecimal(q.Element("BookValue").Value);
                quote.Change = GetDecimal(q.Element("Change").Value);
                quote.DividendShare = GetDecimal(q.Element("DividendShare").Value);
                quote.LastTradeDate = GetDateTime(q.Element("LastTradeDate") + " " + q.Element("LastTradeTime").Value);
                quote.EarningsShare = GetDecimal(q.Element("EarningsShare").Value);
                quote.EpsEstimateCurrentYear = GetDecimal(q.Element("EPSEstimateCurrentYear").Value);
                quote.EpsEstimateNextYear = GetDecimal(q.Element("EPSEstimateNextYear").Value);
                quote.EpsEstimateNextQuarter = GetDecimal(q.Element("EPSEstimateNextQuarter").Value);
                quote.DailyLow = GetDecimal(q.Element("DaysLow").Value);
                quote.DailyHigh = GetDecimal(q.Element("DaysHigh").Value);
                quote.YearlyLow = GetDecimal(q.Element("YearLow").Value);
                quote.YearlyHigh = GetDecimal(q.Element("YearHigh").Value);
                quote.MarketCapitalization = GetDecimal(q.Element("MarketCapitalization").Value);
                quote.Ebitda = GetDecimal(q.Element("EBITDA").Value);
                quote.ChangeFromYearLow = GetDecimal(q.Element("ChangeFromYearLow").Value);
                quote.PercentChangeFromYearLow = GetDecimal(q.Element("PercentChangeFromYearLow").Value);
                quote.ChangeFromYearHigh = GetDecimal(q.Element("ChangeFromYearHigh").Value);
                quote.LastTradePrice = GetDecimal(q.Element("LastTradePriceOnly").Value);
                quote.PercentChangeFromYearHigh = GetDecimal(q.Element("PercebtChangeFromYearHigh").Value); //missspelling in yahoo for field name
                quote.FiftyDayMovingAverage = GetDecimal(q.Element("FiftydayMovingAverage").Value);
                quote.TwoHunderedDayMovingAverage = GetDecimal(q.Element("TwoHundreddayMovingAverage").Value);
                quote.ChangeFromTwoHundredDayMovingAverage = GetDecimal(q.Element("ChangeFromTwoHundreddayMovingAverage").Value);
                quote.PercentChangeFromTwoHundredDayMovingAverage = GetDecimal(q.Element("PercentChangeFromTwoHundreddayMovingAverage").Value);
                quote.PercentChangeFromFiftyDayMovingAverage = GetDecimal(q.Element("PercentChangeFromFiftydayMovingAverage").Value);
                quote.Name = q.Element("Name").Value;
                quote.Open = GetDecimal(q.Element("Open").Value);
                quote.PreviousClose = GetDecimal(q.Element("PreviousClose").Value);
                quote.ChangeInPercent = GetDecimal(q.Element("ChangeinPercent").Value);
                quote.PriceSales = GetDecimal(q.Element("PriceSales").Value);
                quote.PriceBook = GetDecimal(q.Element("PriceBook").Value);
                quote.ExDividendDate = GetDateTime(q.Element("ExDividendDate").Value);
                quote.PeRatio = GetDecimal(q.Element("PERatio").Value);
                quote.DividendPayDate = GetDateTime(q.Element("DividendPayDate").Value);
                quote.PegRatio = GetDecimal(q.Element("PEGRatio").Value);
                quote.PriceEpsEstimateCurrentYear = GetDecimal(q.Element("PriceEPSEstimateCurrentYear").Value);
                quote.PriceEpsEstimateNextYear = GetDecimal(q.Element("PriceEPSEstimateNextYear").Value);
                quote.ShortRatio = GetDecimal(q.Element("ShortRatio").Value);
                quote.OneYearPriceTarget = GetDecimal(q.Element("OneyrTargetPrice").Value);
                quote.Volume = GetDecimal(q.Element("Volume").Value);
                quote.StockExchange = q.Element("StockExchange").Value;

                //quote.LastUpdate = DateTime.Now;
            }
        }

        private static decimal? GetDecimal(string input)
        {
            if (input == null) return null;

            input = input.Replace("%", "");

            decimal value;

            if (Decimal.TryParse(input, out value)) return value;
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
    }
}
