using MarketChanges.DataEntities.Entities;
using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketChanges.Web.Controllers
{
    public class BaseController : Controller
    {
        private Quote _DataContext = null;
        protected Quote DataContext
        {
            get
            {
                if (_DataContext == null)
                    _DataContext = new Quote();

                // Eager load Category info
                var options = new DataLoadOptions();
                options.LoadWith<Quote>(p => p.Company.CompanyName);

                return _DataContext;
            }
        }
    }
}
