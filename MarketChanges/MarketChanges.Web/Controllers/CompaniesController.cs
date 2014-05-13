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
    public class CompaniesController : Controller
    {
        private readonly ISessionFactoryProvider SessionFactoryProvider = new SessionFactoryProvider();
        //
        // GET: /Companies/

        // GET: /Products/SortAndPage?SortColumn=columnName&Ascending=true|false&page=number&pageSize=number
        

    }
}
