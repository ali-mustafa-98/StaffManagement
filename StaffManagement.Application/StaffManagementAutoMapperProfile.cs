using AutoMapper;
using StaffManagement.Designations;
using StaffManagement.Staffs;

namespace StaffManagement;
public class StaffManagementAutoMapperProfile : Profile
{
    public StaffManagementAutoMapperProfile()
    {
        MapStaffs();
        MapDesignations();
    }
    void MapStaffs()
    {
        CreateMap<CreateStaffDto, Staff>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()));
        CreateMap<UpdateStaffDto, Staff>();
        CreateMap<Staff, StaffDto>();
    }

    void MapDesignations()
    {
        CreateMap<CreateDesignationDto, Designation>()
            .ForMember(x => x.Id, opt => opt.MapFrom(x => Guid.NewGuid()));
        CreateMap<Designation, DesignationDto>();
    }
}