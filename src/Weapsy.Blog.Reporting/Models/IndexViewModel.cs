using System;
using System.Collections.Generic;

namespace Weapsy.Blog.Reporting.Models
{
    public class IndexViewModel
    {
        public Guid BlogId { get; set; }
        public string BlogTitle { get; set; }
        public string BlogTheme { get; set; }
        public IList<IndexPostViewModel> Posts { get; set; }
    }
}