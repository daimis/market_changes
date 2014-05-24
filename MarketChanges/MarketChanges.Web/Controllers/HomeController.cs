using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Options;
using MarketChanges.Data;
using MarketChanges.Data.DataContext;
using MarketChanges.DataContracts;
using MarketChanges.DataEntities.Entities;
using MarketChanges.Web.Models;
using MarketChanges.Web.Models.Home;
using NHibernate.Criterion;
using NHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketChanges.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionFactoryProvider SessionFactoryProvider = new SessionFactoryProvider();

        private List<string> CompanyNames()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            Company companyAlias = null;

            var cmp = repository
                .AsQueryOver(() => companyAlias)
                .Where(Restrictions.On(() => companyAlias.CompanySymbol).IsNotNull)
                .List();

            List<string> list = new List<string>();

            foreach (var c in cmp)
            {
                list.Add(c.CompanyName);
            }

            List<string> newList = list.Distinct().ToList();

            return newList;
        }

        private IEnumerable<Quote> LastQuotes()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            Quote qteAlias = null;

            var quotes = repository
                .AsQueryOver(() => qteAlias)
                .Where(Restrictions.On(() => qteAlias.AskRealtime).IsNotNull)
                .List();

            IEnumerable<Quote> list = quotes.GroupBy(item => item.Company.Id).Select(group => group.Last());

            return list;
        }

        public ActionResult Index(string sortBy = "Company.CompanyName", bool ascending = true, int page = 1, int pageSize = 20)
        {
            var model = new CompanyGridModels()
            {
                // Sorting-related properties
                SortBy = sortBy,
                SortAscending = ascending,

                // Paging-related properties
                CurrentPageIndex = page,
                PageSize = pageSize
            };

            IEnumerable<Quote> allQte = LastQuotes();
            // Determine the total number of quotes being paged through (needed to compute PageCount)
            model.TotalRecordCount = allQte.Count();

            // Get the current page of quotes
            model.Quotes = allQte
                .Skip((model.CurrentPageIndex - 1) * model.PageSize)
                .Take(model.PageSize);

            IRepository repository = new Repository(SessionFactoryProvider);

            Company companyAlias = null;

            IList<Company> cmp = repository
                .AsQueryOver(() => companyAlias)
                .Where(Restrictions.On(() => companyAlias.Id).IsNotNull)
                .List();

            ViewData["Trending"] = cmp;

            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Categories()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            List<Sector> list = new List<Sector>();

            Sector sectorAlias = null;

            IList<Sector> sec = repository
                .AsQueryOver(() => sectorAlias)
                .Where(Restrictions.On(() => sectorAlias.Id).IsNotNull)
                .List();

            Industry industryAlias = null;

            IList<Industry> ind = repository
                .AsQueryOver(() => industryAlias)
                .Where(Restrictions.On(() => industryAlias.Id).IsNotNull)
                .List();

            Company companyAlias = null;

            IList<Company> cmp = repository
                .AsQueryOver(() => companyAlias)
                .Where(Restrictions.On(() => companyAlias.Id).IsNotNull)
                .List();

            ViewData["Sectors"] = sec;
            ViewData["Industries"] = ind;
            ViewData["Companies"] = cmp;

            return View();
        }

        [HttpPost]
        public ActionResult SectorList(string SectorName)
        {

            return View();
        }

        public ActionResult Favorites()
        {
            ViewBag.Message = "Your favorites.";

            return View();
        }

        [ActionName("SideBarCompany")]
        public ActionResult SideBarCompany()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            Company companyAlias = null;

            IList<Company> cmp = repository
                .AsQueryOver(() => companyAlias)
                .Where(Restrictions.On(() => companyAlias.Id).IsNotNull)
                .List();

            ViewData["Trending"] = cmp;

            
            return View();
        }

        public ActionResult CompanyInfo()
        {
            var model = new MyCompanyNames();

            return View(model);
        }

        [HttpPost]
        public ActionResult CompanyInfo(string cmpName)
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            var cmpBool = repository
                .Any<Company>(com => com.CompanyName == cmpName);

            if (cmpBool)
            {
                var cmp = repository
                    .First<Company>(com => com.CompanyName == cmpName);

                Quote quoteAlias = null;

                ViewData["OneCompany"] = cmp;

                var qte = repository
                    .AsQueryOver(() => quoteAlias)
                    .Where(q => q.Company.Id == cmp.Id)
                    .List();

                if (qte != null)
                {
                    ViewData["OneQuote"] = qte.Last();
                }

                object[] objAsk = new object[30];
                object[] objBid = new object[30];
                object[] objClose = new object[30];

                foreach (var q in qte)
                {
                    if(q.LastUpdate.Date.Month == System.DateTime.Now.Month)
                    {
                        objAsk[q.LastUpdate.Day] = q.Ask;
                        objBid[q.LastUpdate.Day] = q.Bid;
                        objClose[q.LastUpdate.Day] = q.PreviousClose;
                    }
                }

                if (objAsk == null)
                {
                    objAsk[0] = 0;
                    objBid[0] = 0;
                    objClose[0] = 0;
                }

                Highcharts chart = new Highcharts("chart")
           .InitChart(new Chart
           {
               DefaultSeriesType = ChartTypes.Line,
               MarginRight = 130,
               MarginBottom = 25,
               ClassName = "chart"
           })
           .SetTitle(new Title
           {
               Text = cmp.CompanyName,
               X = -20
           })
           .SetSubtitle(new Subtitle
           {
               Text = cmp.CompanySymbol,
               X = -20
           })
           .SetXAxis(new XAxis
           {
               Categories = new[] { "0", "1", }
           })
           .SetYAxis(new YAxis
           {
               Title = new YAxisTitle { Text = "Price, $" },
               PlotLines = new[]
                                      {
                                          new YAxisPlotLines
                                          {
                                              Value = 0,
                                              Width = 1,
                                              Color = ColorTranslator.FromHtml("#808080")
                                          }
                                      }
           })
           .SetLegend(new Legend
           {
               Layout = Layouts.Vertical,
               Align = HorizontalAligns.Right,
               VerticalAlign = VerticalAligns.Top,
               X = -10,
               Y = 100,
               BorderWidth = 0
           })
           .SetSeries(new[]
                       {
                           new Series { Name = "Ask", Data = new DotNet.Highcharts.Helpers.Data(objAsk)},
                           new Series { Name = "Bid", Data = new DotNet.Highcharts.Helpers.Data(objBid)},
                           new Series { Name = "Closed", Data = new DotNet.Highcharts.Helpers.Data(objClose)}
                       }
           );

                ViewData["OneCompanyChart"] = chart;

            }
            else
            {
                ViewData["OneCompanyBad"] = "Bad company name!";
            }

            return View();
        }

        [ActionName("Autocomplete")]
        public ActionResult Autocomplete(string term)
        {
            List<string> list = CompanyNames();

            var filteredItems = list.Where(
                item => item.IndexOf(term, StringComparison.InvariantCultureIgnoreCase) >= 0
                ).Take(25);

            return Json(filteredItems, JsonRequestBehavior.AllowGet);
        }
    }
}
