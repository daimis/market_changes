using MarketChanges.Data;
using MarketChanges.Data.DataContext;
using MarketChanges.DataContracts;
using MarketChanges.DataEntities.Entities;
using MarketChanges.Web.Models;
using NHibernate.Criterion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketChanges.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISessionFactoryProvider SessionFactoryProvider = new SessionFactoryProvider();

        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult CompanyInfo()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            List<Company> list = new List<Company>();

            var droplist = new List<MyListTable>();

            Company companyAlias = null;

            var cmp = repository
                .AsQueryOver(() => companyAlias)
                .Where(Restrictions.On(() => companyAlias.CompanySymbol).IsNotNull)
                .List();

            CompanyListModel model = new CompanyListModel();
            foreach (var c in cmp)
            {
                droplist.Add(new MyListTable
                {
                    Key = c.Id,
                    Display = c.CompanyName
                });
            }

            ViewBag.CompaniesName = cmp;

            model.DropDownList = new SelectList(droplist, "Key", "Display");

            return View(model);
        }

        public ActionResult Categories()
        {
            IRepository repository = new Repository(SessionFactoryProvider);

            List<Sector> list = new List<Sector>();

            Sector sectorAlias = null;

            var cmp = repository
                .AsQueryOver(() => sectorAlias)
                .Where(Restrictions.On(() => sectorAlias.Id).IsNotNull)
                .List();

            CompanyListModel model = new CompanyListModel();
            //model.companyList = cmp;

            ViewBag.Sectors = cmp;

            return View();
        }

        public ActionResult Favorites()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult SideBarCompany()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
