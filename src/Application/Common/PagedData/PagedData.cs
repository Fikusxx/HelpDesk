using Marten.Pagination;

namespace Application.Common;

public sealed class PagedData<TReadModel> where TReadModel : class, IReadModel
{
	public long TotalItemCount { get; private set; }
	public long PageNumber { get; private set; } = 1;
	public long PageSize { get; private set; }
	public long PageCount { get; private set; }
	public bool HasPreviousPage { get; private set; }
	public bool HasNextPage { get; private set; }
	public bool IsFirstPage { get; private set; } = true;
	public bool IsLastPage { get; private set; }
	public long FirstItemOnPage { get; private set; }
	public long LastItemOnPage { get; private set; }
	public IEnumerable<TReadModel> Items { get; private set; } = Enumerable.Empty<TReadModel>();

	private PagedData() { }

	public static PagedData<TReadModel> Create(IPagedList<TReadModel> list)
	{
		var pagedData = new PagedData<TReadModel>();

		if (list is null || list.Count == 0)
			return pagedData;

		pagedData.TotalItemCount = list.TotalItemCount;
		pagedData.PageNumber = list.PageNumber;
		pagedData.PageSize = list.PageSize;
		pagedData.PageCount = list.PageCount;
		pagedData.HasPreviousPage = list.HasPreviousPage;
		pagedData.HasNextPage = list.HasNextPage;
		pagedData.IsFirstPage = list.IsFirstPage;
		pagedData.IsLastPage = list.IsLastPage;
		pagedData.FirstItemOnPage = list.FirstItemOnPage;
		pagedData.LastItemOnPage = list.LastItemOnPage;
		pagedData.Items = list;

		return pagedData;
	}
}
