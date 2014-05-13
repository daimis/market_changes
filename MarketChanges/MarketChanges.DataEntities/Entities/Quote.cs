using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketChanges.DataEntities.Entities
{
    public class Quote : EntityBase<Quote>
    {
        public virtual DateTime LastUpdate { get; set; }

        public virtual decimal? Ask { get; set; }
        public virtual decimal? Bid { get; set; }
        public virtual decimal? AskRealtime { get; set; }
        public virtual decimal? BidRealtime { get; set; }
        public virtual decimal? ChangeRealtime { get; set; }
        public virtual decimal? AverageDailyVolume { get; set; }
        public virtual decimal? BookValue { get; set; }
        public virtual decimal? Change { get; set; }
        public virtual decimal? DividendShare { get; set; }
        public virtual DateTime? LastTradeDate { get; set; }
        public virtual decimal? EarningsShare { get; set; }
        public virtual decimal? EpsEstimateCurrentYear { get; set; }
        public virtual decimal? EpsEstimateNextYear { get; set; }
        public virtual decimal? EpsEstimateNextQuarter { get; set; }
        public virtual decimal? DailyLow { get; set; }
        public virtual decimal? DailyHigh { get; set; }
        public virtual decimal? YearlyLow { get; set; }
        public virtual decimal? YearlyHigh { get; set; }
        public virtual decimal? MarketCapitalization { get; set; }
        public virtual decimal? Ebitda { get; set; }
        public virtual decimal? ChangeFromYearLow { get; set; }
        public virtual decimal? PercentChangeFromYearLow { get; set; }
        public virtual decimal? ChangeFromYearHigh { get; set; }
        public virtual decimal? LastTradePrice { get; set; }
        public virtual decimal? PercentChangeFromYearHigh { get; set; }
        public virtual decimal? FiftyDayMovingAverage { get; set; }
        public virtual decimal? TwoHunderedDayMovingAverage { get; set; }
        public virtual decimal? ChangeFromTwoHundredDayMovingAverage { get; set; }
        public virtual decimal? PercentChangeFromTwoHundredDayMovingAverage { get; set; }
        public virtual decimal? PercentChangeFromFiftyDayMovingAverage { get; set; }
        public virtual decimal? PreviousClose { get; set; }
        public virtual decimal? ChangeInPercent { get; set; }
        public virtual decimal? PriceSales { get; set; }
        public virtual decimal? PriceBook { get; set; }
        public virtual DateTime? ExDividendDate { get; set; }
        public virtual decimal? PeRatio { get; set; }
        public virtual DateTime? DividendPayDate { get; set; }
        public virtual decimal? PegRatio { get; set; }
        public virtual decimal? PriceEpsEstimateCurrentYear { get; set; }
        public virtual decimal? PriceEpsEstimateNextYear { get; set; }
        public virtual decimal? ShortRatio { get; set; }
        public virtual decimal? OneYearPriceTarget { get; set; }
        public virtual decimal? Volume { get; set; }
        public virtual string StockExchange { get; set; }

        public virtual Company Company { get; set; }
    }
}
