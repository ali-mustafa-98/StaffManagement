
using AutoMapper;

namespace StaffManagement.Staffs;
public class StaffService : BaseCrudService<
	Staff,
	StaffDto,
	Guid,
	StaffRequestDto,
	CreateStaffDto,
	UpdateStaffDto>, IStaffService
{
	public StaffService(IMapper mapper, IStaffRepository staffRepository) : base(mapper, staffRepository)
	{
	}


	protected override IQueryable<Staff> CreateFilteredQuery(IQueryable<Staff> query, StaffRequestDto input)
	{
		query = base.CreateFilteredQuery(query, input);

		if (!string.IsNullOrEmpty(input.Search))
		{
			var searchTerms = input.Search.Split(',');
			foreach (var term in searchTerms)
			{
				query = query.Where(x => x.FirstName.ToLower() == term.ToLower());
			}
		}

		return query;
	}
	public override async Task<StaffDto> UpdateAsync(Guid id, UpdateStaffDto updateInput)
	{
		if (id != updateInput.Id)
		{
			//Throw an exception and we may need to localize it
		}
		return await base.UpdateAsync(id, updateInput);
	}
}