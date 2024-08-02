using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StaffManagement.Staffs;

namespace StaffManagement;
public class BaseCrudService<
	TEntity,
	TEntityDto,
	TKey,
	TEntityRequestDto,
	TEntityCreateDto,
	TEntityUpdateDto>
	where TEntity : class
	where TEntityDto : BaseEntityDto<TKey>
	where TEntityRequestDto : PagedAndSortedRequestDto
{

	/// <summary>
	/// For localization into different languages according to the current culture
	/// </summary>
	//public ILocalizer Localizer { get;  }
	public IMapper Mapper { get; }
	public IRepository<TEntity, TKey> Repository { get; }
	//Other common injectable properties will be here

	public BaseCrudService(IMapper mapper, IRepository<TEntity, TKey> repository)
	{
		Mapper = mapper;
		Repository = repository;
	}

	public virtual async Task DeleteAsync(TKey id)
	{
		await Repository.DeleteAsync(id);
	}
	public virtual async Task<TEntityDto> CreateAsync(TEntityCreateDto createInput)
	{
		//Check create policy or create role
		var entity = await MapToEntityAsync(createInput);
		entity = await Repository.CreateAsync(entity);

		return await MapToGetOutputDtoAsync(entity);
	}
	public virtual async Task<TEntityDto> GetAsync(TKey id)
	{
		var entity = await Repository.GetAsync(id);
		return await MapToGetOutputDtoAsync(entity);
	}
	public virtual async Task<PagedResultDto<TEntityDto>> GetListAsync(TEntityRequestDto input)
	{
		var query = Repository.GetQueryable();

		var count = await Repository.CountAsync();

		List<TEntityDto> output = [];
		if (count > 0)
		{
			//Filter the query according to reqeust
			query = CreateFilteredQuery(query, input);

			//Execute the query 
			var entitiesList = await query.ToListAsync();

			output = Mapper.Map<List<TEntity>, List<TEntityDto>>(entitiesList);
		}

		return new()
		{
			TotalCount = count,
			Items = output
		};
	}
	protected virtual IQueryable<TEntity> CreateFilteredQuery(IQueryable<TEntity> query, TEntityRequestDto input)
	{
		query = query.Skip(input.SkipCount ?? 0).Take(input.MaxResultResult ?? 10);

		return query;
	}
	public virtual async Task<TEntityDto> UpdateAsync(TKey id, TEntityUpdateDto input)
	{

		var entity = await Repository.GetAsync(id);
		//TODO: Check if input has id different than given id and normalize if it's default value, throw ex otherwise
		await MapToEntityAsync(input, entity);
		await Repository.UpdateAsync(entity);

		return await MapToGetOutputDtoAsync(entity);
	}
	protected virtual Task MapToEntityAsync(TEntityUpdateDto updateInput, TEntity entity)
	{
		entity = Mapper.Map(updateInput, entity);
		return Task.CompletedTask;
	}


	protected virtual Task<TEntity> MapToEntityAsync(TEntityCreateDto createInput)
	{
		var entity = Mapper.Map<TEntityCreateDto, TEntity>(createInput);

		return Task.FromResult(entity);
	}
	protected virtual Task<TEntityDto> MapToGetOutputDtoAsync(TEntity entity)
	{
		return Task.FromResult(Mapper.Map<TEntity, TEntityDto>(entity));
	}

}
