namespace TiendaVirtual.Domain.Pagination
{
    using System;
    using System.Collections.Generic;

    public interface IPagedList
    {
        int TotalItems { get; set; }

        int ItemsPerPage { get; set; }

        int CurrentPage { get; set; }

        int TotalPages { get; }

        bool HasPreviousPage { get; }

        bool HasNextPage { get; }
    }

    public class PagedList<T> : List<T>, IPagedList
    {
        public PagedList(IEnumerable<T> source, int totalItems, int currentPage, int pageSize)
        {
            this.TotalItems = totalItems;
            this.CurrentPage = currentPage;
            this.ItemsPerPage = pageSize;
            this.AddRange(source);
        }

        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }

        public int TotalPages
        {
            get { return (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage); }
        }

        public bool HasPreviousPage
        {
            get { return (CurrentPage > 1); }
        }

        public bool HasNextPage
        {
            get { return CurrentPage < TotalPages; }
        }
    }

}