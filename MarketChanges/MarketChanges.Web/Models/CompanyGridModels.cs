using MarketChanges.DataEntities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MarketChanges.Web.Models
{
    public class CompanyGridModels
    {
        // Constructor
        public CompanyGridModels()
        {
            // Define any default values here...
            this.PageSize = 10;
            this.NumericPageCount = 10;
        }


        // Data properties
        public IEnumerable<Quote> Quotes { get; set; }
        

        // Sorting-related properties
        public string SortBy { get; set; }
        public bool SortAscending { get; set; }
        public string SortExpression
        {
            get
            {
                return this.SortAscending ? this.SortBy + " asc" : this.SortBy + " desc";
            }
        }


        // Paging-related properties
        public int CurrentPageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalRecordCount { get; set; }
        public int PageCount
        {
            get
            {
                return Math.Max(this.TotalRecordCount / this.PageSize, 1);
            }
        }
        public int NumericPageCount { get; set; }
    }
}