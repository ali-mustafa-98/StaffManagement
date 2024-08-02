namespace StaffManagement.Staffs;

public interface IRepository<T, Key> where T : class
{
	Task<T> GetAsync(Key id);
	IQueryable<T> GetQueryable();
	Task<T> CreateAsync(T entity);
	Task<T> UpdateAsync(T entity);
	Task DeleteAsync(Key id);

	Task<int> CountAsync();
	//Task<T> SearchForEntityAsync(string propertyName, object propertyValue);


	//IQueryable<T> ApplySearching(IQueryable<T> query, string searchProperty, object searchValue);

	//IQueryable<T> ApplySorting(IQueryable<T> query, string sortingProperty, bool ascending);

}
