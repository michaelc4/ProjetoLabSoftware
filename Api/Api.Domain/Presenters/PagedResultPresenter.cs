﻿using System;
using System.Collections.Generic;

namespace Api.Domain.Presenters
{
    public abstract class PagedResultBase
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage
        {
            get { return (CurrentPage - 1) * PageSize + 1; }
        }

        public int LastRowOnPage
        {
            get { return Math.Min(CurrentPage * PageSize, RowCount); }
        }
    }

    public class PagedResultPresenter<T> : PagedResultBase where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResultPresenter()
        {
            Results = new List<T>();
        }
    }
}
