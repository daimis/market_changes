using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketChanges.DataEntities.Entities;

namespace MarketChanges.DataEntities.Mappings
{
    class QuoteMap : EntityMapBase<Quote>
    {
        public QuoteMap()
        {
            Map(q => q.LastUpdate).Not.Nullable();

            Map(q => q.Ask).Nullable();
            Map(q => q.Bid).Nullable();
            Map(q => q.AverageDailyVolume);
            Map(q => q.BookValue);
            Map(q => q.Change);
            Map(q => q.DividendShare);
            Map(q => q.LastTradeDate);
            Map(q => q.EarningsShare);
            Map(q => q.EpsEstimateCurrentYear);
            Map(q => q.EpsEstimateNextYear);
            Map(q => q.EpsEstimateNextQuarter);
            Map(q => q.DailyLow);
            Map(q => q.DailyHigh);
            Map(q => q.YearlyLow);
            Map(q => q.YearlyHigh);
            Map(q => q.MarketCapitalization);
            Map(q => q.Ebitda);
            Map(q => q.ChangeFromYearLow);
            Map(q => q.PercentChangeFromYearLow);
            Map(q => q.ChangeFromYearHigh);
            Map(q => q.LastTradePrice);
            Map(q => q.PercentChangeFromYearHigh);
            Map(q => q.FiftyDayMovingAverage);
            Map(q => q.TwoHunderedDayMovingAverage);
            Map(q => q.ChangeFromTwoHundredDayMovingAverage);
            Map(q => q.PercentChangeFromTwoHundredDayMovingAverage);
            Map(q => q.PercentChangeFromFiftyDayMovingAverage);
            Map(q => q.PreviousClose);
            Map(q => q.ChangeInPercent);
            Map(q => q.PriceSales);
            Map(q => q.PriceBook);
            Map(q => q.ExDividendDate);
            Map(q => q.PeRatio);
            Map(q => q.DividendPayDate);
            Map(q => q.PegRatio);
            Map(q => q.PriceEpsEstimateCurrentYear);
            Map(q => q.PriceEpsEstimateNextYear);
            Map(q => q.ShortRatio);
            Map(q => q.OneYearPriceTarget);
            Map(q => q.Volume);
            Map(q => q.StockExchange);

            References(q => q.Company);

        }
    }
}
