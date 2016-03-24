using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZKCloud.Domain.Models {
    public class PagedList<T> : List<T> {
        public long RecordCount { get; private set; }

		#region IPagedList Members

		//public int PageCount { get; private set; }
		//public int TotalItemCount { get; private set; }  RecordCount
		public long PageIndex { get; private set; }
		public long PageNumber { get { return PageIndex + 1; } }
		public long PageSize { get; private set; }
		public bool HasPreviousPage { get; private set; }
		public bool HasNextPage { get; private set; }
		public bool IsFirstPage { get; private set; }
		public bool IsLastPage { get; private set; }
		public long ItemStart { get; private set; }
		public long ItemEnd { get; private set; }

		#endregion

		public long PageCount {
            get { return RecordCount % PageSize == 0 ? RecordCount / PageSize : RecordCount / PageSize + 1; }
        }

        private PagedList() { }

        private PagedList(IEnumerable<T> source)
            : base(source) { }

        public static PagedList<T> Create(IEnumerable<T> source, long recordCount, long pageSize, long pageIndex) {
            return new PagedList<T>(source) {
                RecordCount = recordCount,
                PageSize = pageSize,
                PageIndex = pageIndex
            };
        }
    }
}
