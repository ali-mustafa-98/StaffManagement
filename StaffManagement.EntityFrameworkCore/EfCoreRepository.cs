using Microsoft.EntityFrameworkCore;
using StaffManagement.Staffs;
using System.Linq.Expressions;
using System.Reflection;

namespace StaffManagement;
public class EfCoreRepository<TEntity, TContext, TKey> : IRepository<TEntity, TKey>
    where TEntity : class
    where TContext : DbContext
{
    private readonly TContext _context;

    public EfCoreRepository(TContext context)
    {
        this._context = context;
    }

    public virtual IQueryable<TEntity> GetQueryable()
    {
        return _context.Set<TEntity>().AsQueryable();
    }
    public async Task<TEntity> CreateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(TKey id)
    {
        if (id is null)
        {
            throw new ArgumentNullException("The Identifier must not be null");
        }
        var entity = await GetAsync(id);
        if (!_context.Set<TEntity>().Local.Contains(entity))
        {
            throw new InvalidOperationException("Entity is not being tracked by the context.");
        }
        _context.Set<TEntity>().Remove(entity);
        //await Task.Run(() => _context.Set<TEntity>().Remove(entity));
        await _context.SaveChangesAsync();
    }

    public async Task<TEntity> GetAsync(TKey id)
    {
        TEntity? entity = await _context.Set<TEntity>().FindAsync(id);
        if (entity is null)
        {
            throw new ArgumentNullException("There is no Entity Corresponds with the give identifier");
        }
        return entity;
    }



    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        if (entity is null)
        {
            throw new ArgumentNullException("Please double check that you have entered a valid identifier");
        }
        if (!_context.Set<TEntity>().Local.Contains(entity))
        {
            throw new InvalidOperationException("Entity is not being tracked by the context.");
        }
        _context.Entry(entity).State = EntityState.Modified;
        //await Task.Run(() => _context.Entry(entity).State = EntityState.Modified);
        await _context.SaveChangesAsync();
        return entity;
    }




    public async Task<TEntity> SearchForEntityAsync(string propertyName, object propertyValue)
    {

        var entityType = typeof(TEntity);
        var parameter = Expression.Parameter(entityType, "e");
        var property = Expression.Property(parameter, propertyName);

        if (property.Type == typeof(string))
        {
            var equals = Expression.Equal(property, Expression.Constant(propertyValue));
            var lambda = Expression.Lambda<Func<TEntity, bool>>(equals, parameter);
            return await _context.Set<TEntity>().AsQueryable().FirstOrDefaultAsync(lambda);
        }

        var propertyAsString = Expression.Call(property, "ToString", null);

        var propertyValueAsString = Expression.Constant(propertyValue?.ToString());

        var equals2 = Expression.Equal(propertyAsString, propertyValueAsString);
        var lambda2 = Expression.Lambda<Func<TEntity, bool>>(equals2, parameter);

        var entity = await _context.Set<TEntity>().AsQueryable()
            .FirstOrDefaultAsync(lambda2);

        return entity;

    }





    public IQueryable<TEntity> ApplySorting(IQueryable<TEntity> query, string sortProperty, bool ascending)
    {

        PropertyInfo propertyExists = typeof(TEntity).GetProperty(sortProperty, BindingFlags.Public | BindingFlags.Instance);
        if (propertyExists is null)
        {
            throw new InvalidOperationException("Please make sure that you have entered a valid sorting property");

        }

        var parameter = Expression.Parameter(typeof(TEntity), "c");
        var property = Expression.Property(parameter, sortProperty);
        var lambda = Expression.Lambda<Func<TEntity, object>>(Expression.Convert(property, typeof(object)), parameter);
        return ascending ? query.OrderBy(lambda) : query.OrderByDescending(lambda);
    }
    public IQueryable<TEntity> ApplySearching(IQueryable<TEntity> query, string searchProperty, object searchValue)
    {
        PropertyInfo propertyExists = typeof(TEntity).GetProperty(searchProperty, BindingFlags.Public | BindingFlags.Instance);
        if (propertyExists is null)
        {
            throw new InvalidOperationException("Please double check if the SearchProperty is correct");
        }
        var parameter = Expression.Parameter(typeof(TEntity));
        var propertyAccess = Expression.Property(parameter, searchProperty);

        // Convert the property to a string if it's not already
        Expression convertedProperty = propertyAccess;
        if (propertyAccess.Type != typeof(string))
        {
            var toStringMethod = typeof(object).GetMethod("ToString");
            convertedProperty = Expression.Call(propertyAccess, toStringMethod);
        }

        // Create an expression representing the 'Contains' operation on the property
        var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
        var containsExpression = Expression.Call(convertedProperty, containsMethod, Expression.Constant(searchValue.ToString(), typeof(string)));

        // Combine the property access and 'contains' expression into a lambda expression
        var lambda = Expression.Lambda<Func<TEntity, bool>>(containsExpression, parameter);

        // Use the lambda expression in a Where clause to filter the entities
        var filteredEntities = query.Where(lambda);

        return filteredEntities;

    }

    public async Task<int> CountAsync()
    {
        return await _context.Set<TEntity>().CountAsync();
    }
}
