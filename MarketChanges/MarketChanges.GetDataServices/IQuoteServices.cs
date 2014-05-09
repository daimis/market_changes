using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketChanges.GetDataServices
{
    public interface IQuoteServices
    {
        DateTime LastUpdate
        {
            get;
            set;
        }

        string StockExchange
        {
            get;
            set;
        }

        decimal? Volume
        {
            get;
            set;
        }

        decimal? PeRatio
        {
            get;
            set;
        }

        decimal? PercentChangeFromTwoHundredDayMovingAverage
        {
            get;
            set;
        }

        DateTime? DividendPayDate
        {
            get;
            set;
        }

        decimal? DividendYield
        {
            get;
            set;
        }

        decimal? OneYearPriceTarget
        {
            get;
            set;
        }

        decimal? ShortRatio
        {
            get;
            set;
        }

        decimal? PriceEpsEstimateNextYear
        {
            get;
            set;
        }

        decimal? PriceEpsEstimateCurrentYear
        {
            get;
            set;
        }

        decimal? PegRatio
        {
            get;
            set;
        }

        DateTime? ExDividendDate
        {
            get;
            set;
        }

        decimal? PriceBook
        {
            get;
            set;
        }

        decimal? PriceSales
        {
            get;
            set;
        }

        decimal? ChangeInPercent
        {
            get;
            set;
        }

        decimal? PreviousClose
        {
            get;
            set;
        }

        /*decimal? Open
        {
            get;
            set;
        }*/

        string Name
        {
            get;
            set;
        }

        decimal? PercentChangeFromFiftyDayMovingAverage
        {
            get;
            set;
        }

        decimal? ChangeFromTwoHundredDayMovingAverage
        {
            get;
            set;
        }

        decimal? TwoHunderedDayMovingAverage
        {
            get;
            set;
        }

        decimal? FiftyDayMovingAverage
        {
            get;
            set;
        }

        decimal? LastTradePrice
        {
            get;
            set;
        }

        decimal? PercentChangeFromYearHigh
        {
            get;
            set;
        }

        decimal? ChangeFromYearHigh
        {
            get;
            set;
        }

        decimal? PercentChangeFromYearLow
        {
            get;
            set;
        }

        decimal? ChangeFromYearLow
        {
            get;
            set;
        }

        decimal? Ebitda
        {
            get;
            set;
        }

        decimal? MarketCapitalization
        {
            get;
            set;
        }

        decimal? YearlyHigh
        {
            get;
            set;
        }

        decimal? YearlyLow
        {
            get;
            set;
        }

        decimal? DailyHigh
        {
            get;
            set;
        }

        decimal? DailyLow
        {
            get;
            set;
        }

        decimal? EpsEstimateNextQuarter
        {
            get;
            set;
        }

        decimal? EpsEstimateNextYear
        {
            get;
            set;
        }

        decimal? EpsEstimateCurrentYear
        {
            get;
            set;
        }

        decimal? EarningsShare
        {
            get;
            set;
        }

        DateTime? LastTradeDate
        {
            get;
            set;
        }

        decimal? DividendShare
        {
            get;
            set;
        }

        decimal? Change
        {
            get;
            set;
        }

        decimal? ChangePercent
        {
            get;
            set;
        }

        decimal? BookValue
        {
            get;
            set;
        }

        decimal? Ask
        {
            get;
            set;
        }

        decimal? Bid
        {
            get;
            set;
        }

        decimal? AverageDailyVolume
        {
            get;
            set;
        }

        string Symbol
        {
            get;
            set;
        }

    }
}
