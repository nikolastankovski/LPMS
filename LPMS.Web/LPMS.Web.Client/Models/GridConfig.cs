using Radzen;

namespace LPMS.Web.Models
{
    public class GridConfig<TModel>
    {
        public bool AllowFiltering { get; set; } = true;
        public bool AllowColumnResize { get; set; } = true;
        public bool AllowAlternatingRows { get; set; } = true;
        public bool AllowSorting { get; set; } = true;
        public bool AllowPaging { get; set; } = true;
        public bool ShowPagingSummary { get; set; } = true;

        public FilterMode FilterMode { get; set; } = FilterMode.Advanced;
        public HorizontalAlign PagerHorizontalAlign { get; set; } = HorizontalAlign.Left;
        public LogicalFilterOperator LogicalFilterOperator { get; set; } = LogicalFilterOperator.Or;
        public DataGridSelectionMode SelectionMode { get; set; } = DataGridSelectionMode.Single;

        public int PageSize { get; set; } = 5;
        public IEnumerable<int> PageSizeOptions { get; set; } = new List<int>() { 5, 10, 20, 50 };
        public IEnumerable<TModel> Data { get; set; } = Enumerable.Empty<TModel>();
        public string ColumnWidth { get; set; } = "300px";
        public string EmptyText { get; set; } = "empty";
    }
}