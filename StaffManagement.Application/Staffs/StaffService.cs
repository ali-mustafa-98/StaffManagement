
using AutoMapper;
using StaffManagement.Designations;

namespace StaffManagement.Staffs;
public class StaffService : BaseCrudService<
    Staff,
    StaffDto,
    Guid,
    StaffRequestDto,
    CreateStaffDto,
    UpdateStaffDto>, IStaffService
{
    private readonly IStaffRepository _staffRepository;

    public StaffService(IMapper mapper, IStaffRepository staffRepository) : base(mapper, staffRepository)
    {
        _staffRepository = staffRepository;
    }


    protected override async Task<Staff> MapToEntityAsync(CreateStaffDto createInput)
    {
        var entity = await base.MapToEntityAsync(createInput);

        if (createInput.DesignationIds != null && createInput.DesignationIds.Any())
        {
            foreach (var designationId in createInput.DesignationIds)
            {
                entity.AddStaffDesignation(new StaffDesignation(Guid.NewGuid(), entity.Id, designationId));
            }
        }
        return entity;
    }

    protected override async Task<StaffDto> MapToGetOutputDtoAsync(Staff entity)
    {
        var entityDto = await base.MapToGetOutputDtoAsync(entity);

        entityDto.Designations = [];
        if (entity.StaffDesignations != null && entity.StaffDesignations.Any())
        {
            foreach (var staffDesignation in entity.StaffDesignations)
            {
                if (staffDesignation.Designation != null)
                {
                    entityDto.Designations.Add(Mapper.Map<Designation, DesignationDto>(staffDesignation.Designation));

                }
            }
        }
        return entityDto;
    }
    protected override IQueryable<Staff> CreateFilteredQuery(IQueryable<Staff> query, StaffRequestDto input)
    {
        query = _staffRepository.WithNoTracking();

        query = base.CreateFilteredQuery(query, input);

        if (input.DesignationIds != null && input.DesignationIds.Any())
        {
            query = query.Where(x => x.StaffDesignations != null && x.StaffDesignations.Any(x => input.DesignationIds.Contains(x.DesignationId)));
        }
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