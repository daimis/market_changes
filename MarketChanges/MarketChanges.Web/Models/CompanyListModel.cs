using MarketChanges.DataEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketChanges.Web.Models
{
    public class CompanyListModel
    {
        public IList<Company> companyList { get; set; }

        public SelectList DropDownList { get; set; }
    }

    public class MyListTable
    {
        public int Key { get; set; }
        public string Display { get; set; }
    }
}