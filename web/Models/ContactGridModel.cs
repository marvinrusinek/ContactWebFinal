using ContactWebLibrary;
using MvcContrib.Pagination;
using MvcContrib.UI.Grid;

namespace ContactWeb.Models
{
    public class ContactGridModel {
        public GridSortOptions GridSortOptions { get; set; }
        public IPagination<Contact> ContactPagedList { get; set; }
    }
}